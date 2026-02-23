using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanesRecetas.application.Plan;
using PlanesRecetas.domain.Persons;
using PlanesRecetas.domain.Plan;
using PlanesRecetas.domain.Recipe;
using PlanesRecetas.webapi.Parameters.Plan;

namespace PlanesRecetas.webapi.ControllersP
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanAlimentarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PlanAlimentarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePlanAlimentario([FromBody] CreatePlanAlimentarioParameter request)
        {
            Guid planGuid = Guid.NewGuid(); int days = 0;
            List<CreateDietaComand> dietas= request.Dietas.Select(dt => {
                Guid dietaID = Guid.NewGuid();
                CreateDietaComand dietacomand = new CreateDietaComand
                {
                    FechaConsumo = request.FechaInicio.AddDays(days),
                    Id =dietaID,
                    PlanId = planGuid               
                };
                int orden = 0;
                List<DietaRecetaComand> platillos = dt.Recetas.Select(r =>
                {
                    orden++;
                    DietaRecetaComand dietaReceta = new DietaRecetaComand()
                    {
                        DietaId = dietaID,
                        Orden = orden,
                        RecetaId = r.RecetaId,
                        TiempoId = r.TiempoId
                    };
                    return dietaReceta;


                } ).ToList();
                dietacomand.Platillos = platillos;
                days++;
            return dietacomand;}).ToList();
          
            CreatePlanAlimentacionComand createPlanAlimentario = new CreatePlanAlimentacionComand
            {
                Id = planGuid,
                PacienteId =  request.PacienteId,
                NutricionistaId = request.NutricionistaId,
                Dieta = dietas,
                FechaInicio = request.FechaInicio,
                DuracionDias = request.DuracionDias
            };
            var result = await _mediator.Send(createPlanAlimentario);
            return Ok(result);
            
        }
    }
}
