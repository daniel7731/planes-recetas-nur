using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.application.Medicos;
using PlanesRecetas.domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Pacientes
{
    public class GetPacienteByIdQueryHandler
        : IRequestHandler<GetPacienteByIdQuery, Result<PacienteDto>>
    {
        private readonly IPacienteRepository _pacienteRepository;

        public GetPacienteByIdQueryHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<Result<PacienteDto>> Handle(GetPacienteByIdQuery request, CancellationToken cancellationToken)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(request.Id, request.ReadOnly);
            if (paciente is null)
                return Result.Failure<PacienteDto>(Errors.PacientesNotFound);
            PacienteDto dto = new PacienteDto
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                FechaNacimiento = paciente.FechaNacimiento,
                Peso = paciente.Peso,
                Altura = paciente.Altura
            };


            return Result.Success(dto);
        }
    }
}
