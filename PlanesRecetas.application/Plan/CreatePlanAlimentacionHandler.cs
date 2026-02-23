using Joseco.DDD.Core.Abstractions;
using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.domain.Persons;
using PlanesRecetas.domain.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Plan
{
    public class CreatePlanAlimentacionHandler : IRequestHandler<CreatePlanAlimentacionComand, Result<Guid>>
    {
        private readonly IPlanAlimentacionRepository _planAlimentacionRepository;
        private readonly IDietaRepository _dietaRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly INutricionistaRepository _nutricionistaRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreatePlanAlimentacionHandler(IPlanAlimentacionRepository planAlimentacionRepository , IDietaRepository dietaRepository , IPacienteRepository pacienteRepository ,
            INutricionistaRepository nutricionistaRepository , IUnitOfWork unitOfWork)
        {
            _planAlimentacionRepository = planAlimentacionRepository;
            _dietaRepository = dietaRepository;
            _pacienteRepository = pacienteRepository;
            _nutricionistaRepository = nutricionistaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreatePlanAlimentacionComand request, CancellationToken cancellationToken)
        {
            //colocar valores de ejemplo para paciente y nutricionista, ya que no se han implementado los repositorios de estas entidades
            Guid pacienteId = request.PacienteId;
            Guid nutricionistaId = request.NutricionistaId;

            Paciente paciente1 = await _pacienteRepository.GetByIdAsync(pacienteId);
            if ( paciente1 == null)
            {
                return Result.Failure<Guid>(Error.Failure("", $"Paciente with Id '{pacienteId}' not found.", []));
            }   
            Nutricionista nutricionista1 = await _nutricionistaRepository.GetByIdAsync(nutricionistaId);
            if (nutricionista1 == null)
            {
                return Result.Failure<Guid>(Error.Failure("", $"Nutricionista with Id '{nutricionistaId}' not found.", []));
            }
            Guid planId = request.Id;

            var planAlimentacion = new PlanAlimentacion(
                planId,
                paciente1,
                nutricionista1,
                request.FechaInicio,
                request.DuracionDias
            );
           
            await _planAlimentacionRepository.AddAsync(planAlimentacion);
            request.Dieta.ForEach(async d =>
            {
                var dieta = new Dieta(d.Id, d.FechaConsumo, d.PlanId);
                await _dietaRepository.AddAsync(dieta);

                d.Platillos.ForEach(async p =>
                {
                    DietaReceta dietaReceta = new DietaReceta { 
                    Orden = p.Orden,
                    RecetaId = p.RecetaId,
                    TiempoId = p.TiempoId,
                    DietaId = dieta.Id };
                    await _dietaRepository.AddDietaReceta(dieta,dietaReceta); 
                });
            });
            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Success(planAlimentacion.Id);
        }
    }
}
