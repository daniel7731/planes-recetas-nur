using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanesRecetas.application.Care;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.application.Pacientes.PlanesRecetas.application.Persons;
using PlanesRecetas.webapi.Infrastructure;

namespace PlanesRecetas.webapi.Controllers
{
    [Route("api/plans/[controller]")]
    [ApiController]
    public class TipoAlimentoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TipoAlimentoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            ApiResponse response;
            try
            {
                var result = await _mediator.Send(new GetTipoAlimentoByIdQuery(id), ct);
                response = ResponseHelper.CreateResponse(result);
            }
            catch(Exception ex)
            {
                response = ResponseHelper.CreateErrorResponse(ex);  
            }
            return Ok(response);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            ApiResponse response;
            try
            {
                var result = await _mediator.Send(new GetAllTipoAlimentosQuery(), ct);
                response = ResponseHelper.CreateResponse(result);
            }
            catch(Exception ex)
            {
                response = ResponseHelper.CreateErrorResponse(ex);
              
            }
            return Ok(response);
        }
    }
}