#region Singleton 文件信息
/***********************************************************
**文 件 名：Singleton 
**命名空间：Utility 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2017/12/6 10:39:38 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;

namespace Utility
{
    /// <summary>
    /// 获取类型单例
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    public static class Singleton<TService> where TService : class, new()
    {
        private static TService _instance;

        /// <summary>
        /// 获取单例
        /// </summary>
        /// <returns></returns>
        public static TService GetInstance()
        {
            if (_instance == null)
            {
                System.Threading.Interlocked.CompareExchange(ref _instance, Activator.CreateInstance<TService>(), default(TService));
            }

            return _instance;
        }
    }
}
