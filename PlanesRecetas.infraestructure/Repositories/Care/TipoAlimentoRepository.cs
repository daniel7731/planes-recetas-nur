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
    public class TipoAlimentoRepository : ITipoAlimentoRepository
    {
        private readonly DomainDbContext _dbContext;
       

        public TipoAlimentoRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(TipoAlimento entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _dbContext.TipoAlimento.AddAsync(entity);
        }

        public async Task<TipoAlimento?> GetByIdAsync(int id)
        {
            var single= await _dbContext.TipoAlimento.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return single;
        }

        public List<TipoAlimento> GetAll()
        {
            return _dbContext.TipoAlimento.AsNoTracking()
                        .OrderBy(x => x.Nombre)
                        .ToList();
        }

        public async Task<TipoAlimento?> GetByNameAsync(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return null;

            return await _dbContext.TipoAlimento.AsNoTracking()
                              .FirstOrDefaultAsync(x => x.Nombre == nombre);
        }

        public async Task<bool> ExistsAsync(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return false;

            return await _dbContext.TipoAlimento.AsNoTracking()
                              .AnyAsync(x => x.Nombre == nombre);
        }

        public Task<TipoAlimento?> GetByIdAsync(Guid id, bool readOnly = false)
        {
            throw new NotImplementedException();
        }
    }
}
