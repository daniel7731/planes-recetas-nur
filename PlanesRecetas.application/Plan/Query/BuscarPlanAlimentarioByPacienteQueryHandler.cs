using Joseco.DDD.Core.Abstractions;
using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.application.Care;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.application.Recipe;
using PlanesRecetas.domain.Persons;
using PlanesRecetas.domain.Plan;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Plan.Query
{
    public class BuscarPlanAlimentarioByPacienteQueryHandler : IRequestHandler<BuscarPlanAlimentarioByPacienteQuery, Result<List<PlanAlimentacionDto>>>
    {

        private readonly IPlanAlimentacionRepository _planRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly INutricionistaRepository _nutricionistaRepository;
        private readonly IDietaRepository _dietaRepository;
        public BuscarPlanAlimentarioByPacienteQueryHandler(IPlanAlimentacionRepository planRepository, IPacienteRepository pacienteRepository, INutricionistaRepository nutricionistaRepository, IDietaRepository dietaRepository)
        {

            _planRepository = planRepository;
            _pacienteRepository = pacienteRepository;
            _nutricionistaRepository = nutricionistaRepository;
            _dietaRepository = dietaRepository;
        }
        public async Task<Result<List<PlanAlimentacionDto>>> Handle(BuscarPlanAlimentarioByPacienteQuery request, CancellationToken cancellationToken)
        {
            // 1. Fetch the base list
            List<PlanAlimentacion> list;
            if (request.IgnorarFiltroFecha)
            {
                list = await _planRepository.GetByPacienteIdAsync(request.Id);
            }
            else
            {
                list = await _planRepository.SearchPacienteIdAsync(request.Id, request.FechaInicio, true);
            }

            if (list == null || !list.Any())
                return Result.Success(new List<PlanAlimentacionDto>());

            // 2. Map to DTOs
            // Note: If GetDietasPlan is async, add 'async' to the lambda and 'await' the call.
            // If it's sync, we can map directly.
            var dtoTasks = list.Select(async p => new PlanAlimentacionDto
            {
                Id = p.Id,
                Paciente = new PacienteDto
                {
                    Id = p.PacienteId,
                    Nombre = p.Paciente?.Nombre,
                    Peso = p.Paciente?.Peso ?? 0,
                    Altura = p.Paciente?.Altura ?? 0,
                },
                Nutricionista = new Medicos.NutricionistaDto
                {
                    Id = p.NutricionistaId,
                    Nombre = p.Nutricionista?.Nombre,
                    Activo = p.Nutricionista?.Activo ?? false
                },
                FechaInicio = p.FechaInicio,
                DuracionDias = p.DuracionDias,
                Requerido = p.Requerido,
                // Mapping nested collections
                Dietas = _dietaRepository.GetDietasPlan(p.Id).Select(d => new DietaDto
                {
                    Id = d.Id,
                    Fecha = d.FechaConsumo,
                    DietasRecetas = d.DietaRecetas.Select(dr => new DietaRecetaDto
                    {
                        Orden = (int)dr.Orden,
                        Receta = new RecetaDto { Id = dr.Receta.Id, Nombre = dr.Receta.Nombre , TiempoId = dr.Tiempo.Id , 
                         
                        TiempoNombre = dr.Tiempo.Nombre,
                            
                            Instrucciones= dr.Receta.Instrucciones , Ingredientes = [] },
                        Tiempo = new TiempoDto { Id = dr.Tiempo.Id, Nombre = dr.Tiempo.Nombre }
                    }).ToList()
                }).ToList()
            });

            // 3. Resolve all tasks and convert to List
            var dtoList = (await Task.WhenAll(dtoTasks)).ToList();

            return Result.Success(dtoList);
        }
    }
 }
