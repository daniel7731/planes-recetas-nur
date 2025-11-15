using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Care
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        List<Categoria> GetAll();
        Task<Categoria?> GetByNombreAsync(string nombre);
        Task UpdateAsync(Categoria categoria);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
