using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Data;
using Utility.EntityFramework.Extensions;
using Utility.Extensions;

namespace Utility.Framework.Test.Datas
{
    public class SysUser : AuditEntityBase<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50), Required]
        public string Account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50), Required]
        public string Password { get; set; }

        public Guid? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual SysRole Role { get; set; }
    }
    public class SysRole : AuditEntityBase<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50), Required]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(150)]
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<SysUser> Users { get; set; }

        public SysRole()
        {
            Users = new List<SysUser>();
        }
    }

    public class UserInfor
    {
        public string Account { get; set; }

        public string Password { get; set; }

        [FromEntity("Name", "Role")]
        public string RoleName { get; set; }
    }
}
