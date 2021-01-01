using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Context;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    public class RequestLoggingMiddleware
    {
        readonly RequestDelegate next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            RegisterRequestId(context);

            var requestBody = await ReadRequestBodyAsync(context);

            Log.ForContext("RequestHeaders", context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
               .ForContext("RequestBody", requestBody)
               .Debug(context.Request.Method + " " + context.Request.Path);

            await next(context);
        }

        private void RegisterRequestId(HttpContext context)
        {
            string requestId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(requestId))
            {
                requestId = Guid.NewGuid().ToString();
            }

            LogContext.PushProperty("RequestId", requestId);
        }

        private async Task<string> ReadRequestBodyAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            var body = context.Request.Body;
            var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
            await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
            var textBody = Encoding.UTF8.GetString(buffer);

            body.Seek(0, SeekOrigin.Begin);
            context.Request.Body = body;

            return textBody;
        }
    }
}
