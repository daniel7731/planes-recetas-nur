using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Persons
{
    public interface INutricionistaRepository : IRepository<Nutricionista>
    {

        List<Nutricionista> GetAll();
        Task UpdateAyscn(Nutricionista paciente);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);


    }
}
