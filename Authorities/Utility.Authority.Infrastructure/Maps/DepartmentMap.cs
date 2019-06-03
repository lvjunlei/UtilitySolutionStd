using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utility.Authority.Domain.Roles;

namespace Utility.Authority.Infrastructure.Maps
{
    public class DepartmentMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(u => u.Code)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
