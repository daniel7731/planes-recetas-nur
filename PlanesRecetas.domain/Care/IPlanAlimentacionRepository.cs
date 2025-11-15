using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Care
{
    public interface IPlanAlimentacionRepository : IRepository<PlanAlimentacion>
    {
        List<PlanAlimentacion> GetAll();
        Task<PlanAlimentacion?> GetByPacienteIdAsync(Guid pacienteId);
        Task<PlanAlimentacion?> GetByNutricionistaIdAsync(Guid nutricionistaId);
        Task UpdateAsync(PlanAlimentacion plan);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
