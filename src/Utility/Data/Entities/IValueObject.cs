#region IValueObject 文件信息
/***********************************************************
**文 件 名：IValueObject 
**命名空间：Utility.Data.Entities 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-28 15:20:45 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;

namespace Utility.Data.Entities
{
    /// <summary>
    /// 值对象定义
    /// </summary>
    public interface IValueObject : IEquatable<IValueObject>
    {
    }
}
