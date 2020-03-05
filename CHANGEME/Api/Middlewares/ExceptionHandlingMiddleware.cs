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
                var timestamp = DateTime.UtcNow;
                Log.Error(ex, $"Unknown Error Caught at {timestamp}");
                await HandleExceptionAsync(context, ex, timestamp);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, DateTime timestamp)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Message = "Internal Server Error",
                Timestamp = timestamp,
                Exception = exception.Message
            }));
        }
    }
}

