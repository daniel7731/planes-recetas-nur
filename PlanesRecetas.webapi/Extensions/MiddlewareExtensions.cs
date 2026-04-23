using PlanesRecetas.webapi.Middleware;
using Serilog;
namespace PlanesRecetas.webapi.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseRequestCorrelationId(this IApplicationBuilder app)
    {
        app.UseMiddleware<CorrelationIdMiddleWare>();
        return app;
    }

    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
    {
      
        app.UseSerilogRequestLogging(opts =>
            opts.MessageTemplate = "HTTP {RequestMethod} {RequestPath} -> {StatusCOde} ({Elapsed:0.0ms})"
        );
       

        return app;
    }
}