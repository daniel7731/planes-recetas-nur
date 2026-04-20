using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanesRecetas.application.Recipe;
using PlanesRecetas.domain.Recipe.Query;
using PlanesRecetas.webapi.Parameters.Recipe;

namespace PlanesRecetas.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecetaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateReceta([FromBody] CreateRecetaParameter request, CancellationToken ct)
        {

            Guid guid = Guid.NewGuid();

            List<CreateRecetaIngredienteComand> ingredientes = request.IngredienteList.Select(ingredient =>
            {
                return new CreateRecetaIngredienteComand
                {
                    IngredienteId = ingredient.Id,
                    CantidadValor = ingredient.CantidadValor,
                    RecetaId = guid
                };
            }).ToList();
            CreateRecetaCommand createReceta = new CreateRecetaCommand
            {
                Id = guid,
                Nombre = request.Nombre,
                Ingredientes = ingredientes,
                TiempoId = request.TiempoId,
                Instrucciones = request.Instrucciones
            };
            var result = await _mediator.Send(createReceta, ct);

            return Ok(result);
        }
        //GetRecetaByIdQuery query = new GetRecetaByIdQuery { Id = id };
        [HttpPost("[action]")]
        public async Task<IActionResult> GetRecetaById(ParamGetReceta param, CancellationToken ct)
        {
            try
            {
                GetRecetaByIdQuery query = new(param.Id,true,param.IncludeIngredientes);
                var result = await _mediator.Send(query, ct);
                if (result.IsSuccess)
                {
                    return Ok(result.Value);
                }
                if (!result.IsSuccess)
                    return NotFound(result.Error);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetAllRecetas(CancellationToken ct)
        {
            GetAllRecetasQuery query = new GetAllRecetasQuery();
            var result = await _mediator.Send(query, ct);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return NotFound();
            }
        }
    }
}