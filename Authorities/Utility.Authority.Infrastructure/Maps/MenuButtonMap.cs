using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utility.Authority.Domain.Menus;

namespace Utility.Authority.Infrastructure.Maps
{
    public class MenuButtonMap : IEntityTypeConfiguration<MenuButton>
    {
        public void Configure(EntityTypeBuilder<MenuButton> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(u => u.Code)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(u => u.Description)
                .HasMaxLength(150);
        }
    }
}
