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

        public Task AddAsync(Unidad entity)
        {
            _dbContext.Unidad.Add(entity);
            return Task.CompletedTask;
        }

        public List<Unidad> GetAll()
        {
            return _dbContext.Unidad.AsNoTracking()
                       .OrderBy(x => x.Nombre)
                       .ToList();
        }

        public Task<Unidad?> GetByIdAsync(Guid id, bool readOnly = false)
        {
            return null;
        }

        public async Task<Unidad?> GetUnidad(int id)
        {
            return await _dbContext.Unidad
                .AsNoTracking()                
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
