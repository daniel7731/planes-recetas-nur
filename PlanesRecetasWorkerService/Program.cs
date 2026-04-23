using Joseco.DDD.Core.Abstractions;
using Joseco.Outbox.EFCore;
using Nur.Store2025.Observability;
using PlanesRecetas.application;
using PlanesRecetas.application.Pacientes.Evento;
using PlanesRecetas.infraestructure;
using PlanesRecetas.infraestructure.Consumer;


var builder = Host.CreateApplicationBuilder(args);

string serviceName = "planes-recetas.worker-service";

Console.WriteLine("Building Host...");
builder.UseLogging(serviceName, builder.Configuration);
builder.Services.AddAplication()
.AddInfrastructure(builder.Configuration, builder.Environment, serviceName);
builder.Services.AddOutboxBackgroundService<DomainEvent>();
Console.WriteLine("Host Built. Starting Host...");
var host = builder.Build();
await host.StartAsync();
Console.WriteLine("Host Started Successfully!");
await host.WaitForShutdownAsync();
