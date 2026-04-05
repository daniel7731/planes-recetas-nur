using Humanizer.Configuration;
using Joseco.DDD.Core.Abstractions;
using Joseco.Outbox.Contracts.Service;
using Joseco.Outbox.EFCore;
using Joseco.Outbox.EFCore.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PlanesRecetas.domain.Care;
using PlanesRecetas.domain.Metrics;
using PlanesRecetas.domain.Persons;
using PlanesRecetas.domain.Plan;
using PlanesRecetas.domain.Recipe;
using PlanesRecetas.infraestructure.Persistence;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using PlanesRecetas.infraestructure.Repositories.Care;
using PlanesRecetas.infraestructure.Repositories.Metrics;
using PlanesRecetas.infraestructure.Repositories.Persons;
using PlanesRecetas.infraestructure.Repositories.Plan;
using PlanesRecetas.infraestructure.Repositories.Recipe;


namespace PlanesRecetas.infraestructure.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
      
        services.Configure<DataBaseSettings>(
           
         

            configuration.GetSection("DataBaseSettings")
        );
        services.AddDbContext<DomainDbContext>((sp, options) =>
        {
            // Use IOptions to get the settings
            var settings = sp.GetRequiredService<IOptions<DataBaseSettings>>().Value;
            options.UseNpgsql(settings.ConnectionString);
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IDatabase, DomainDbContext>()
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
    

        return services;
    }
}
