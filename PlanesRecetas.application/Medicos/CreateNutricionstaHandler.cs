using Joseco.DDD.Core.Abstractions;
using Joseco.DDD.Core.Results;
using MediatR;
using MediatR.Pipeline;
using PlanesRecetas.domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Medicos
{
    public class CreateNutricionstaHandler : IRequestHandler<CreateNutricionistaComand, Result<Guid>>
    {
        private readonly INutricionistaRepository _nutricionistaRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateNutricionstaHandler(INutricionistaRepository nutricionistaRepository, IUnitOfWork unitOfWork)
        {
            _nutricionistaRepository = nutricionistaRepository;
            _unitOfWork = unitOfWork;
        }

        async Task<Result<Guid>> IRequestHandler<CreateNutricionistaComand, Result<Guid>>.Handle(CreateNutricionistaComand request, CancellationToken cancellationToken)
        {
            var nutricionista = new Nutricionista(request.Guid, request.Nombre,request.Activo,request.FechaCreacion);
            await _nutricionistaRepository.AddAsync(nutricionista);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Success(nutricionista.Id);
        }
    }
}
