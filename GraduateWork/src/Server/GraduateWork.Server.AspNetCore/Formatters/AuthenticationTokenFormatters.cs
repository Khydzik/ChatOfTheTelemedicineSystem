using GraduateWork.Server.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GraduateWork.Server.AspNetCore.Formatters
{
    public static class AuthenticationTokenFormatters
    {
        private static IOptions<AuthOptions> _authOptions { get; }
        public static void AddAuthenticationService(this IServiceCollection services)
        {
            using (var buildServiceProvide = services.BuildServiceProvider())
            {
                var authOption = buildServiceProvide.GetRequiredService<IOptions<AuthOptions>>();

                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {

                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {

                        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                        ValidateIssuer = true,
                        ValidIssuer = authOption.Value.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = authOption.Value.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = authOption.Value.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });
            }
        }
    }
}