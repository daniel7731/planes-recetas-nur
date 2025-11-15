using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Care
{
    public interface ITipoAlimentoRepository : IRepository<TipoAlimento>
    {
        List<TipoAlimento> GetAll();
        Task<TipoAlimento?> GetByIdAsync(int id);
        Task<TipoAlimento?> GetByNameAsync(string nombre);
        Task<bool> ExistsAsync(string nombre);
    }
}
