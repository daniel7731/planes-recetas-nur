using PlanesRecetas.application;
using PlanesRecetas.application.Messaging;
using PlanesRecetas.application.Pacientes.Evento;
using PlanesRecetas.infraestructure;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using PlanesRecetas.webapi;
using PlanesRecetas.webapi.Extensions;

var builder = WebApplication.CreateBuilder(args);
string serviceName = "planesrecetas.api";
//builder.Host.UseLogging(serviceName, builder.Configuration);



builder.Services.AddAplication()
    .AddInfrastructure(builder.Configuration, builder.Environment, serviceName)
    .AddPresentation(builder.Configuration, builder.Environment);


var app = builder.Build();
if (args.Contains("--migrate"))
{
    app.ApplyMigrations();
    return; // Exit container after migration
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
}

app.UseRouting();

app.UseHealthChecks();

//app.UseRequestCorrelationId();

//app.UseRequestContextLogging();

//app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();