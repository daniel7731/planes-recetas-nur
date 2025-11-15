using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanesRecetas.application.Care;

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
        public async Task<IActionResult> CreateReceta([FromBody] CreateRecetaCommand request, CancellationToken ct)
        {
            var result = await _mediator.Send(request, ct);

            return Ok(result);
        }
    }
}
