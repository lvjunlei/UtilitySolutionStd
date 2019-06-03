using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Utility.Authority.Domain.Roles;
using Utility.Data;

namespace Utility.Authority.Domain.Menus
{
    /// <summary>
    /// 菜单按钮信息
    /// </summary>
    public class MenuButton : EntityBase<int>
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 按钮编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 所属菜单ID
        /// </summary>
        public Guid MenuId { get; set; }

        /// <summary>
        /// 按钮所属菜单信息
        /// </summary>
        public virtual Menu Menu { get; set; }

        /// <summary>
        /// 按钮分配的角色
        /// </summary>
        [NotMapped]
        public IEnumerable<Role> Roles
        {
            get
            {
                if (RoleMenuButtons == null || !RoleMenuButtons.Any())
                {
                    return new List<Role>();
                }
                return RoleMenuButtons.Select(u => u.Role);
            }
        }

        /// <summary>
        /// 角色按钮集合
        /// </summary>
        public virtual ICollection<RoleMenuButton> RoleMenuButtons { get; set; }
    }
}
