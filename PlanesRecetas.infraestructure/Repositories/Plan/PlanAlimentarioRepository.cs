using Microsoft.EntityFrameworkCore;
using PlanesRecetas.domain.Plan;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Repositories.Plan
{
    public class PlanAlimentarioRepository : IPlanAlimentacionRepository
    {
        private readonly DomainDbContext _dbContext;
        public PlanAlimentarioRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(PlanAlimentacion entity)
        {
            await _dbContext.PlanAlimentacion.AddAsync(entity);
        }
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public List<PlanAlimentacion> GetAll()
        {
            return _dbContext.PlanAlimentacion.ToList();
        }
        public Task<PlanAlimentacion?> GetByIdAsync(Guid id, bool readOnly = false)
        {
            return Task.FromResult(_dbContext.PlanAlimentacion.FirstOrDefault(i => i.Id == id));
        }

    

        public Task<PlanAlimentacion?> GetByNutricionistaIdAsync(Guid nutricionistaId)
        {
            return Task.FromResult(_dbContext.PlanAlimentacion.FirstOrDefault(i => i.NutricionistaId == nutricionistaId));
        }
        public Task<List<PlanAlimentacion>> GetByPacienteIdAsync(Guid pacienteId)
        {
            return Task.FromResult(
                _dbContext.PlanAlimentacion
                .Include(p => p.Paciente)
                .Include(p => p.Nutricionista)
                .Include(p => p.Dietas)
                .Where(i => i.PacienteId == pacienteId).ToList());
        }

        public async Task<List<DietaReceta>> GetRecetasByPlanIdAsync(Guid planId)
        {
            return await _dbContext.DietaReceta.
                 Include(d => d.Dieta)
                .Include(d => d.Receta)
                .Include(d => d.Tiempo)
                .Where(r => r.Dieta.PlanAlimentacionId == planId).ToListAsync();
        }

        public Task<List<PlanAlimentacion>> SearchPacienteIdAsync(Guid pacienteId, DateTime FechaInicio, bool readOnly = false)
        {
            
            var list = _dbContext.PlanAlimentacion.Include(p => p.Paciente
               ).Include(p => p.Nutricionista)
               .Include(p => p.Dietas).Where(p => p.PacienteId == pacienteId && p.FechaInicio >= FechaInicio).ToList();

            return Task.FromResult(list);
        }

        public Task UpdateAsync(PlanAlimentacion plan)
        {
            _dbContext.PlanAlimentacion.Update(plan);
            return Task.CompletedTask;
        }
    }
}
