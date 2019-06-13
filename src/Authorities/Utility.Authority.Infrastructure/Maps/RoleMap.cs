using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utility.Authority.Domain.Roles;

namespace Utility.Authority.Infrastructure.Maps
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(u => u.Code)
                .HasMaxLength(50);
            builder.Property(u => u.Description)
                .HasMaxLength(150);
        }
    }
}
