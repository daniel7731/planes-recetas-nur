using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.application.Care;
using PlanesRecetas.domain.Care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Care
{
    public sealed class GetAllCategoriasQueryHandler
        : IRequestHandler<GetAllCategoriasQuery, Result<List<CategoriaDto>>>
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public GetAllCategoriasQueryHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public Task<Result<List<CategoriaDto>>> Handle(GetAllCategoriasQuery request, CancellationToken cancellationToken)
        {
            var categorias = _categoriaRepository.GetAll();
            if (categorias == null || categorias.Count == 0)
                return Task.FromResult(Result.Failure<List<CategoriaDto>>(Errors.CategoriesNotFound));
            var list = categorias.Select(x => new CategoriaDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                TipoAlimentoId = x.TipoAlimentoId
            }).ToList();
            return Task.FromResult(Result.Success(list));
        }
    }
}
