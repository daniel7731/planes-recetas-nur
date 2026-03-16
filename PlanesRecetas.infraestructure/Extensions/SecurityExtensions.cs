using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlanesRecetas.infraestructure.Extensions;

namespace PlanesRecetas.infraestructure.Extensions;

public static class SecurityExtensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services, IHostEnvironment environment)
    {
        if (environment is IWebHostEnvironment)
        {
            /* var jwtOptions = services.BuildServiceProvider().GetRequiredService<JwtOptions>();
             services.AddSecurity(jwtOptions, CatalogPermission.PermissionsList);*/
        }
        return services;
    }
}
