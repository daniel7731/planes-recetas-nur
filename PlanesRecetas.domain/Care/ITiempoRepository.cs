using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Care
{
    public interface  ITiempoRepository : IRepository<Tiempo>
    {
        List<Tiempo> GetAll();
        Task<Tiempo?> GetByNombreAsync(string nombre);
        Task UpdateAsync(Tiempo tiempo);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<Tiempo?> GetById(int id);

    }
}
