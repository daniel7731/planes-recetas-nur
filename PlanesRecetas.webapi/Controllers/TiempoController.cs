using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanesRecetas.application.Care;
using PlanesRecetas.webapi.Infrastructure;

namespace PlanesRecetas.webapi.Controllers
{
    [Route("api/plans/[controller]")]
    [ApiController]
    public class TiempoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TiempoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse response;
            try
            {
                var result = await _mediator.Send(new GetAllTiemposQuery());
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