using FileServer.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace FileServer
{
    /// <summary>
    /// Startup
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
            services.Configure<IISOptions>(o =>
            {
                o.ForwardClientCertificate = false;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // 解决Multipart body length limit 134217728 exceeded
            services.Configure<FormOptions>(u =>
            {
                u.ValueLengthLimit = 2147483647;
                u.MultipartBodyLengthLimit = 2147483647; //2G
            });

            services.AddCors();

            services.AddSwaggerGen(u =>
            {
                u.SwaggerDoc("v1", new Info { Title = "文件服务API", Version = "v1" });
                //添加xml文件
                u.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "FileServer.XML"));

                u.OperationFilter<SwaggerFileUploadFilter>();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // API 跨域访问配置
            app.UseCors(u =>
            {
                u.AllowAnyHeader();
                u.AllowAnyMethod();
                u.AllowAnyOrigin();
            });

            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();
            loggerFactory.AddNLog();
            //NLog.LogManager.LoadConfiguration("NLog.config");
            env.ConfigureNLog("NLog.config");

            app.UseMvc();
            app.UseStaticFiles();

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
