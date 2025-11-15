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
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DomainDbContext _dbContext;

        public CategoriaRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Categoria entity)
        {
           await _dbContext.Categoria.AddAsync(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Categoria> GetAll()
        {
            return _dbContext.Categoria.ToList();
        }

        public async Task<Categoria?> GetByIdAsync(Guid id, bool readOnly = false)
        {
            return await _dbContext.Categoria.SingleAsync(x => x.Id == id);
        }

        public Task<Categoria?> GetByNombreAsync(string nombre)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Categoria categoria)
        {
            throw new NotImplementedException();
        }
    }
}
