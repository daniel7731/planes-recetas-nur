using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanesRecetas.application.Plan;
using PlanesRecetas.domain.Recipe;
using PlanesRecetas.webapi.Parameters.Diet;

namespace PlanesRecetas.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DietaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateDietaDiaria([FromBody] AddDietaParameter request)
        {
            Guid guid = Guid.NewGuid();
            /**
             * guid,
                request.PlanId,
                request.Platillos,
                request.NDiaPlan,
                request.Fecha
             */
           /* var platillos = new List<Receta>();
            foreach (var platilloId in request.Platillos)
            {
                Receta r = new Receta(platilloId);
                platillos.Add(r);
            }


            CreateDietaComand createDieta = new CreateDietaComand{
                 Id = guid,
                 PlanId = request.PlanId,
                 Platillos = platillos,
                 NDiasPlan = request.NDiaPlan,
                 Fecha = request.Fecha,
            };
            var result = await _mediator.Send(createDieta);*/

            return Ok(Guid.NewGuid());
        }
    }
}
