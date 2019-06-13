using System;
using System.Collections.Generic;
using System.Text;
using Utility.Authority.Domain.Menus;

namespace Utility.Authority.Domain.Roles
{
    public class RoleMenuButton
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MenuButtonId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual MenuButton MenuButton { get; set; }
    }
}
