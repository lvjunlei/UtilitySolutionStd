#region IUpdateBase 文件信息
/***********************************************************
**文 件 名：IUpdateBase 
**命名空间：Utility.Data 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2018/7/23 16:28:07 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

namespace Utility.Data
{
    /// <summary>
    /// 要修改实体的基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IUpdateBase<TKey> : IEntity<TKey>
    {
    }
}
