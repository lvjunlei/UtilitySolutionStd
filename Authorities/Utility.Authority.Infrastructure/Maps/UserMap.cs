using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utility.Authority.Domain.Users;

namespace Utility.Authority.Infrastructure.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(u => u.Account)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(u => u.Password)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(u => u.Description)
                .HasMaxLength(150);

            builder.HasOne(u => u.Department)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.DepartmentId);
        }
    }
}
