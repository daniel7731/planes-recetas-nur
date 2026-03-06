using Joseco.DDD.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using PlanesRecetas.application.Behaviors;
using PlanesRecetas.application.Messaging;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.application.Pacientes.Evento;
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
            config.RegisterServicesFromAssembly(typeof(PublishPlanCreated).Assembly);
            config.RegisterServicesFromAssembly(typeof(PublishIngredienteCreated).Assembly);
            config.RegisterServicesFromAssembly(typeof(PacienteCreated).Assembly);
        });
        return services;
    }
}
