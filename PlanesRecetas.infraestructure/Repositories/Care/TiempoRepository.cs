using Microsoft.EntityFrameworkCore;
using PlanesRecetas.domain.Care;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Repositories.Care
{
    public class TiempoRepository : ITiempoRepository
    {
        private readonly DomainDbContext _dbContext;
        public TiempoRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Tiempo entity)
        {
           await _dbContext.Tiempo.AddAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            _dbContext.Tiempo.Remove(new Tiempo { Id = id });   
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(int id)
        {
           bool tiempo = _dbContext.Tiempo.Any(x => x.Id == id);
           return Task.FromResult(tiempo);
        }

        public List<Tiempo> GetAll()
        {
            return _dbContext.Tiempo.AsNoTracking()
                     .OrderBy(x => x.Nombre)
                     .ToList();
        }

        public Task<Tiempo?> GetById(int id)
        {
            return _dbContext.Tiempo.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Tiempo?> GetByIdAsync(Guid id, bool readOnly = false)
        {
          
            return null;
        }

        public Task<Tiempo?> GetByNombreAsync(string nombre)
        {
            var single =  _dbContext.Tiempo.FirstOrDefaultAsync(x => x.Nombre == nombre);
            return single;
        }

        public Task UpdateAsync(Tiempo tiempo)
        {
            _dbContext.Tiempo.Update(tiempo);   
            return Task.CompletedTask;
        }
    }
}
