using Joseco.DDD.Core.Abstractions;
using Joseco.Outbox.Contracts.Service;
using Joseco.Outbox.EFCore;
using Joseco.Outbox.EFCore.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using PlanesRecetas.domain.Care;
using PlanesRecetas.domain.Metrics;
using PlanesRecetas.domain.Persons;
using PlanesRecetas.domain.Plan;
using PlanesRecetas.domain.Recipe;
using PlanesRecetas.infraestructure.Persistence;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using PlanesRecetas.infraestructure.Persistence.Repositories;
using PlanesRecetas.infraestructure.Repositories.Care;
using PlanesRecetas.infraestructure.Repositories.Metrics;
using PlanesRecetas.infraestructure.Repositories.Persons;
using PlanesRecetas.infraestructure.Repositories.Plan;
using PlanesRecetas.infraestructure.Repositories.Recipe;


namespace PlanesRecetas.infraestructure.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<StoredDbContext>((sp, options) => {
            var settings = sp.GetRequiredService<DataBaseSettings>();
            options.UseSqlServer(settings.ConnectionString);
        });

        services.AddDbContext<DomainDbContext>((sp, options) => {
            var settings = sp.GetRequiredService<DataBaseSettings>();
            options.UseSqlServer(settings.ConnectionString);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IDatabase, StoredDbContext>()
            .AddScoped<IPacienteRepository, PacienteRepository>()
            .AddScoped<INutricionistaRepository, NutricionistaRepository>()
            .AddScoped<ICategoriaRepository, CategoriaRepository>()
            .AddScoped<ITipoAlimentoRepository, TipoAlimentoRepository>()
            .AddScoped<IUnidadRepository, UnidadRepository>()
            .AddScoped<IIngredienteRepository, IngredienteRepository>()
            .AddScoped<ITiempoRepository, TiempoRepository>()
            .AddScoped<IRecetaRepository, RecetaRepository>()
            .AddScoped<IDietaRepository, DietaRepository>()
            .AddScoped<IPlanAlimentacionRepository, PlanAlimentarioRepository>()
            .AddScoped<IOutboxDatabase<DomainEvent>, UnitOfWork>()    // AddScoped<IOutboxDatabase<DomainEvent>, OutboxDatabase>() // or UnitOfWork
            .AddOutbox<DomainEvent>();
            // Scoped Out Servicio inyectado
            //.AddOutboxBackgroundService<DomainEvent>(5000);

        /***
         
          builder.Services
            .AddScoped<IOutboxDatabase<DomainEvent>, OutboxDatabase>() // or UnitOfWork
            .AddOutbox<DomainEvent>();
         */
        services.Decorate<IOutboxService<DomainEvent>, OutboxTracingService<DomainEvent>>();

        return services;
    }
}
