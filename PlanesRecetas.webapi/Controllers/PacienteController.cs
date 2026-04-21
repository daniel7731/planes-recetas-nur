using IdentityModel.OidcClient;
using Joseco.DDD.Core.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.application.Pacientes.PlanesRecetas.application.Persons;
using PlanesRecetas.webapi.Infrastructure;
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
            ApiResponse response = new();
            try
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
                response = ResponseHelper.CreateResponse(result);
            }
            catch(Exception e)
            {
                response = ResponseHelper.CreateErrorResponse(e);
            }
            return Ok(response);
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
            ApiResponse response = new ();
            try
            {
                var result = await _mediator.Send(new GetPacienteByIdQuery(id, true), ct);
                response = ResponseHelper.CreateResponse(result);
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
            ApiResponse response = new();
            try
            {
                var result = await _mediator.Send(new GetAllPacientesQuery(), ct);
                response = ResponseHelper.CreateResponse(result);
            }
            catch(Exception e)
            {
                response = ResponseHelper.CreateErrorResponse(e);
            } 
            return Ok(response);
        }
        /*public async Task<IActionResult> Delete([FromBody] DeletePacienteCommand request, CancellationToken ct)
        {
            var result = await _mediator.Send(request);
            if (!result.IsSuccess) return NotFound(result.Error);
            return NoContent();
        }*/

    }
}