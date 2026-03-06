using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using PlanesRecetas.infraestructure.Persistence.DomainModel;

namespace PlanesRecetas.infraestructure.Persistence.Design;

internal class DbContextFactory : IDesignTimeDbContextFactory<DomainDbContext>
{
    public DomainDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        DataBaseSettings dataBaseSettings = new();
        configuration.Bind("DataBaseSettings", dataBaseSettings);
        var connectionString = dataBaseSettings.ConnectionString;

        var optionsBuilder = new DbContextOptionsBuilder<DomainDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new DomainDbContext(optionsBuilder.Options);
    }

    private IConfiguration BuildConfiguration()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables();

        // Aquí puedes agregar lógica condicional para consultar Vault si se requiere

        return builder.Build();
    }
}
