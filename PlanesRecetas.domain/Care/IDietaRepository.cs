using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Care
{
    public interface IDietaRepository : IRepository<Dieta>
    {
        List<Dieta> GetAll();
        Task<Dieta?> GetByNombreAsync(string nombre);
        Task UpdateAsync(Dieta dieta);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
