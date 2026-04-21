using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanesRecetas.infraestructure.Care;
using PlanesRecetas.webapi.Infrastructure;

namespace PlanesRecetas.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            ApiResponse response;
            try
            {
                var result = await _mediator.Send(new GetAllCategoriasQuery(), ct);
                response = ResponseHelper.CreateResponse(result);
            }
            catch(Exception e)
            {
                response = ResponseHelper.CreateErrorResponse(e);
            }        
            return Ok(response);
        }
    }
}