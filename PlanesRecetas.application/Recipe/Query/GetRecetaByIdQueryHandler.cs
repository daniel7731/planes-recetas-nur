using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.domain.Recipe;
using PlanesRecetas.domain.Recipe.Query;


namespace PlanesRecetas.application.Recipe.Query
{
    public class GetRecetaByIdQueryHandler : IRequestHandler<GetRecetaByIdQuery, Result<RecetaDto>>
    {
        private readonly IRecetaRepository recetaRepository;
        private readonly IRecetaIngredienteRepository recetaIngredienteRepository;
        private readonly IIngredienteRepository ingredienteRepository;
        public GetRecetaByIdQueryHandler(IRecetaRepository recetaRepository , 
            IRecetaIngredienteRepository recetaIngredienteRepository , IIngredienteRepository ingredienteRepository)
        {
            this.recetaRepository = recetaRepository;
            this.recetaIngredienteRepository = recetaIngredienteRepository;
            this.ingredienteRepository = ingredienteRepository;
        }
        public async Task<Result<RecetaDto>> Handle(GetRecetaByIdQuery request, CancellationToken cancellationToken)
        {
            var receta = await recetaRepository.GetByIdAsync(request.Id, request.ReadOnly);
            if (receta is null)
                return Result.Failure<RecetaDto>(Errors.RecetaNotFound);

            RecetaDto dto = new RecetaDto
            {
                Id = receta.Id,
                Nombre = receta.Nombre,
                Instrucciones = receta.Instrucciones,
                TiempoId = receta.TiempoId,
                TiempoNombre = receta.Tiempo.Nombre
            };
            if (request.IncludeIngredientes)
            {
               
              
                var listIngredientes = ingredienteRepository.GetIngredientesByRecetaId(receta.Id);
                dto.Ingredientes = listIngredientes.Select(ri => new RecetaIngredienteDto
                {
                    RecetaId = receta.Id,
                    IngredienteId = ri.Id,
                    IngredienteNombre = ri.Nombre,
                    CantidadValor = ri.CantidadValor,
                    CategoriaNombre = ri.Categoria.Nombre,
                    UnidadNombre = ri.Unidad.Nombre 
                }).ToList();
            }

            return Result.Success(dto);




        }
    }
}
