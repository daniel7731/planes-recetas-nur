using k8s.KubeConfigModels;
using Microsoft.EntityFrameworkCore;
using PlanesRecetas.domain.Recipe;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using PlanesRecetas.infraestructure.Persistence.DomainModel.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PlanesRecetas.infraestructure.Repositories.Recipe
{
    public class RecetaRepository : IRecetaRepository
    {
        private readonly DomainDbContext _dbContext;
        public RecetaRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Receta entity)
        {
            // throw new NotImplementedException();
            await _dbContext.Receta.AddAsync(entity);
        }

        public Task AddIngredientes(Receta receta, List<RecetaIngrediente> ingredientes)
        {
            // _dbContext.    
            ingredientes.ForEach(ingrediente =>
            {
                _dbContext.RecetaIngrediente.Add(ingrediente);
            });
            return Task.CompletedTask;

        }

        public Task DeleteAsync(Receta receta)
        {
            throw new NotImplementedException();
        }

        public List<Receta> GetAll()
        {
            return _dbContext.Receta.Include(r => r.Tiempo).
                ToList();   
        }

        public async Task<Receta?> GetByIdAsync(Guid id, bool readOnly = false) { 
        
            return await _dbContext.Receta.Include(r => r.Tiempo).SingleAsync(r => r.Id == id);
        }
        public Task UpdateAsync(Receta receta)
        {
            throw new NotImplementedException();
            // _dbContext.Receta.Update(receta);   
            //return Task.CompletedTask;
        }
    }
}
