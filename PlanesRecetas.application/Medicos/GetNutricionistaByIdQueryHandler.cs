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
    public class GetNutricionistaByIdQueryHandler
       : IRequestHandler<GetNutricionistaByIdQuery, Result<NutricionistaDto>>
    {
        private readonly INutricionistaRepository _nutricionistaRepository;

        public GetNutricionistaByIdQueryHandler(INutricionistaRepository nutricionistaRepository)
        {
            _nutricionistaRepository = nutricionistaRepository;
        }

        public async Task<Result<NutricionistaDto>> Handle(GetNutricionistaByIdQuery request, CancellationToken cancellationToken)
        {
            var nutricionista = await _nutricionistaRepository.GetByIdAsync(request.Id, request.ReadOnly);
            if (nutricionista is null)
                return Result.Failure<NutricionistaDto>(Errors.NutricionistaNotFound);
            var single = new NutricionistaDto
            {
                Id = nutricionista.Id,
                Nombre = nutricionista.Nombre,
                FechaCreacion = nutricionista.FechaCreacion,
                Activo = nutricionista.Activo
            };
            return Result.Success(single);
        }
    }
}
