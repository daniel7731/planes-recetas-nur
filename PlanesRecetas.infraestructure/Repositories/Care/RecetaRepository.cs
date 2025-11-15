using PlanesRecetas.domain.Care;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using PlanesRecetas.infraestructure.Persistence.DomainModel.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Repositories.Care
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
            await  _dbContext.Receta.AddAsync(entity);
        }

        public Task AddIngredientes(Receta receta, List<Ingrediente> ingredientes)
        {
           
            return Task.CompletedTask;

        }

        public Task DeleteAsync(Receta receta)
        {
            throw new NotImplementedException();
        }

        public List<Receta> GetAll()
        {
            throw new NotImplementedException();
            /*var list =  _dbContext.Receta.ToList(); 
            return list;*/
        }

        public Task<Receta?> GetByIdAsync(Guid id, bool readOnly = false)
        {
            throw new NotImplementedException();
        }
        public Task UpdateAsync(Receta receta)
        {
            throw new NotImplementedException();
           // _dbContext.Receta.Update(receta);   
            //return Task.CompletedTask;
        }
    }
}
