using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PlanesRecetas.application;
using PlanesRecetas.infraestructure;
using PlanesRecetas.infraestructure.Extensions;
using PlanesRecetas.webapi;
using PlanesRecetas.webapi.Extensions;
using Prometheus;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
string serviceName = "planesrecetas.api";
//builder.Host.UseLogging(serviceName, builder.Configuration);
builder.Host.UseSerilog((ctx, services, config)
    => config.ReadFrom.Configuration(ctx.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithProperty("Application", serviceName));

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy(), tags: ["ready"]);

builder.Services.AddAplication()
.AddInfrastructure(builder.Configuration, builder.Environment, serviceName)
.AddPresentation(builder.Configuration, builder.Environment)
.AddConsulServiceDiscovery(builder.Configuration, builder.Environment);


var app = builder.Build();
app.ApplyMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
}

app.UseRouting();

app.UseHealthChecks();

app.UseRequestCorrelationId();

app.UseRequestContextLogging();
app.UseHttpMetrics();
//app.UseExceptionHandler();
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("live")
});
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("ready"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapMetrics();

app.Run();