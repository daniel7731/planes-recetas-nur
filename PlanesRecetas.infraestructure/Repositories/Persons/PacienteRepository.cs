namespace PlanesRecetas.infraestructure.Repositories.Persons
{
    using Microsoft.EntityFrameworkCore;
    using PlanesRecetas.domain.Persons;
    using PlanesRecetas.infraestructure.Persistence.DomainModel;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PacienteRepository : IPacienteRepository
    {
        private readonly DomainDbContext _dbContext;
        public PacienteRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Paciente entity)
        {
           await _dbContext.Paciente.AddAsync(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            var entity = _dbContext.Paciente.FirstOrDefault(x => x.Id == id);
            if (entity!= null) {
                _dbContext.Paciente.Remove(entity);
            }
            return Task.CompletedTask;
           
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return Task.FromResult( _dbContext.Paciente.Any(x => x.Id == id));
        }

        public List<Paciente> GetAll()
        {
            return _dbContext.Paciente.OrderBy(x => x.Apellido).ToList();
        }

        public Task<Paciente?> GetByEmailAsync(string email)
        {
            return _dbContext.Paciente.FirstOrDefaultAsync(x => x.Email != null && x.Email.ToLower() == email.ToLower());
        }

        public async Task<Paciente?> GetByIdAsync(Guid id, bool readOnly = false)
        {
            var single = await _dbContext.Paciente.FirstOrDefaultAsync(x=> x.Id == id);
            return single;
        }

        public Task UpdateAyscn(Paciente paciente)
        {
            _dbContext.Paciente.Update(paciente);
            // SaveChanges happens in UnitOfWork
            return Task.CompletedTask;
        }
    }
}
