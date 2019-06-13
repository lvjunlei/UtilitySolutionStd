using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Utility.Authority.Domain.Deptments;
using Utility.Authority.Domain.Roles;
using Utility.Data;

namespace Utility.Authority.Domain.Users
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class User : AggregateRoot
    {
        #region 属性

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; private set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool IsAdmin { get; private set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActived { get; private set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabled { get; private set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 登录失败次数
        /// </summary>
        public int LoginFailureTimes { get; private set; }

        /// <summary>
        /// 上一次登录失败时间
        /// </summary>
        public DateTime? LastLoginFailureTime { get; private set; }

        /// <summary>
        /// 上次修改密码时间
        /// </summary>
        public DateTime? LastAlterPasswordTime { get; private set; }

        /// <summary>
        /// 所属部门ID
        /// </summary>
        public Guid? DepartmentId { get; private set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public virtual Department Department { get; private set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        [NotMapped]
        public IEnumerable<Role> Roles
        {
            get
            {
                if (UserRoles == null || !UserRoles.Any())
                {
                    return new List<Role>();
                }

                return UserRoles.Select(u => u.Role);
            }
        }

        /// <summary>
        /// 用户角色集合
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数，给EF使用
        /// </summary>
        protected User()
        {
            IsActived = false;
            IsEnabled = false;
            LoginFailureTimes = 0;
            LastAlterPasswordTime = null;
            LastLoginFailureTime = null;
            Department = null;
            UserRoles = new List<UserRole>();
        }

        /// <summary>
        /// 初始化用户信息
        /// </summary>
        /// <param name="name">用户姓名</param>
        /// <param name="account">用户账号</param>
        /// <param name="isAdmin">是否管理员，默认false</param>
        public User(string name, string account, bool isAdmin = false)
            : this()
        {
            Name = name;
            Account = account;
            IsAdmin = isAdmin;
            Password = "";
        }

        #endregion

        #region 方法

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        public void AlterPassword(string oldPassword, string newPassword)
        {
            if (oldPassword != Password)
            {
                throw new Exception("当前密码不正确");
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                throw new Exception("新密码不能为空");
            }

            Password = newPassword;
        }

        /// <summary>
        /// 激活用户信息
        /// </summary>
        public void ActiveUser()
        {
            IsActived = true;
        }

        /// <summary>
        /// 设置账号不可用
        /// </summary>
        public void DisEnabledUser()
        {
            IsEnabled = false;
        }

        /// <summary>
        /// 设置账号可用
        /// </summary>
        public void EnabledUser()
        {
            IsEnabled = true;
        }

        #endregion
    }
}
