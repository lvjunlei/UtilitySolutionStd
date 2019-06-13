using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utility.Authority.Domain.Roles;

namespace Utility.Authority.Infrastructure.Maps
{
    public class RoleMenuButtonMap : IEntityTypeConfiguration<RoleMenuButton>
    {
        public void Configure(EntityTypeBuilder<RoleMenuButton> builder)
        {
            builder.HasKey(u => new { u.RoleId, u.MenuButtonId });

            builder.HasOne(u => u.Role)
                .WithMany(u => u.RoleMenuButtons)
                .HasForeignKey(u => u.RoleId);
            builder.HasOne(u => u.MenuButton)
                .WithMany(u => u.RoleMenuButtons)
                .HasForeignKey(u => u.MenuButtonId);
        }
    }
}
