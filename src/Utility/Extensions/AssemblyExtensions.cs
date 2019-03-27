using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Utility.Extensions
{
    /// <summary>
    /// AssemblyExtensions
    /// </summary>
    public static class AssemblyExtensions
    {
        #region GetFileVersion(获取程序集的文件版本号)
        /// <summary>
        /// 获取程序集的文件版本号
        /// </summary>
        /// <param name="assembly">Assembly</param>
        /// <returns>程序集文件版本号</returns>
        public static Version GetFileVersion(this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            var info = FileVersionInfo.GetVersionInfo(assembly.Location);
            return new Version(info.FileVersion);
        }
        #endregion

        #region GetProductVersion(获取程序集的产品版本)
        /// <summary>
        /// 获取程序集的产品版本
        /// </summary>
        /// <param name="assembly">Assembly</param>
        /// <returns>程序集产品版本</returns>
        public static Version GetProductVersion(this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            var info = FileVersionInfo.GetVersionInfo(assembly.Location);
            return new Version(info.ProductVersion);
        }
        #endregion

        #region MyRegion

        /// <summary>
        /// 获取实现了接口类型 T 的类型集合
        /// </summary>
        /// <typeparam name="T">接口类型T</typeparam>
        /// <param name="assembly">Assembly</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypes<T>(this Assembly assembly)
        {
            if (!typeof(T).IsInterface)
            {
                throw new ArgumentException($"类型 {typeof(T)} 不是接口（interface）类型！");
            }
            var rs = from t in assembly.GetTypes()
                     where typeof(T).IsAssignableFrom(t) && t.IsClass
                     select t;

            return rs;
        }

        #endregion
    }
}
