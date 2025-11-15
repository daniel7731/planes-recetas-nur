using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Medicos
{
    public class GetAllNutricionistasQueryHandler
        : IRequestHandler<GetAllNutricionistasQuery, Result<List<NutricionistaDto>>>
    {
        private readonly INutricionistaRepository _nutricionistaRepository;

        public GetAllNutricionistasQueryHandler(INutricionistaRepository nutricionistaRepository)
        {
            _nutricionistaRepository = nutricionistaRepository;
        }

        public Task<Result<List<NutricionistaDto>>> Handle(GetAllNutricionistasQuery request, CancellationToken cancellationToken)
        {
            var nutricionistas = _nutricionistaRepository.GetAll();
            if (nutricionistas == null || nutricionistas.Count == 0)
                return Task.FromResult(Result.Failure<List<NutricionistaDto>>(Errors.NutricionistasNotFound));
            var  list = nutricionistas.Select(n => new NutricionistaDto
            {
                Id = n.Id,
                Nombre = n.Nombre,
                FechaCreacion = n.FechaCreacion,
                Activo = n.Activo
            }).ToList();
            return Task.FromResult(Result.Success(list));
        }
    }
}
