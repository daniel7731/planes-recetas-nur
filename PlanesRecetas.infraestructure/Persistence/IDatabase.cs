namespace PlanesRecetas.infraestructure.Persistence;

public interface IDatabase : IDisposable
{
    void Migrate();
}
