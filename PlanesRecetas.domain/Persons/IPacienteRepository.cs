using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Persons
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        List<Paciente> GetAll();
        Task<Paciente?> GetByEmailAsync(string email);
        Task UpdateAyscn(Paciente paciente);
        Task<bool> ExistsAsync(Guid id);
       
    }

}
