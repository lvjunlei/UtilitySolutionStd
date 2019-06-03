using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Authority.Domain.Roles;

namespace Utility.Authority.Infrastructure.Maps
{
    public class RoleMenuMap : IEntityTypeConfiguration<RoleMenu>
    {
        public void Configure(EntityTypeBuilder<RoleMenu> builder)
        {
            builder.HasKey(u => new { u.RoleId, u.MenuId });

            builder.HasOne(u => u.Role)
                .WithMany(u => u.RoleMenus)
                .HasForeignKey(u => u.RoleId);
            builder.HasOne(u => u.Menu)
                .WithMany(u => u.RoleMenus)
                .HasForeignKey(u => u.MenuId);
        }
    }
}
