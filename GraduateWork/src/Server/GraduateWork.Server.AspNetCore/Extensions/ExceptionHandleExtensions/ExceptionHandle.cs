using GraduateWork.Server.Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GraduateWork.Server.AspNetCore.Extensions.ExceptionHandleExtensions
{
    public static class ExceptionHandle
    {
        public static void AddExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {                    
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 404;
                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    if (ex != null)
                    {
                        var response = new ResponseError<object>()
                        {
                            Result = null,
                            Error = new Error()
                            {
                                Id = Constants.Validation,
                                Message = ex.Error.Message
                            }
                        };
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
                    }
                });
            });
        }
    }
}
