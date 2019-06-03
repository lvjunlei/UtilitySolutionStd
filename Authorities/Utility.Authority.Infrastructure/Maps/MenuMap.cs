using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utility.Authority.Domain.Menus;

namespace Utility.Authority.Infrastructure.Maps
{
    public class MenuMap : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(u => u.Code)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(u => u.Url)
                .HasMaxLength(150);
            builder.Property(u => u.System)
                .HasMaxLength(50);

            builder.HasMany(u => u.Chidren)
                .WithOne(u => u.Parent)
                .HasForeignKey(u => u.ParentId);

            builder.HasMany(u => u.MenuButtons)
                .WithOne(u => u.Menu)
                .HasForeignKey(u => u.MenuId);
        }
    }
}
