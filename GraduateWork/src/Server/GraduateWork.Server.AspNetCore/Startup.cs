using GraduateWork.Server.AspNetCore.Extensions.CorsExtensions;
using GraduateWork.Server.AspNetCore.Extensions.SwaggerExtentions;
using GraduateWork.Server.AspNetCore.Formatters;
using GraduateWork.Server.Data;
using GraduateWork.Server.Data.Models;
using LearningProject.Web.Extensions.RegisterExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GraduateWork.Server.Common.Models;
using Microsoft.AspNetCore.Mvc;
using GraduateWork.Server.AspNetCore.Filters;
using Serilog;
using System.IO;
using Microsoft.Extensions.Logging;
using GraduateWork.Server.AspNetCore.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Owin;


namespace GraduateWork.Server.AspNetCore
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo
                .RollingFile(Path.Combine(hostingEnvironment.ContentRootPath, "log-error.txt")).CreateLogger();

            _configuration = configuration;
        }        

        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddScopedExtensions(_configuration);
            services.AddApiVersioning(r => 
            {
                r.ReportApiVersions = true;
                r.AssumeDefaultVersionWhenUnspecified = true;
                r.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddSwaggerService();
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(
                _configuration.GetConnectionString(Constants.DefaultConnection),
                apt => apt.MigrationsAssembly(typeof(ApplicationContext).Namespace)));
       
            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(GlobalExceptionFilter));
            });
            services.AddIdentity<UserInfoEntity, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationContext>()
               .AddDefaultTokenProviders();
            services.AddTransient<DataSeeder>();
            services.AddOptions();
            services.AddAuthenticationService();
            services.AddCorsService();
            services.AddSignalR();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataSeeder seeder, ILoggerFactory loggerFactory)
        {            
            loggerFactory.AddSerilog();
            app.MigrationOfContext().GetAwaiter().GetResult();
            seeder.Seed().GetAwaiter().GetResult();
            app.AddCorsConfig();          
            app.UseStaticFiles();
            app.UseAuthentication();
            
            app.UseSignalR(router => { router.MapHub<ChatHub>("/chatHub");});
            app.UseMvc();
            app.AddSwaggerConfig();
        }
    }
}
