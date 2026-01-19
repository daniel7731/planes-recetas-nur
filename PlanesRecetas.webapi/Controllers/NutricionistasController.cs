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
            Guid guid = Guid.NewGuid();
           // request.Guid= guid;
           CreateNutricionistaComand createNutricionista = new CreateNutricionistaComand(
                guid,
                request.Nombre,
                request.Activo,
                request.FechaCreacion
                );
            var result = await _mediator.Send(createNutricionista);

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
