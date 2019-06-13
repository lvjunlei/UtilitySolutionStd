using System.Collections.Generic;
using Utility.Authority.Domain.Users;
using Utility.Data;

namespace Utility.Authority.Domain.Deptments
{
    /// <summary>
    /// 部门信息
    /// </summary>
    public class Department : AggregateRoot
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 部门角色
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}
