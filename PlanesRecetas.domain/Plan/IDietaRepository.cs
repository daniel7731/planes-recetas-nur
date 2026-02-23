using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Plan
{
    public interface IDietaRepository : IRepository<Dieta>
    {
        List<Dieta> GetDietasPlan(Guid planId);
      
        Task UpdateAsync(Dieta dieta);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task AddDietaReceta(Dieta dieta, DietaReceta dietaReceta);

        List<DietaReceta> GetDietaRecetas(Guid dietaId);
    }
}
