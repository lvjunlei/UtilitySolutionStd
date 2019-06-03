using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Data
{
    /// <summary>
    /// 用户信息接口定义
    /// </summary>
    public interface IUser : IEntity<Guid>
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        string Account { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        string RoleName { get; set; }
    }
}
