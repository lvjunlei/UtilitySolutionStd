using FileServer.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Utility.Logs;

namespace FileServer.Controllers
{
    /// <summary>
    /// 文件服务配置
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        /// <summary>
        /// 获取文件服务配置信息
        /// </summary>
        /// <returns>文件服务配置信息</returns>
        [HttpGet("GetConfig")]
        public async Task<IActionResult> GetConfig()
        {
            try
            {
                return await Task.Run(() =>
                {
                    var cfg = FileServerConfig.GetConfig();
                    LogHelper.Info($"查询文件服务配置信息：ClientIP（{HttpContext.GetClientIp()}）");
                    return Ok(cfg);
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"查询文件服务配置信息发生了异常：{ex.Message}");
                LogHelper.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 更新文件存储路径
        /// </summary>
        /// <param name="fileSavePath">新的文件存储路径</param>
        /// <returns></returns>
        [HttpPost("UpdateFileSavePath")]
        public async Task<IActionResult> UpdateFileSavePath(string fileSavePath)
        {
            try
            {
                return await Task.Run(() =>
                {
                    FileServerConfig.SetFileSavePath(fileSavePath);
                    LogHelper.Info($"文件存储路径修改为：{fileSavePath}");
                    return Ok($"修改文件存储路径：{fileSavePath} 完成");
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"修改文件存储路径为：{fileSavePath} 发生了异常", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 更新文件访问路径
        /// </summary>
        /// <param name="fileVisitUrl">新的文件访问路径</param>
        /// <returns></returns>
        [HttpPost("UpdateFileVisitUrl")]
        public async Task<IActionResult> UpdateFileVisitUrl(string fileVisitUrl)
        {
            try
            {
                return await Task.Run(() =>
                {
                    FileServerConfig.SetFileVisitUrl(fileVisitUrl);
                    LogHelper.Info($"文件访问路径修改为：{fileVisitUrl}");
                    return Ok($"修改文件访问路径：{fileVisitUrl} 完成");
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"修改文件访问路径为：{fileVisitUrl} 发生了异常", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}