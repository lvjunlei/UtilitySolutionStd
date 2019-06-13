using System;
using Utility.Authority.Domain.Menus;

namespace Utility.Authority.Domain.Roles
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleMenu
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
        public Guid MenuId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Menu Menu { get; set; }
    }
}
