using FileServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Logs;

namespace FileServer.Controllers
{
    /// <summary>
    /// 文件管理服务API
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        #region Upload

        /// <summary>
        /// 上传文件（以表单形式上传）
        /// </summary>
        /// <param name="fromFile">文件信息</param>
        /// <param name="filePath">
        /// 指定的文件存储路径
        /// (为空时则使用默认目录)
        /// </param>
        /// <param name="canCover">能否覆盖已存在文件</param>
        /// <param name="isAutoRename">是否自动重命名</param>
        /// <returns>文件上传结果</returns>
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile fromFile, string filePath = null, bool canCover = false, bool isAutoRename = false)
        {
            try
            {
                var file = ContentDispositionHeaderValue
                    .Parse(fromFile.ContentDisposition)
                    .FileName
                    .Trim();
                var fileName = file.Value;
                if (isAutoRename)
                {
                    fileName = $"{Guid.NewGuid().ToString()}.{fileName.Substring(fileName.LastIndexOf('.') + 1)}";
                }
                var result = new FileUploadResult(fileName, file.Value, fromFile.Length);

                var savePath = Path.Combine(FileServerConfig.FileSavePath, fileName);
                if (!Directory.Exists(FileServerConfig.FileSavePath))
                {
                    Directory.CreateDirectory(FileServerConfig.FileSavePath);
                }

                if (!string.IsNullOrEmpty(filePath))
                {
                    var path = Path.Combine(FileServerConfig.FileSavePath, filePath);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    savePath = Path.Combine(FileServerConfig.FileSavePath, path, fileName);
                }

                if (System.IO.File.Exists(savePath))
                {
                    if (!canCover)
                    {
                        result.Message = $"已存在文件 {savePath}";
                        return Conflict(result);
                    }
                    System.IO.File.Delete(savePath);
                }

                var visitUrl = savePath.Remove(0, FileServerConfig.FileSavePath.Length);
                if (!string.IsNullOrEmpty(FileServerConfig.FileVisitUrl))
                {
                    visitUrl = FileServerConfig.FileVisitUrl + visitUrl;
                }
                result.VisitUrl = visitUrl;
                LogHelper.Info($"visitUrl:{visitUrl}");
                LogHelper.Info($"FileVisitUrl:{FileServerConfig.FileVisitUrl}");
                return await Task.Run(() =>
                {
                    using (var fs = new FileStream(savePath, FileMode.Create))
                    {
                        fromFile.CopyTo(fs);
                        fs.Flush();
                    }
                    return Ok(result);
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 上传文件信息
        /// </summary>
        /// <param name="files">文件信息</param>
        /// <param name="filePath">
        /// 指定的文件存储路径
        /// (为空时则使用默认目录)
        /// </param>
        /// <param name="canCover">能否覆盖已存在文件</param>
        /// <param name="isAutoRename">是否自动重命名</param>
        /// <returns>文件上传结果</returns>
        [HttpPost]
        [Route("Uploads")]
        public async Task<IActionResult> Uploads(ICollection<IFormFile> files, string filePath = null, bool canCover = false, bool isAutoRename = false)
        {
            var size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(FileServerConfig.FileSavePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new { count = files.Count, size, filePath });
        }

        #endregion

        #region Download

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="filePath">
        /// 指定下载文件的路径
        /// (为空时则使用默认目录)
        /// </param>
        /// <returns></returns>
        [HttpGet("Download")]
        public async Task<IActionResult> Download(string fileName, string filePath = null)
        {
            try
            {
                if (!Directory.Exists(FileServerConfig.FileSavePath))
                {
                    return NotFound($"找不到文件存储路径：{FileServerConfig.FileSavePath}");
                }
                var fileFullName = Path.Combine(FileServerConfig.FileSavePath, fileName);

                if (!string.IsNullOrEmpty(filePath))
                {
                    var path = Path.Combine(FileServerConfig.FileSavePath, filePath);
                    if (!Directory.Exists(path))
                    {
                        return NotFound($"找不到文件存储路径：{path}");
                    }
                    fileFullName = Path.Combine(FileServerConfig.FileSavePath, path, fileName);
                }
                if (!System.IO.File.Exists(fileFullName))
                {
                    return NotFound($"找不到指定的文件：{filePath} {fileName}");
                }
                return await Task.Run(() =>
                {
                    var fs = new FileStream(fileFullName, FileMode.Open);
                    return File(fs, "application/vnd.android.package-archive", fileName);
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除指定文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="filePath">
        /// 指定删除文件的路径
        /// (为空时则使用默认目录)
        /// </param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string fileName, string filePath = null)
        {
            try
            {
                if (!Directory.Exists(FileServerConfig.FileSavePath))
                {
                    return NotFound($"找不到文件存储路径：{FileServerConfig.FileSavePath}");
                }
                var fileFullName = Path.Combine(FileServerConfig.FileSavePath, fileName);

                if (!string.IsNullOrEmpty(filePath))
                {
                    var path = Path.Combine(FileServerConfig.FileSavePath, filePath);
                    if (!Directory.Exists(path))
                    {
                        return NotFound($"找不到文件存储路径：{path}");
                    }
                    fileFullName = Path.Combine(FileServerConfig.FileSavePath, path, fileName);
                }

                return await Task.Run(() =>
                {
                    if (System.IO.File.Exists(fileFullName))
                    {
                        System.IO.File.Delete(fileFullName);
                    }
                    return Ok($"文件：{filePath} {fileName} 已删除");
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 批量删除指定文件
        /// </summary>
        /// <param name="files">文件名称集合</param>
        /// <returns></returns>
        [HttpDelete("Deletes")]
        public async Task<IActionResult> Deletes(params string[] files)
        {
            try
            {
                if (!Directory.Exists(FileServerConfig.FileSavePath))
                {
                    return NotFound($"找不到文件存储路径：{FileServerConfig.FileSavePath}");
                }

                return await Task.Run(() =>
                {
                    var sb = new StringBuilder();
                    foreach (var fileName in files)
                    {
                        var fileFullName = Path.Combine(FileServerConfig.FileSavePath, fileName);
                        try
                        {
                            if (System.IO.File.Exists(fileFullName))
                            {
                                System.IO.File.Delete(fileFullName);
                            }
                            sb.AppendLine($"删除文件 {fileName} 成功；");
                        }
                        catch (Exception ex)
                        {
                            sb.AppendLine($"删除文件 {fileName} 失败，{ex.Message}；");
                        }
                    }
                    return Ok(sb.ToString());
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}