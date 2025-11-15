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
    public class DeletePacienteCommandHandler : IRequestHandler<DeletePacienteCommand, Result<Guid>>
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePacienteCommandHandler(IPacienteRepository pacienteRepository, IUnitOfWork unitOfWork)
        {
            _pacienteRepository = pacienteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeletePacienteCommand request, CancellationToken cancellationToken)
        {
            // check if exists before deleting
            bool exists = await _pacienteRepository.ExistsAsync(request.Id);
            if (!exists)
                return Result.Failure<Guid>(Error.None);

            await _pacienteRepository.DeleteAsync(request.Id);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success(request.Id);
        }
    }
}
