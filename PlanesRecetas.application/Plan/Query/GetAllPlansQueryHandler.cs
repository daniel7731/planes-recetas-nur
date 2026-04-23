using Joseco.DDD.Core.Abstractions;
using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.application.Care;
using PlanesRecetas.application.Medicos;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.application.Recipe;
using PlanesRecetas.domain.Persons;
using PlanesRecetas.domain.Plan;

namespace PlanesRecetas.application.Plan.Query
{
    public class GetAllPlansQueryHandler : IRequestHandler<GetAllPlansQuery, Result<List<PlanAlimentacionDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlanAlimentacionRepository _planRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly INutricionistaRepository _nutricionistaRepository;
        private readonly IDietaRepository _dietaRepository;
        public GetAllPlansQueryHandler(IUnitOfWork unitOfWork, IPlanAlimentacionRepository planRepository, IDietaRepository dietaRepository, IPacienteRepository pacienteRepository
            , INutricionistaRepository nutricionistaRepository)
        {
            _unitOfWork = unitOfWork;
            _planRepository = planRepository;
            _dietaRepository = dietaRepository;
            _pacienteRepository = pacienteRepository;
            _nutricionistaRepository = nutricionistaRepository;
        }
        public async Task<Result<List<PlanAlimentacionDto>>> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
        {

            List<PlanAlimentacion> plans = _planRepository.GetAll();
            var planDtos = new List<PlanAlimentacionDto>();
            planDtos = plans.Select(plan => new PlanAlimentacionDto()
            {
                Nutricionista = plan.Nutricionista != null ? new NutricionistaDto()
                {
                    Id = plan.Nutricionista.Id,
                    Nombre = plan.Nutricionista.Nombre,
                    
                } : null,
                Paciente = plan.Paciente != null ? new PacienteDto()
                {
                    Id = plan.Paciente.Id,
                    Nombre = plan.Paciente.Nombre,
                    
                } : null,   
                Requerido = plan.Requerido,
                FechaInicio = plan.FechaInicio,
                Id = plan.Id,
                DuracionDias = plan.DuracionDias,
                    Dietas = plan.Dietas != null ? plan.Dietas.Select(dieta => new DietaDto()
                    {
                        Id = dieta.Id,
                        Fecha = dieta.FechaConsumo,
                        DietasRecetas = dieta.DietaRecetas != null ? dieta.DietaRecetas.Select(dr => new DietaRecetaDto()
                        {
                            Orden = (int)dr.Orden,
                            Tiempo = dr.Tiempo != null ? new TiempoDto()
                            {
                                Id = dr.Tiempo.Id,
                                Nombre = dr.Tiempo.Nombre
                            } : null,
                            Receta = dr.Receta != null ? new RecetaDto()
                            {
                                Id = dr.Receta.Id,
                                Nombre = dr.Receta.Nombre,
                                Instrucciones = dr.Receta.Instrucciones,
                                TiempoId = dr.Receta.TiempoId,
                                TiempoNombre = dr.Receta.Tiempo.Nombre
                            } : null

                        }).ToList() : new List<DietaRecetaDto>()
                    }).ToList() : new List<DietaDto>()
            }).ToList();

            return Result.Success(planDtos);
        }
    }
}
