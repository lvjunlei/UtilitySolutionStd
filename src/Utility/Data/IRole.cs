using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Data
{
    /// <summary>
    /// 用户信息定义
    /// </summary>
    public interface IRole : IEntity<Guid>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        string RoleName { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        string RoleCode { get; set; }
    }
}
