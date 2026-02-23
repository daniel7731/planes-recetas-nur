using Joseco.DDD.Core.Abstractions;
using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.domain.Persons;
using PlanesRecetas.domain.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Plan
{
    public class GetPlanAlimentarioQueryHandler : IRequestHandler<GetPlanAlimentaryQuery, Result<List<PlanAlimentacionDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlanAlimentacionRepository _planRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly INutricionistaRepository _nutricionistaRepository;
        private readonly IDietaRepository _dietaRepository;
        public GetPlanAlimentarioQueryHandler(IUnitOfWork unitOfWork, IPlanAlimentacionRepository planRepository , IDietaRepository dietaRepository , IPacienteRepository pacienteRepository
            ,INutricionistaRepository nutricionistaRepository)
        {
            _unitOfWork = unitOfWork;
            _planRepository = planRepository;
            _dietaRepository = dietaRepository;
            _pacienteRepository = pacienteRepository;
            _nutricionistaRepository = nutricionistaRepository;
        }
        public async Task<Result<List<PlanAlimentacionDto>>> Handle(GetPlanAlimentaryQuery request, CancellationToken cancellationToken)
        {
            // request.Id;
           PlanAlimentacion Plan = await _planRepository.GetByIdAsync(request.Id);  
            if (Plan is null)
                return Result.Failure<List<PlanAlimentacionDto>>(Errors.PlanNotFound);

           Paciente paciente = await _pacienteRepository.GetByIdAsync(Plan.PacienteId); 

           Nutricionista nutricionista = await _nutricionistaRepository.GetByIdAsync(Plan.NutricionistaId);

           List<Dieta> dietas  =  _dietaRepository.GetDietasPlan(Plan.Id);

           dietas.ForEach(d =>
           {
                List<DietaReceta> recetas = _dietaRepository.GetDietaRecetas(d.Id);
                d.Platillos = recetas;
           });
            PlanAlimentacionDto dto = new PlanAlimentacionDto
            {
                Id = Plan.Id,
                Paciente = new PacienteDto { Id = paciente.Id, Nombre = paciente.Nombre },
                Nutricionista = new Medicos.NutricionistaDto { Id = Plan.NutricionistaId, Nombre = nutricionista.Nombre },
                FechaInicio = Plan.FechaInicio,
                DuracionDias = Plan.DuracionDias,
                Dietas = dietas.Select(d =>
                     new DietaDto {
                         Id = d.Id,
                         Fecha = d.FechaConsumo,
                         Recetas = d.Platillos.Select(p => new DietaRecetaDto
                         {
                          
                             Orden = (int) p.Orden,
                             
                             Tiempo = new Care.TiempoDto { Id = p.TiempoId},
                             Receta = new Recipe.RecetaDto
                             {
                                 Id = p.Receta.Id,
                                 Nombre = p.Receta.Nombre,
                                 Instrucciones = p.Receta.Instrucciones,
                                 TiempoId = p.TiempoId,
                                 TiempoNombre = p.Receta.Tiempo.Nombre

                             }
                             


                         }).ToList()

                     }

                ).ToList()
            };
            return Result.Success(new List<PlanAlimentacionDto> { dto });
        }
    }
}
