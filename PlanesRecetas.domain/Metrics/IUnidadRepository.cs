using Joseco.DDD.Core.Abstractions;
using PlanesRecetas.domain.Care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Metrics
{
    public interface IUnidadRepository : IRepository<UnidadMedida>
    {
   
        List<UnidadMedida> GetAll();
        Task<UnidadMedida?> GetUnidad(int id);
        Task AddAsync(UnidadMedida unidad);

        Task<UnidadMedida?> GetByIdAsync(int id, CancellationToken ct = default);
        Task UpdateAsync(UnidadMedida unidad, CancellationToken ct = default);
    }
}
