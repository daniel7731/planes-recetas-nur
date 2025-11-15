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
    public sealed class GetAllUnidadesQueryHandler
        : IRequestHandler<GetAllUnidadesQuery, Result<List<UnidadDto>>>
    {
        private readonly IUnidadRepository _unidadRepository;

        public GetAllUnidadesQueryHandler(IUnidadRepository unidadRepository)
        {
            _unidadRepository = unidadRepository;
        }

        public Task<Result<List<UnidadDto>>> Handle(GetAllUnidadesQuery request, CancellationToken cancellationToken)
        {
            var unidades = _unidadRepository.GetAll();

            if (unidades == null || unidades.Count == 0)
                return Task.FromResult(Result.Failure<List<UnidadDto>>(Errors.UnidadesNotFound));

            var list = unidades.Select(x => new UnidadDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Simbolo = x.Simbolo
            }).ToList();

            return Task.FromResult(Result.Success(list));
        }
    }
}
