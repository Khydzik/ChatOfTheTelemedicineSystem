using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GraduateWork.Server.AspNetCore.Extensions.SwaggerExtentions
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();            

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Api V1");
            });
        }

        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "My Api", Version = "v1" });
            });
        }

    }
}
