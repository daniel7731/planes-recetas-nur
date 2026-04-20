using Joseco.DDD.Core.Abstractions;
using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.domain.Persons;
using PlanesRecetas.domain.Plan;

namespace PlanesRecetas.application.Plan.Query
{
    public class BuscarPlanAlimentarioByPacienteQueryHandler : IRequestHandler<BuscarPlanAlimentarioByPacienteQuery, Result<List<PlanAlimentacionDto>>>
    {
      
        private readonly IPlanAlimentacionRepository _planRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly INutricionistaRepository _nutricionistaRepository;
        private readonly IDietaRepository _dietaRepository;
        public BuscarPlanAlimentarioByPacienteQueryHandler( IPlanAlimentacionRepository planRepository, IPacienteRepository pacienteRepository, INutricionistaRepository nutricionistaRepository, IDietaRepository dietaRepository)
        {
   
            _planRepository = planRepository;
            _pacienteRepository = pacienteRepository;
            _nutricionistaRepository = nutricionistaRepository;
            _dietaRepository = dietaRepository;
        }
        public async Task<Result<List<PlanAlimentacionDto>>> Handle(BuscarPlanAlimentarioByPacienteQuery request, CancellationToken cancellationToken)
        {
            List<PlanAlimentacion> list = new List<PlanAlimentacion>();
            if (request.IgnorarFiltroFecha)
            {
                list = await _planRepository.GetByPacienteIdAsync(request.Id);
            }
            else
            {
                list = await _planRepository.SearchPacienteIdAsync(request.Id, request.FechaInicio, true);

            }      
            if (list == null || list.Count == 0)
                    return Result.Success(new List<PlanAlimentacionDto>());
             var dtoList =    list.Select( p => new PlanAlimentacionDto
                {
                    Id = p.Id,
                    Paciente = new PacienteDto { Id = p.PacienteId, 
                        Nombre = p.Paciente.Nombre,
                        Peso  = p.Paciente.Peso,
                        Altura = p.Paciente.Altura,
                    },
                    Nutricionista = new Medicos.NutricionistaDto { Id = p.NutricionistaId, 
                        
                        Nombre = p.Nutricionista.Nombre , Activo = p.Nutricionista.Activo },
                    FechaInicio = p.FechaInicio,
                    DuracionDias = p.DuracionDias,
                    Dietas = _dietaRepository.GetDietasPlan(p.Id).Select(d =>
                         new DietaDto
                         {
                             Id = d.Id,
                             Fecha = d.FechaConsumo,
                             Recetas = _dietaRepository.GetDietaRecetas(d.Id).Select(p => new DietaRecetaDto
                             {
                                 Orden = (int)p.Orden,
                                 Receta = new Recipe.RecetaDto
                                    {
                                        Id = p.RecetaId,
                                        Nombre = p.Receta.Nombre,
                                        Instrucciones = p.Receta.Instrucciones,
                                        Ingredientes = []
                                 },
                             }).ToList()
                         }).ToList()
                }).ToList();
            //  _planRepository.GetUltimoPlanAlimentarioPorPacienteId(request.Id);
            return Result.Success(dtoList);
        }
    }
}
