using Joseco.DDD.Core.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.application.Pacientes.PlanesRecetas.application.Persons;
using PlanesRecetas.webapi.Parameters.Persons;
using System;

namespace PlanesRecetas.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PacienteController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePaciente([FromBody] CreatePacienteParameter request)
        {
            Guid guid = Guid.NewGuid();
            //request.Guid = guid;
            CreatePacienteComand createPaciente = new CreatePacienteComand(
                guid,
                request.Nombre,
                request.Apellido,
                request.FechaNacimiento,
                request.Peso,
                request.Altura
                );
            var result = await _mediator.Send(createPaciente);

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Exists([FromBody] ExistsPacienteQuery request, CancellationToken ct)
        {
            var result = await _mediator.Send(request);
            if (!result.IsSuccess)
                return NotFound();
            return result.Value ? Ok(result) : NotFound();
        }
        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPacienteByIdQuery(id, true), ct);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(result.Value);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllPacientesQuery(), ct);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(result.Value);
        }
        /*public async Task<IActionResult> Delete([FromBody] DeletePacienteCommand request, CancellationToken ct)
        {
            var result = await _mediator.Send(request);
            if (!result.IsSuccess) return NotFound(result.Error);
            return NoContent();
        }*/

    }
}
