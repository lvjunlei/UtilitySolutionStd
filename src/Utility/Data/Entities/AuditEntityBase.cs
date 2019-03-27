#region AuditEntityBase 文件信息
/***********************************************************
**文 件 名：AuditEntityBase 
**命名空间：Cares.Core 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2017/11/14 8:58:18 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.Collections.Generic;
using Utility.Helpers;

namespace Utility.Data
{
    /// <summary>
    /// 带有审计信息的业务基类
    /// 业务实体基类，默认ID为Guid类型
    /// </summary>
    public class AuditEntityBase : AuditEntityBase<Guid>
    {
        public AuditEntityBase()
        {
            Id = GuidHelper.CreateSequentialGuid();
        }
    }

    /// <summary>
    /// 业务实体基类
    /// </summary>
    /// <typeparam name="TKey">实体主键</typeparam>
    public class AuditEntityBase<TKey> : EntityBase<TKey>, ICreate, IUpdate
    {
        /// <summary>
        /// 实体创建时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 实体创建人
        /// 长度设定为 50
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 最后更新人
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 获取Hash值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<TKey>.Default.GetHashCode(Id) * 397) ^ CreatedTime.GetHashCode();
            }
        }

        public AuditEntityBase()
        {
            UpdateTime = CreatedTime = DateTime.Now;
        }
    }
}
