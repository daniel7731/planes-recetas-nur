using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Care
{
    public interface IRecetaRepository : IRepository<Receta>
    {
        List<Receta> GetAll();
        Task UpdateAsync(Receta receta);
        Task DeleteAsync(Receta receta);
        Task AddIngredientes(Receta receta, List<Ingrediente> ingredientes);
    }
}
