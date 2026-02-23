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
    public sealed class GetAllUnidadesMedidaQueryHandler
        : IRequestHandler<GetAllUnidadesMedidaQuery, Result<List<UnidadMedidaDto>>>
    {
        private readonly IUnidadRepository _unidadRepository;

        public GetAllUnidadesMedidaQueryHandler(IUnidadRepository unidadRepository)
        {
            _unidadRepository = unidadRepository;
        }

        public Task<Result<List<UnidadMedidaDto>>> Handle(GetAllUnidadesMedidaQuery request, CancellationToken cancellationToken)
        {
            var unidades = _unidadRepository.GetAll();

            if (unidades == null || unidades.Count == 0)
                return Task.FromResult(Result.Failure<List<UnidadMedidaDto>>(Errors.UnidadesNotFound));

            var list = unidades.Select(x => new UnidadMedidaDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Simbolo = x.Simbolo
            }).ToList();

            return Task.FromResult(Result.Success(list));
        }
    }
}
