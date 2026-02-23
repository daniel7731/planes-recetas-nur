using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Metrics
{
    public interface IUnidadMedidaRepository : IRepository<UnidadMedida>
    {
        List<UnidadMedida> GetAll();
        Task<UnidadMedida?> GetByNombreAsync(string nombre);
        Task UpdateAsync(UnidadMedida unidadMedida);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);

    }
}
