using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PlanesRecetas.application.Recipe;
using PlanesRecetas.domain.Recipe.Query;
using PlanesRecetas.webapi.Infrastructure;
using PlanesRecetas.webapi.Parameters.Recipe;

namespace PlanesRecetas.webapi.Controllers
{
    [Route("api/plans/[controller]")]
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
            ApiResponse response;
            try
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
                response = ResponseHelper.CreateResponse(result);
            }
            catch(Exception ex)
            {
                response = ResponseHelper.CreateErrorResponse(ex);
            }
            return Ok(response);
        }
        //GetRecetaByIdQuery query = new GetRecetaByIdQuery { Id = id };
        [HttpPost("[action]")]
        public async Task<IActionResult> GetRecetaById(ParamGetReceta param, CancellationToken ct)
        {
            ApiResponse response;
            try
            {
                GetRecetaByIdQuery query = new(param.Id,true,param.IncludeIngredientes);
                var result = await _mediator.Send(query, ct);
                response = ResponseHelper.CreateResponse(result);   
            }
            catch(Exception ex)
            {
                response = ResponseHelper.CreateErrorResponse(ex);
            }
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetAllRecetas(CancellationToken ct)
        {
            ApiResponse response;
            try
            {
                GetAllRecetasQuery query = new GetAllRecetasQuery();
                var result = await _mediator.Send(query, ct);
                response  = ResponseHelper.CreateResponse(result);
            }
            catch (Exception ex)
            {
                response = ResponseHelper.CreateErrorResponse(ex);  
            }
            return Ok(response);
        }
    }
}