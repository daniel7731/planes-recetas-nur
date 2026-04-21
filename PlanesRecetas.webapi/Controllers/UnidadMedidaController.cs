using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanesRecetas.application.Metrics;
using PlanesRecetas.webapi.Infrastructure;
using PlanesRecetas.webapi.Parameters.Metrics;

namespace PlanesRecetas.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadMedidaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UnidadMedidaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            ApiResponse response;
            try
            {
                var result = await _mediator.Send(new GetAllUnidadesMedidaQuery(), ct);
                response = ResponseHelper.CreateResponse(result);
            }
            catch(Exception ex)
            {
                response = ResponseHelper.CreateErrorResponse(ex);
            }
            return Ok(response);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUnidad([FromBody] CreateUnidadParameter request)
        {
            ApiResponse response;
            try
            {
                CreateUnidadMedidaComand createUnidad = new CreateUnidadMedidaComand
                {
                    Nombre = request.Nombre,
                    Simbolo = request.Simbolo
                };

                var result = await _mediator.Send(createUnidad);
                response = ResponseHelper.CreateResponse(result);
            }
            catch(Exception ex)
            {
                response = ResponseHelper.CreateErrorResponse(ex);
               
            }
            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUnidad(int id, [FromBody] UpdateUnidadMedidaParameter request)
        {
            ApiResponse response;
            try
            {
                UpdateUnidadMedidaComand command = new UpdateUnidadMedidaComand
                {
                    Id = id,
                    Nombre = request.Nombre,
                    Simbolo = request.Simbolo
                };
                var result = await _mediator.Send(command);
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