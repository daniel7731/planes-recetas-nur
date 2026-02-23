using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanesRecetas.application.Care;
using PlanesRecetas.application.Recipe;
using PlanesRecetas.webapi.Parameters.Recipe;

namespace PlanesRecetas.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredienteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IngredienteController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateIngrediente([FromBody] CreateIngredienteParamater request, CancellationToken ct)
        {
            Guid guid = Guid.NewGuid();
            CreateIngredienteCommand createIngrediente = new CreateIngredienteCommand
            {
                Id = guid,
                Calorias = request.Calorias,
                Nombre = request.Nombre,
                CantidadValor = request.CantidadValor,
                CategoriaId = request.CategoriaId,
                UnidadId = request.UnidadId
            };
            var result = await _mediator.Send(createIngrediente, ct);

            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllIngrediente(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllIngredientesQuery(), ct);

            if (!result.IsSuccess || result.Value is null || result.Value.Count == 0)
                return NotFound();

            return Ok(result.Value);
        }
    }
}
