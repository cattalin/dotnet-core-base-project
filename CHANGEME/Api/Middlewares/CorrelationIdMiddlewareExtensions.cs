using Microsoft.AspNetCore.Builder;

namespace Api.Middlewares
{
    public static class CorrelationIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogRequestId(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorrelationIdMiddleware>();
        }
    }
}

