using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.domain.Care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Care
{
    public class GetAllTiemposQueryHandler : IRequestHandler<GetAllTiemposQuery, Result<List<TiempoDto>>>
    {
        private readonly ITiempoRepository _tiempoRepository;

        public GetAllTiemposQueryHandler(ITiempoRepository tiempoRepository)
        {
            _tiempoRepository = tiempoRepository;
        }

        public Task<Result<List<TiempoDto>>> Handle(GetAllTiemposQuery request, CancellationToken cancellationToken)
        {
            var tiempos =  _tiempoRepository.GetAll();
            if (tiempos == null || tiempos.Count == 0)
            {
                return Task.FromResult(Result.Failure<List<TiempoDto>>(Errors.TiemposNotFound));
            }
            var dtoList = tiempos.Select(t => new TiempoDto(t.Id, t.Nombre)).ToList();

            return Task.FromResult(Result.Success(dtoList));
        }
    }
}
