using LiteDB;
using Newtonsoft.Json;
using System;
using System.Linq;
using Utility.Logs;

namespace FileServer
{
    /// <summary>
    /// 服务配置信息
    /// </summary>
    public class FileServerConfig
    {
        /// <summary>
        /// 文件存储路径
        /// </summary>
        public static string FileSavePath { get; private set; }

        /// <summary>
        /// 可访问的相对路径
        /// </summary>
        public static string FileVisitUrl { get; private set; }

        /// <summary>
        /// 初始化配置文件
        /// </summary>
        static FileServerConfig()
        {
            using (var db = new LiteDatabase(@"Config.db"))
            {
                var cfgs = db.GetCollection<SaveConfig>("saveconfigs");
                var cfg = cfgs.Find(u => u.Name == "default").FirstOrDefault();
                if (cfg == null)
                {
                    cfg = new SaveConfig
                    {
                        Name = "default",
                        FileSavePath = "C:\\Files\\",
                        FileVisitUrl = "Files"
                    };
                    cfgs.Insert(cfg);
                }
                FileSavePath = cfg.FileSavePath;
                FileVisitUrl = cfg.FileVisitUrl;
            }
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        public static SaveConfig GetConfig()
        {
            try
            {
                using (var db = new LiteDatabase(@"Config.db"))
                {
                    var cfgs = db.GetCollection<SaveConfig>("saveconfigs");
                    var cfg = cfgs.Find(u => u.Name == "default").FirstOrDefault();
                    if (cfg == null)
                    {
                        cfg = new SaveConfig
                        {
                            Name = "default",
                            FileSavePath = "C:\\Files\\",
                            FileVisitUrl = "Files"
                        };
                        cfgs.Insert(cfg);
                    }
                    return cfg;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("查询配置信息发生了异常~", ex);
                return null;
            }
        }

        /// <summary>
        /// 设置文件存储路径
        /// </summary>
        /// <param name="fileSavePath">新的存储路径</param>
        public static void SetFileSavePath(string fileSavePath)
        {
            if (string.IsNullOrEmpty(fileSavePath))
            {
                throw new ArgumentNullException($"文件存储路径 fileSavePath {fileSavePath} 不能为空");
            }
            using (var db = new LiteDatabase(@"Config.db"))
            {
                var cfgs = db.GetCollection<SaveConfig>("saveconfigs");
                var cfg = cfgs.Find(u => u.Name == "default").FirstOrDefault();
                if (cfg == null)
                {
                    cfg = new SaveConfig
                    {
                        Name = "default",
                        FileSavePath = "C:\\Files\\",
                        FileVisitUrl = "Files"
                    };
                    cfgs.Insert(cfg);
                }
                cfg.FileSavePath = fileSavePath;
                cfgs.Update(cfg);
            }
            FileSavePath = fileSavePath;
        }

        /// <summary>
        /// 设置文件访问路径
        /// </summary>
        /// <param name="fileVisitUrl">新的访问路径</param>
        public static void SetFileVisitUrl(string fileVisitUrl)
        {
            if (string.IsNullOrEmpty(fileVisitUrl))
            {
                throw new ArgumentNullException($"文件访问路径 fileVisitUrl {fileVisitUrl} 不能为空");
            }
            using (var db = new LiteDatabase(@"Config.db"))
            {
                var cfgs = db.GetCollection<SaveConfig>("saveconfigs");
                var cfg = cfgs.Find(u => u.Name == "default").FirstOrDefault();
                if (cfg == null)
                {
                    cfg = new SaveConfig
                    {
                        Name = "default",
                        FileSavePath = "C:\\Files\\",
                        FileVisitUrl = "Files"
                    };
                    cfgs.Insert(cfg);
                }
                cfg.FileVisitUrl = fileVisitUrl;
                cfgs.Update(cfg);
            }
            FileVisitUrl = fileVisitUrl;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SaveConfig
    {
        /// <summary>
        /// 
        /// </summary>
        [BsonId]
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// 配置文件名称
        /// </summary>
        [JsonIgnore]
        public string Name { get; set; }

        /// <summary>
        /// 文件存储路径
        /// </summary>
        public string FileSavePath { get; set; }

        /// <summary>
        /// 可访问的相对路径
        /// </summary>
        public string FileVisitUrl { get; set; }
    }
}
