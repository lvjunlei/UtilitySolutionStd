using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Utility.Authority.Domain.Users;
using Utility.Data;
using Utility.EntityFramework.Extensions;

namespace Utility.Authority.Infrastructure
{
    public class MySqlDbContext : DbContext, IDbContext
    {
        public MySqlDbContext(DbContextOptions<MySqlDbContext> optins)
            : base(optins)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
