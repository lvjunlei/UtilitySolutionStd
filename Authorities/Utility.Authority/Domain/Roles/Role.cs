using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Utility.Authority.Domain.Menus;
using Utility.Authority.Domain.Users;
using Utility.Data;

namespace Utility.Authority.Domain.Roles
{
    /// <summary>
    /// 角色信息
    /// </summary>
    public class Role : AggregateRoot
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 角色分配的用户信息
        /// </summary>
        [NotMapped]
        public IEnumerable<User> Users
        {
            get
            {
                if (UserRoles == null || !UserRoles.Any())
                {
                    return new List<User>();
                }
                return UserRoles.Select(u => u.User);
            }
        }

        /// <summary>
        /// 角色菜单信息
        /// </summary>
        [NotMapped]
        public IEnumerable<Menu> Menus
        {
            get
            {
                if (RoleMenus == null || !RoleMenus.Any())
                {
                    return new List<Menu>();
                }
                return RoleMenus.Select(u => u.Menu);
            }
        }

        /// <summary>
        /// 角色菜单按钮信息
        /// </summary>
        [NotMapped]
        public IEnumerable<MenuButton> MenuButtons
        {
            get
            {
                if (RoleMenuButtons == null || !RoleMenuButtons.Any())
                {
                    return new List<MenuButton>();
                }

                return RoleMenuButtons.Select(u => u.MenuButton);
            }
        }

        /// <summary>
        /// 角色菜单信息集合
        /// </summary>
        public virtual ICollection<RoleMenu> RoleMenus { get; set; }

        /// <summary>
        /// 用户角色集合
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }

        /// <summary>
        /// 角色按钮集合
        /// </summary>
        public virtual ICollection<RoleMenuButton> RoleMenuButtons { get; set; }
    }
}
