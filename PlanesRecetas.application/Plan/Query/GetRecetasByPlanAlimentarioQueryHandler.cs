using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.application.Care;
using PlanesRecetas.application.Recipe;
using PlanesRecetas.domain.Plan;

namespace PlanesRecetas.application.Plan.Query
{
    public class GetRecetasByPlanAlimentarioQueryHandler : IRequestHandler<GetRecetasByPlanAlimentarioQuery, Result<List<DietaRecetaDto>>>
    {
        private readonly IPlanAlimentacionRepository _repository;
        
        public GetRecetasByPlanAlimentarioQueryHandler(IPlanAlimentacionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<DietaRecetaDto>>> Handle(GetRecetasByPlanAlimentarioQuery request, CancellationToken cancellationToken)
        {
            var dietas = await _repository.GetRecetasByPlanIdAsync(request.PlanId);
            if (dietas == null || !dietas.Any())
            {
                return Result.Success(new List<DietaRecetaDto>());
            }

            var list = dietas.Select(r => new DietaRecetaDto
            {
                // Agregamos 'new RecetaDto' y validamos con '?'
                Receta = r.Receta != null ? new RecetaDto
                {
                    Id = r.Receta.Id,
                    Nombre = r.Receta.Nombre,
                    Instrucciones = r.Receta.Instrucciones,
                    TiempoId = r.Tiempo.Id,
                    TiempoNombre = r.Tiempo.Nombre,       
                    Ingredientes = []
                } : null,
                
                // Agregamos 'new TiempoDto' y validamos con '?'
                Tiempo = r.Tiempo != null ? new TiempoDto
                {
                    Id = r.Tiempo.Id,
                    Nombre = r.Tiempo.Nombre
                } : null,
                Orden = (int)(r.Orden == null ? 0 : r.Orden)
            }).ToList();

            return Result.Success(list);
        }
    }
}
