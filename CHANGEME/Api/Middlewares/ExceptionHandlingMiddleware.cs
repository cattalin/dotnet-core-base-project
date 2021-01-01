using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"{ex.GetType().Name} Caught at {DateTime.UtcNow}");
                await RespondToException(context, HttpStatusCode.InternalServerError, "Internal Server Error", ex);
            }
        }

        private static Task RespondToException(HttpContext context, HttpStatusCode failureStatusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)failureStatusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Message = message,
                Timestamp = DateTime.UtcNow,
            }));
        }

        private static Task RespondToException(HttpContext context, HttpStatusCode failureStatusCode, string message, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)failureStatusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Message = message,
                Details = exception.Message,
                Type = exception.GetType().Name,
                Timestamp = DateTime.UtcNow,
            }));
        }
    }
}

