using Joseco.DDD.Core.Abstractions;
using Joseco.DDD.Core.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanesRecetas.domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Pacientes
{
    public class CreatePacienteHandler : IRequestHandler<CreatePacienteComand, Result<Guid>>
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreatePacienteHandler> _logger;
        public CreatePacienteHandler(IPacienteRepository pacienteRepository, IUnitOfWork unitOfWork , ILogger<CreatePacienteHandler> logger)
        {
            _pacienteRepository = pacienteRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        async Task<Result<Guid>> IRequestHandler<CreatePacienteComand, Result<Guid>>.Handle(CreatePacienteComand request, CancellationToken cancellationToken)
        {
            // throw new NotImplementedException();
            var paciente = new Paciente(request.Guid, request.Nombre, request.Apellido,
                request.FechaNacimiento, request.Peso, request.Altura);

            try
            {
                await _pacienteRepository.AddAsync(paciente);
                await _unitOfWork.CommitAsync(cancellationToken);

                _logger.LogInformation("Creating paciente with id {PacienteId}", paciente.Id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating paciente with id {PacienteId}", paciente.Id);
                return Result.Failure<Guid>(Errors.PacienteNotCreated);
            }
          

            return Result.Success(paciente.Id);
        }

    }
}
