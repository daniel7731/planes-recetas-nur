using Inventory.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nur.Store2025.Observability;
using PlanesRecetas.infraestructure.Extensions;
using System.Reflection;


namespace PlanesRecetas.infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment,
        string serviceName)
    {
         services.AddSecrets(configuration, environment)
        .AddObservability(environment, serviceName)
        .AddDatabase()
        .AddSecurity(environment)
        .AddRabbitMQ(configuration,environment);


        return services;
    }

}
