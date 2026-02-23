using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.application.Care;
using PlanesRecetas.domain.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Recipe
{
    public sealed class GetAllIngredientesQueryHandler
       : IRequestHandler<GetAllIngredientesQuery, Result<List<IngredienteDto>>>
    {
        private readonly IIngredienteRepository _ingredienteRepository;

        public GetAllIngredientesQueryHandler(IIngredienteRepository ingredienteRepository)
        {
            _ingredienteRepository = ingredienteRepository;
        }

        public Task<Result<List<IngredienteDto>>> Handle(GetAllIngredientesQuery request, CancellationToken cancellationToken)
        {
            var ingredientes = _ingredienteRepository.GetAll();

            if (ingredientes == null || ingredientes.Count == 0)
                return Task.FromResult(Result.Failure<List<IngredienteDto>>(Errors.IngredientesNotFound));
            var list = ingredientes.Select(x => new IngredienteDto
            {
                Calorias = x.Calorias,
                CategoriaId = x.CategoriaId,
                Id = x.Id,
                Nombre = x.Nombre,
                UnidadId = x.UnidadId

            }).ToList();

            return Task.FromResult(Result.Success(list));
        }
    }
}
