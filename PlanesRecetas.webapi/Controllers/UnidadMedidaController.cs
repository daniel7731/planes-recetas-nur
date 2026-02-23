using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanesRecetas.application.Metrics;
using PlanesRecetas.webapi.Parameters.Metrics;

namespace PlanesRecetas.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadMedidaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public  UnidadMedidaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllUnidadesMedidaQuery(), ct);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(result.Value);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUnidad([FromBody] CreateUnidadParameter request)
        {
            CreateUnidadMedidaComand createUnidad = new CreateUnidadMedidaComand
            {
                Nombre = request.Nombre,
                Simbolo = request.Simbolo
            };

            var result = await _mediator.Send(createUnidad);
            return Ok(result.Value);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUnidad(int id, [FromBody] UpdateUnidadMedidaParameter request)
        {
            UpdateUnidadMedidaComand command = new UpdateUnidadMedidaComand
            {
                Id = id,
                Nombre = request.Nombre,
                Simbolo = request.Simbolo
            };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
