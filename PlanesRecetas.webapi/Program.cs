using Joseco.DDD.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlanesRecetas.application.Care;
using PlanesRecetas.application.Medicos;
using PlanesRecetas.application.Pacientes;
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
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DomainDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("PlanesRecetas.infraestructure")
));



// Add services to the container.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePacienteHandler).Assembly));

builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<INutricionistaRepository, NutricionistaRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ITipoAlimentoRepository,TipoAlimentoRepository>();
builder.Services.AddScoped<IUnidadRepository, UnidadRepository>();
builder.Services.AddScoped<IIngredienteRepository, IngredienteRepository>();
builder.Services.AddScoped<ITiempoRepository, TiempoRepository>();
builder.Services.AddScoped<IRecetaRepository, RecetaRepository>();
builder.Services.AddScoped<IDietaRepository, DietaRepository>();
builder.Services.AddScoped<IPlanAlimentacionRepository, PlanAlimentarioRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DomainDbContext>();
        // This applies any pending migrations to the database
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
