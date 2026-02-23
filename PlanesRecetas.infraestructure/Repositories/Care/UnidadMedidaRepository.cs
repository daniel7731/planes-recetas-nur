using PlanesRecetas.domain.Metrics;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Repositories.Care
{
    public class UnidadMedidaRepository : IUnidadMedidaRepository
    {
        private readonly DomainDbContext _dbContext;
        public UnidadMedidaRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(UnidadMedida entity)
        {
            await _dbContext.UnidadMedida.AddAsync(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<UnidadMedida> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UnidadMedida?> GetByIdAsync(Guid id, bool readOnly = false)
        {
            throw new NotImplementedException();
        }

        public Task<UnidadMedida?> GetByNombreAsync(string nombre)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UnidadMedida unidadMedida)
        {
            throw new NotImplementedException();
        }
    }
}
