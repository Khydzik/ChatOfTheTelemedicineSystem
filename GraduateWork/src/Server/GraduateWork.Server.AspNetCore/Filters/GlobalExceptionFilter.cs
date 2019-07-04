using GraduateWork.Server.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace GraduateWork.Server.AspNetCore.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                var (error, statuscode) = PrepareResponseForException(context.Exception);
                context.ExceptionHandled = true;
                context.Result = new ObjectResult(error)
                {
                    StatusCode = (int)statuscode
                };
            }
        }

        private (ResponseError<object>, HttpStatusCode) PrepareResponseForException(Exception ex)
        {
            ResponseError<object> error;
            HttpStatusCode statusCode;

            switch(ex)
            {
                case ArgumentException argumentException:
                    statusCode = HttpStatusCode.NotFound;
                    _logger?.LogError(ex, $"NotFound Error: {ex.Message}");
                    error = GetResponseError(argumentException);
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    error = GetResponseError(ex);
                    _logger?.LogCritical(ex, $"REST API Internal Server Error: {ex.Message}");
                    break;
            }

            return (error, statusCode);
        }

        private ResponseError<object> GetResponseError(Exception exception)
        {
            var error = new ResponseError<object>()
            {
                Result = null,
                Error = new Error()
                {
                    Id = Constants.Validation,
                    Message = exception.Message
                }
            };
            return error;
        }
    }
}
