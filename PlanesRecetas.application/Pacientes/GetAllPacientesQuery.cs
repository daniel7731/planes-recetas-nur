using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Pacientes
{
    namespace PlanesRecetas.application.Persons
    {
        public record GetAllPacientesQuery() : IRequest<Result<List<PacienteDto>>>;
    }
}
