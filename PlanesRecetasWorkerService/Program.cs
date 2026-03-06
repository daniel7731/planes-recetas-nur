using Joseco.DDD.Core.Abstractions;
using Joseco.Outbox.EFCore;
using Microsoft.Extensions.DependencyInjection;
using Nur.Store2025.Observability;
using PlanesRecetas.application;
using PlanesRecetas.application.Pacientes.Evento;
using PlanesRecetas.infraestructure;
using PlanesRecetas.infraestructure.Consumer;

var builder = Host.CreateApplicationBuilder(args);

string serviceName = "planes-recetas.worker-service";

builder.UseLogging(serviceName, builder.Configuration);

builder.Services.AddAplication()
.AddInfrastructure(builder.Configuration, builder.Environment, serviceName);
builder.Services.AddHostedService<RabbitTopicWorker<PacienteCreated>>();
builder.Services.AddOutboxBackgroundService<DomainEvent>();

var host = builder.Build();
host.Run();
