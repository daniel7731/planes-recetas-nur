using Microsoft.EntityFrameworkCore;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.domain.Persons;
using PlanesRecetas.infraestructure.Messaging;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using PlanesRecetas.infraestructure.Repositories.Persons;
using PlanesRecetasWorkerService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.Configure<RabbitMQSettings>(
    builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddDbContext<DomainDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddSingleton<IRabbitMQConnection, RabbitMQConnection>();
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var rabbit = scope.ServiceProvider.GetRequiredService<IRabbitMQConnection>()
        as RabbitMQConnection;

    await rabbit!.InitializeAsync();
}

await app.RunAsync();


