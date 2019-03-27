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
    /// 业务实体基类
    /// </summary>
    /// <typeparam name="TKey">实体主键</typeparam>
    public class EntityBase<TKey> : IEntityBase<TKey>
    {
        /// <summary>
        /// 实体ID
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 相等比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is IEntityBase<TKey>))
            {
                return false;
            }

            var entity = (IEntityBase<TKey>)obj;

            return entity.Id.Equals(Id);
        }

        /// <summary>
        /// 相等比较
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(AuditEntityBase<TKey> other)
        {
            return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
        }

        /// <summary>
        /// 获取Hash值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<TKey>.Default.GetHashCode(Id) * 397);
            }
        }

        /// <summary>
        /// 重写方法 实体比较 ==
        /// </summary>
        /// <param name="a">领域实体a</param>
        /// <param name="b">领域实体b</param>
        /// <returns></returns>
        public static bool operator ==(EntityBase<TKey> a, EntityBase<TKey> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }
        /// <summary>
        /// 重写方法 实体比较 !=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(EntityBase<TKey> a, EntityBase<TKey> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 输出领域对象的状态
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }

    /// <summary>
    /// 业务实体基类，默认ID为Guid类型
    /// </summary>
    public class EntityBase : EntityBase<Guid>
    {
        public EntityBase()
        {
            Id = GuidHelper.CreateSequentialGuid();
        }
    }
}
