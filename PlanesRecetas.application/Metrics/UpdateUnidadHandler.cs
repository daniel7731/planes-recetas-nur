using Joseco.DDD.Core.Abstractions;
using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.domain.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Metrics
{
    public class UpdateUnidadHandler : IRequestHandler<UpdateUnidadMedidaComand, Result<int>>
{
    private readonly IUnidadRepository _unidadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUnidadHandler(
        IUnidadRepository unidadRepository,
        IUnitOfWork unitOfWork)
    {
        _unidadRepository = unidadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(
        UpdateUnidadMedidaComand request,
        CancellationToken cancellationToken)
    {
        var unidad = await _unidadRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (unidad is null)
            return Result.Failure<int>(Errors.UnidadNotFound);

        unidad.Update(request.Nombre, request.Simbolo);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success(unidad.Id);
    }
}
}
