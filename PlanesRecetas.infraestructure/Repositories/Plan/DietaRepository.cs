using Microsoft.EntityFrameworkCore;
using PlanesRecetas.domain.Plan;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Repositories.Plan
{
    public class DietaRepository : IDietaRepository
    {
        private readonly DomainDbContext _dbContext;
        public DietaRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Dieta entity)
        {
            await _dbContext.Dieta.AddAsync(entity);
        }
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Dieta> GetAll()
        {
            return _dbContext.Dieta.ToList();
        }

        public Task<Dieta?> GetByIdAsync(Guid id, bool readOnly = false)
        {
            return Task.FromResult(_dbContext.Dieta.FirstOrDefault(i => i.Id == id));
        }



        public Task UpdateAsync(Dieta dieta)
        {
            _dbContext.Dieta.Update(dieta);
            return Task.CompletedTask;
        }

        public async Task AddDietaReceta(Dieta dieta, DietaReceta dietaReceta)
        {
            await _dbContext.DietaReceta.AddAsync(dietaReceta);
        }
    }
}
