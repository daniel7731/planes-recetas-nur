using Microsoft.EntityFrameworkCore;
using PlanesRecetas.infraestructure.Persistence;

namespace PlanesRecetas.webapi.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using var db =
            scope.ServiceProvider.GetRequiredService<IDatabase>();

        db.Migrate();


    }
}
