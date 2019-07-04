using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GraduateWork.Server.AspNetCore.Extensions.CorsExtensions
{
    public static class CorsExtension
    {
        public static void AddCorsConfig(this IApplicationBuilder app)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
        }

        public static void AddCorsService(this IServiceCollection services)
        {
            services.AddCors();
        }

    }
}
