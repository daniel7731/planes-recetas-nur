using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PlanesRecetas.application.Medicos;
using PlanesRecetas.webapi.Infrastructure;
using PlanesRecetas.webapi.Parameters.Doctors;


namespace PlanesRecetas.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutricionistasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NutricionistasController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateNutricionista([FromBody] CreateNutricionistaParameter request)
        {
            ApiResponse response;
            try
            {
                Guid guid = Guid.NewGuid();
                // request.Guid= guid;
                CreateNutricionistaComand createNutricionista = new CreateNutricionistaComand(
                     guid,
                     request.Nombre,
                     request.Activo,
                     request.FechaCreacion
                );
                var result = await _mediator.Send(createNutricionista);
                response   = ResponseHelper.CreateResponse(result);
            }
            catch(Exception e)
            {
                response = ResponseHelper.CreateErrorResponse(e);
            }
            return Ok(response);


        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {

            ApiResponse response;
            try
            {
                var result = await _mediator.Send(new GetAllNutricionistasQuery(), ct);
                response = ResponseHelper.CreateResponse(result);
            }
            catch(Exception e)
            {
                response = ResponseHelper.CreateErrorResponse(e);
                
            }
            return Ok(response);
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            ApiResponse response;
            try
            {    
                var result = await _mediator.Send(new GetNutricionistaByIdQuery(id, true), ct);
                response = ResponseHelper.CreateResponse(result);
            }
            catch(Exception e)
            {
                response = ResponseHelper.CreateErrorResponse(e);
            }
            return Ok(response);

        }

    }
}