using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanesRecetas.application.Plan;
using PlanesRecetas.application.Plan.Query;
using PlanesRecetas.webapi.Parameters.Plan;

namespace PlanesRecetas.webapi.Controllers
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
            List<CreateDietaComand> dietas = request.Dietas.Select(dt =>
            {
                Guid dietaID = Guid.NewGuid();
                CreateDietaComand dietacomand = new CreateDietaComand
                {
                    FechaConsumo = request.FechaInicio.AddDays(days),
                    Id = dietaID,
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


                }).ToList();
                dietacomand.Platillos = platillos;
                days++;
                return dietacomand;
            }).ToList();

            CreatePlanAlimentacionComand createPlanAlimentario = new CreatePlanAlimentacionComand
            {
                Id = planGuid,
                PacienteId = request.PacienteId,
                NutricionistaId = request.NutricionistaId,
                Dieta = dietas,
                FechaInicio = request.FechaInicio,
                DuracionDias = request.DuracionDias
            };
            var result = await _mediator.Send(createPlanAlimentario);
            return Ok(result);

        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPlanAlimentario(Guid id)
        {
            try
            {
                GetPlanAlimentaryQuery query = new GetPlanAlimentaryQuery(id);
                var result = await _mediator.Send(query);
                if (result.IsFailure)
                {
                    return NotFound(result.Error);
                }
                else
                {
                    return Ok(result.Value);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDietaPlan(Guid planId)
        {
            try
            {
                GetRecetasByPlanAlimentarioQuery query = new GetRecetasByPlanAlimentarioQuery(planId);
                var result = await _mediator.Send(query);
                if (result.IsFailure)
                {
                    return NotFound(result.Error);
                }
                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> BuscarPlanAlimentarioByPaciente(ParamSearchPlanAlimentario paramSearchPlan)
        {
            try
            {
                var result = await _mediator.Send(new BuscarPlanAlimentarioByPacienteQuery(paramSearchPlan.PacienteId, paramSearchPlan.DesdeFecha , paramSearchPlan.IgnorarFiltroFecha));
                if (result.IsFailure)
                {
                    return NotFound(result.Error);
                }
                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}