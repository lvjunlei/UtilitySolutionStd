#region ApplicationBuilderExtensions 文件信息

/***********************************************************
**文 件 名：ApplicationBuilderExtensions
**命名空间：Utility.EntityFramework.Extensions
**内     容：
**功     能：
**文件关系：
**作     者：LvJunlei
**创建日期：2019-02-27 16:27:20
**版 本 号：V1.0.0.0
**修改日志：
**版权说明：
************************************************************/

#endregion ApplicationBuilderExtensions 文件信息


namespace Utility.EntityFramework.Extensions
{
    /// <summary>
    /// IApplicationBuilderExtensions
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 使用 主-从（读-写 分离）数据库模式
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseUseMasterSlaveDatabase(this IApplicationBuilder app)
        {
            Configuration.UseMasterSlaveDatabase();
            return app;
        }

        /// <summary>
        /// 使用一主多从（一夫多妻制）数据库模式
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMultiSlaveStrategy(this IApplicationBuilder app)
        {
            Configuration.UseMultiSlaveStrategy();
            return app;
        }
    }
}