using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanesRecetas.application.Medicos;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.domain.Persons;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> CreateNutricionista([FromBody] CreateNutricionistaComand request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllNutricionistasQuery(), ct);
            if (!result.IsSuccess) return NotFound(result.Error);
            return Ok(result.Value);
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetNutricionistaByIdQuery(id, true), ct);
            if (!result.IsSuccess) return NotFound(result.Error);
            return Ok(result.Value);
        }

    }
}
