using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanesRecetas.application.Care;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.application.Pacientes.PlanesRecetas.application.Persons;

namespace PlanesRecetas.webapi.Controllers
{
    [Route("api/[controller]")]
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
            var result = await _mediator.Send(new GetTipoAlimentoByIdQuery(id), ct);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(result.Value);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllTipoAlimentosQuery(), ct);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(result.Value);
        }
    }
}
