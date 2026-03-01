using Joseco.Communication.External.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        services.AddMediatR(config =>
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
            );
        services.AddSecrets(configuration, environment)
           .AddObservability(environment, serviceName)
           .AddDatabase()
           .AddSecurity(environment)
           .AddRabbitMQ(environment);


        return services;
    }

}
