using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Utility.Authority.Domain.Users;
using Utility.Authority.Infrastructure;
using Utility.Commands;
using Utility.Data;
using Utility.EntityFramework;
using Utility.Extensions;

namespace Utility.Authority.Test
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionStrings:Default"];
            services.AddDbContext<IDbContext, MySqlDbContext>(o =>
            {
                o.UseMySQL(connectionString);
            });
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IUnitOfWork<MySqlDbContext>, UnitOfWork<MySqlDbContext>>();

            services.AddScoped<IUnitOfWork>(u => new UnitOfWork(u.GetService<IDbContext>()));
            services.AddSingleton<ICommandHandlerFactory, CommandHandlerFactory>();
            services.AddSingleton<ICommandBus, CommandBus>();
            //services.AddScoped<ICommandHandler<AddUserCommand>, UserCommandHandler>();

            services.Scan(scan => scan
                .FromAssemblies(typeof(UserCommandHandler).GetTypeInfo().Assembly)
                    .AddClasses(classes => classes.Where(x =>
                    {
                        var allInterfaces = x.GetInterfaces();
                        return allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(ICommandHandler<>));
                    }))
                    .AsSelf()
                    .WithTransientLifetime()
            );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDynamicWebApi();

            #region Swagger

            services.AddSwaggerGen(options =>
            {

                string contactName = Configuration.GetSection("SwaggerDoc:ContactName").Value;
                string contactNameEmail = Configuration.GetSection("SwaggerDoc:ContactEmail").Value;
                string contactUrl = Configuration.GetSection("SwaggerDoc:ContactUrl").Value;
                options.SwaggerDoc("v1", new Info
                {
                    Version = Configuration.GetSection("SwaggerDoc:Version").Value,
                    Title = Configuration.GetSection("SwaggerDoc:Title").Value,
                    Description = Configuration.GetSection("SwaggerDoc:Description").Value,
                    Contact = new Contact { Name = contactName, Email = contactNameEmail, Url = contactUrl },
                    License = new License { Name = contactName, Url = contactUrl }
                });

                // 使用动态WebAPI必须要返回true
                options.DocInclusionPredicate((docName, description) =>
                {
                    return true;
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Utility.Authority.Test.xml");
                options.IncludeXmlComments(xmlPath);
                //options.DocumentFilter<HiddenApiFilter>(); // 在接口类、方法标记属性 [HiddenApi]，可以阻止【Swagger文档】生成
                options.OperationFilter<AddHeaderOperationFilter>("correlationId", "Correlation Id for the request", false); // adds any string you like to the request headers - in this case, a correlation id
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                options.OperationFilter<SecurityRequirementsOperationFilter>();
                //给api添加token令牌证书
                //options.AddSecurityDefinition("oauth2", new SecurityScheme
                //{
                //    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                //    //Name = "Authorization",//jwt默认的参数名称
                //    //In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                //    Type = SecuritySchemeType.ApiKey
                //});
            });

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwagger(u => { u.RouteTemplate = "/swagger/{documentName}/swagger.json"; });
            app.UseSwaggerUI(u =>
            {
                u.SwaggerEndpoint("v1/swagger.json", "文件服务API V1");
                //加载汉化的js文件，注意 swagger.js文件属性必须设置为“嵌入的资源”。
                u.InjectJavascript("/Scripts/swagger.js");
            });
        }
    }
}
