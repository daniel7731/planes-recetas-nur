using Joseco.DDD.Core.Abstractions;
using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.domain.Care;
using PlanesRecetas.domain.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Metrics
{
    public class CreateUnidadHandler : IRequestHandler<CreateUnidadMedidaComand, Result<int>>
    {
        private readonly IUnidadRepository _unidadRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUnidadHandler() { }
        public CreateUnidadHandler(IUnidadRepository unidadRepository, IUnitOfWork unitOfWork)
        {
            _unidadRepository = unidadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateUnidadMedidaComand request, CancellationToken cancellationToken)
        {
            var unidad = new domain.Metrics.UnidadMedida(request.Id, request.Nombre, request.Simbolo);

            await _unidadRepository.AddAsync(unidad);
            await _unitOfWork.CommitAsync(cancellationToken);

            
            return Result.Success(unidad.Id);
        }
    }
}
