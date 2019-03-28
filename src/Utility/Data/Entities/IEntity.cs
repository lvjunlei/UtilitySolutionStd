#region IEntityBase 文件信息
/***********************************************************
**文 件 名：IEntityBase 
**命名空间：Utility.Framework 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2017/12/26 14:11:11 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;

namespace Utility.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey> : IEquatable<IEntity<TKey>>
    {
        TKey Id { get; set; }
    }
}
