using System;
using Utility.Authority.Domain.Users;

namespace Utility.Authority.Domain.Roles
{
    public class UserRole
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Role Role { get; set; }
    }
}
