using Joseco.DDD.Core.Abstractions;
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
    public class CreatePacienteHandler : IRequestHandler<CreatePacienteComand, Result<Guid>>
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreatePacienteHandler(IPacienteRepository pacienteRepository, IUnitOfWork unitOfWork)
        {
            _pacienteRepository = pacienteRepository;
            _unitOfWork = unitOfWork;
        }

        async Task<Result<Guid>> IRequestHandler<CreatePacienteComand, Result<Guid>>.Handle(CreatePacienteComand request, CancellationToken cancellationToken)
        {
           // throw new NotImplementedException();
           var paciente = new Paciente(request.Guid,request.Nombre, request.Apellido, 
               request.FechaNacimiento, request.Peso, request.Altura);
           await _pacienteRepository.AddAsync(paciente);
           await _unitOfWork.CommitAsync(cancellationToken);
           return Result.Success(paciente.Id);
        }
       
    }
}
