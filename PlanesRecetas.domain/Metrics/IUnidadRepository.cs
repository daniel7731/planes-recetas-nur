using Joseco.DDD.Core.Abstractions;
using PlanesRecetas.domain.Care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Metrics
{
    public interface IUnidadRepository : IRepository<Unidad>
    {
   
        List<Unidad> GetAll();
        Task<Unidad?> GetUnidad(int id);
    }
}
