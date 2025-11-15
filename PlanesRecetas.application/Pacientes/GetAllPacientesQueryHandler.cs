using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.application.Medicos;
using PlanesRecetas.application.Pacientes.PlanesRecetas.application.Persons;
using PlanesRecetas.domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Pacientes
{
    public class GetAllPacientesQueryHandler
        : IRequestHandler<GetAllPacientesQuery, Result<List<PacienteDto>>>
    {
        private readonly IPacienteRepository _pacienteRepository;

        public GetAllPacientesQueryHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public Task<Result<List<PacienteDto>>> Handle(GetAllPacientesQuery request, CancellationToken cancellationToken)
        {
            // Repository GetAll() is synchronous
            var pacientes = _pacienteRepository.GetAll();
            if (pacientes == null || pacientes.Count == 0)
                return Task.FromResult(Result.Failure<List<PacienteDto>>(Errors.PacientesNotFound));
            var list = pacientes.Select(p => new PacienteDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                FechaNacimiento = p.FechaNacimiento,
                Peso = p.Peso,
                Altura = p.Altura
            }).ToList();
            return Task.FromResult(Result.Success(list));
        }
    }
}
