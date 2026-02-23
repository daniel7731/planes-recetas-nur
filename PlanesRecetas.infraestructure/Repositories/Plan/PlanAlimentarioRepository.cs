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
        public Task<PlanAlimentacion?> GetByPacienteIdAsync(Guid pacienteId)
        {
            return Task.FromResult(_dbContext.PlanAlimentacion.FirstOrDefault(i => i.PacienteId== pacienteId));
        }


        public Task UpdateAsync(PlanAlimentacion plan)
        {
            _dbContext.PlanAlimentacion.Update(plan);
            return Task.CompletedTask;
        }
    }
}
