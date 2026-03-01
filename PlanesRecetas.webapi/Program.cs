using Nur.Store2025.Observability;
using PlanesRecetas.application;
using PlanesRecetas.infraestructure;
using PlanesRecetas.webapi;
using PlanesRecetas.webapi.Extensions;

var builder = WebApplication.CreateBuilder(args);
string serviceName = "planesrecetas.api";
builder.Host.UseLogging(serviceName, builder.Configuration);
builder.Services.AddAplication()
    .AddInfrastructure(builder.Configuration, builder.Environment, serviceName)
    .AddPresentation(builder.Configuration, builder.Environment);


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
    app.ApplyMigrations();
}

app.UseRouting();

app.UseHealthChecks();

app.UseRequestCorrelationId();

app.UseRequestContextLogging();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();