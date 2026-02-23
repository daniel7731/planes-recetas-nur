using Microsoft.EntityFrameworkCore;
using PlanesRecetas.domain.Care;
using PlanesRecetas.domain.Metrics;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Repositories.Metrics
{
    public class UnidadRepository : IUnidadRepository
    {
        private readonly DomainDbContext _dbContext;

        public UnidadRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(UnidadMedida entity)
        {
            _dbContext.UnidadMedida.Add(entity);
            return Task.CompletedTask;
        }

        public List<UnidadMedida> GetAll()
        {
            return _dbContext.UnidadMedida.AsNoTracking()
                       .OrderBy(x => x.Nombre)
                       .ToList();
        }

        public Task<UnidadMedida?> GetByIdAsync(Guid id, bool readOnly = false)
        {
            return null;
        }

        public async Task<UnidadMedida?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _dbContext.UnidadMedida.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<UnidadMedida?> GetUnidad(int id)
        {
            return await _dbContext.UnidadMedida
                .AsNoTracking()                
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task UpdateAsync(UnidadMedida unidad, CancellationToken ct = default)
        {
            _dbContext.UnidadMedida.Update(unidad);
            return Task.CompletedTask;
        }
    }
}
