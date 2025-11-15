using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Care
{
    public interface IIngredienteRepository : IRepository<Ingrediente>
    {
        List<Ingrediente> GetAll();
        Task<Ingrediente?> GetByNombreAsync(string nombre);
        Task UpdateAsync(Ingrediente ingrediente);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
       


    }
}
