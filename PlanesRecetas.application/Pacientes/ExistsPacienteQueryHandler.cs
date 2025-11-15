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
    public class ExistsPacienteQueryHandler : IRequestHandler<ExistsPacienteQuery, Result<bool>>
    {
        private readonly IPacienteRepository _pacienteRepository;

        public ExistsPacienteQueryHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<Result<bool>> Handle(ExistsPacienteQuery request, CancellationToken cancellationToken)
        {
            bool exists = await _pacienteRepository.ExistsAsync(request.Id);
            return Result.Success(exists);
        }
    }
}
