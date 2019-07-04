using GraduateWork.Server.Core;
using GraduateWork.Server.Data;
using Microsoft.Extensions.DependencyInjection;
using GraduateWork.Server.AspNetCore.Token;
using System.IdentityModel.Tokens.Jwt;
using GraduateWork.Server.Common.Models;
using Microsoft.Extensions.Configuration;
using GraduateWork.Server.Common;
using GraduateWork.Server.AspNetCore.Hubs;

namespace LearningProject.Web.Extensions.RegisterExtensions
{
    public static class RegisterExtensions
    {
        public static void AddScopedExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthOptions>(configuration.GetSection(Constants.Section));
            services.AddScoped<ApplicationContext,ApplicationContext>();
            services.AddScoped<IUserService, UserService>(); 
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ChatHub, ChatHub>();
            services.AddScoped<ITokenFormation, TokenFormation>();
            services.AddScoped<JwtSecurityTokenHandler, JwtSecurityTokenHandler>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
