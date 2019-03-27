using Microsoft.EntityFrameworkCore;
using Utility.Data;

namespace Utility.Framework.Test.Datas
{
    public class SlaveDbContext : DbContext, ISlaveDbContext
    {
        public DbSet<SysUser> SysUsers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=TestDb2;user=root;password=root;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<User>().HasIndex(u => u.Aaccount).IsUnique();
        }
    }
}
