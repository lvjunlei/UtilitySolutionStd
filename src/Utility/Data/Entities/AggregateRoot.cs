#region AggregateRoot 文件信息
/***********************************************************
**文 件 名：AggregateRoot 
**命名空间：Utility.Framework 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2017/12/26 14:08:53 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;

namespace Utility.Data
{
    public class AggregateRoot : IAggregateRoot
    {
        #region Object 成员

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var ar = obj as IAggregateRoot;

            return ar != null && Id.Equals(ar.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

        #region IAggregateRoot 成员

        public Guid Id
        {
            get;
            set;
        }

        #endregion
    }
}
