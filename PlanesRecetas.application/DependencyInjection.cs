using Joseco.DDD.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using PlanesRecetas.application.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace PlanesRecetas.application;

public static class DependencyInjection
{
    //agregar los productores aqui
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
        });
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(PublishPlanCreated).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(PublishIngredienteCreated).Assembly);
        });
  

        return services;
    }
}
