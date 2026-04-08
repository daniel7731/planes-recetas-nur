using Nur.Store2025.Observability.Tracing;
using Serilog.Context;
using System.Diagnostics;
namespace PlanesRecetas.webapi.Middleware
{
    public class CorrelationIdMiddleWare(RequestDelegate next)
    {
        private const string CorrelationIdHeader = "X-Correlation-Id";
        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Request.Headers[CorrelationIdHeader].FirstOrDefault( ) ?? Guid.NewGuid().ToString("N")[..8];
            context.Response.Headers[CorrelationIdHeader] = correlationId;  

            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                await next(context);    
            }
        }
    }
}
