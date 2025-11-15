using Microsoft.EntityFrameworkCore;
using PlanesRecetas.domain.Persons;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Repositories.Persons
{
    public class NutricionistaRepository : INutricionistaRepository
    {
        private readonly DomainDbContext _dbContext;
        public NutricionistaRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Nutricionista entity)
        {
            await _dbContext.Nutricionista.AddAsync(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return Task.FromResult(_dbContext.Nutricionista.Any(x => x.Id == id));
        }

        public List<Nutricionista> GetAll()
        {
            return _dbContext.Nutricionista.OrderBy(x => x.Nombre).ToList();
        }

        public async Task<Nutricionista?> GetByIdAsync(Guid id, bool readOnly = false)
        {
            var single = await _dbContext.Nutricionista.FirstOrDefaultAsync(x => x.Id == id);
            

            return single;
        }

        public Task UpdateAyscn(Nutricionista nutricionista)
        {
            _dbContext.Nutricionista.Update(nutricionista);
            // SaveChanges happens in UnitOfWork
            return Task.CompletedTask;
        }
    }
}
