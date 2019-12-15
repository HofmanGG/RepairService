using HelloSocNetw_DAL.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HelloSocNetw_PL.Infrastructure.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<Startup> logger)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<Startup> logger)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (ex is ObjectNotFoundException)
            {
                logger.LogInformation(ex, "Object is not found");
                code = HttpStatusCode.NotFound;
            }

            if (ex is SocketException)
                logger.LogWarning(ex, "Cannot send email");

            if (code == HttpStatusCode.InternalServerError)
                logger.LogError(ex, "Unknown Exception has occurred while executing the request");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(null);
        }
    }
}
