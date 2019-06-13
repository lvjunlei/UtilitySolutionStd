using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Utility.Authority.Domain.Roles;
using Utility.Data;

namespace Utility.Authority.Domain.Menus
{
    /// <summary>
    /// 菜单信息
    /// </summary>
    public class Menu : AggregateRoot
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// 菜单链接地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 所属系统编码
        /// </summary>
        public string System { get; set; }

        /// <summary>
        /// 是否叶子节点
        /// </summary>
        public bool IsLeaf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// 父菜单信息
        /// </summary>
        public virtual Menu Parent { get; set; }

        /// <summary>
        /// 子菜单信息
        /// </summary>
        public virtual ICollection<Menu> Chidren { get; set; }

        /// <summary>
        /// 按钮集合
        /// </summary>
        public virtual ICollection<MenuButton> MenuButtons { get; set; }

        /// <summary>
        /// 菜单分配的角色集合
        /// </summary>
        [NotMapped]
        public IEnumerable<Role> Roles
        {
            get
            {
                if (RoleMenus == null || !RoleMenus.Any())
                {
                    return new List<Role>();
                }
                return RoleMenus.Select(u => u.Role);
            }
        }

        /// <summary>
        /// 角色菜单集合
        /// </summary>
        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
    }
}
