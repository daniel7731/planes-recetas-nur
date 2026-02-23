using PlanesRecetas.domain.Recipe;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Repositories.Recipe
{
    public class IngredienteRepository : IIngredienteRepository
    {
        private readonly DomainDbContext _dbContext;
        public IngredienteRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Ingrediente entity)
        {
           await _dbContext.Ingrediente.AddAsync(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Ingrediente> GetAll()
        {
            return _dbContext.Ingrediente.ToList();
        }

        public Task<Ingrediente?> GetByIdAsync(Guid id, bool readOnly = false)
        {
            return Task.FromResult(_dbContext.Ingrediente.FirstOrDefault(i => i.Id == id));
        }

        public Task<Ingrediente?> GetByNombreAsync(string nombre)
        {
            return Task.FromResult(_dbContext.Ingrediente.FirstOrDefault(i => i.Nombre == nombre));
        }

       

        public Task UpdateAsync(Ingrediente ingrediente)
        {
            _dbContext.Ingrediente.Update(ingrediente); 
            return Task.CompletedTask;
        }
        
    }
}
