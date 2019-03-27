#region ICompareable 文件信息
/***********************************************************
**文 件 名：ICompareable 
**命名空间：Utility 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2017/12/18 11:28:38 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


namespace Utility
{
    /// <summary>
    /// 对象比较接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IComparable<in T>
    {
        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="obj">要比较的对象</param>
        /// <returns></returns>
        bool GreaterThan(T obj);
    }
}
