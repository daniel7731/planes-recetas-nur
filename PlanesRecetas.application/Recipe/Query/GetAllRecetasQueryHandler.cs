using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.domain.Recipe;
using PlanesRecetas.domain.Recipe.Query;


namespace PlanesRecetas.application.Recipe.Query
{
    public class GetAllRecetasQueryHandler : IRequestHandler<GetAllRecetasQuery, Result<List<RecetaDto>>>
    {
        private readonly IRecetaRepository recetaRepository;
        public GetAllRecetasQueryHandler(IRecetaRepository recetaRepository)
        {
            this.recetaRepository = recetaRepository;
        }
        public Task<Result<List<RecetaDto>>> Handle(GetAllRecetasQuery request, CancellationToken cancellationToken)
        {
            var recetas = recetaRepository.GetAll();
            if (recetas == null || recetas.Count == 0)
                return Task.FromResult(Result.Failure<List<RecetaDto>>(Errors.RecetasNotFound));
            var list = recetas.Select(r => new RecetaDto
            {
                Id = r.Id,
                Nombre = r.Nombre,
                Instrucciones = r.Instrucciones,
                TiempoId = r.TiempoId,
                TiempoNombre = r.Tiempo.Nombre
            }).ToList();
            return Task.FromResult(Result.Success(list));
        }
    }
}
