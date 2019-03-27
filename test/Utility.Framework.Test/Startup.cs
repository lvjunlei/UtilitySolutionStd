using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utility.Data;
using Utility.EntityFramework;
using Utility.EntityFramework.Extensions;
using Utility.Framework.Test.Datas;

namespace Utility.Framework.Test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IMasterDbContext, MasterDbContext>();
            services.AddScoped<ISlaveDbContext, SlaveDbContext>();
            services.AddScoped<IUnitOfWork<IMasterDbContext>, UnitOfWork<IMasterDbContext>>();
            services.AddScoped<IUnitOfWork<ISlaveDbContext>, UnitOfWork<ISlaveDbContext>>();
            services.AddUnitOfWork();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
