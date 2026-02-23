using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.domain.Plan;
using PlanesRecetas.domain.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Plan
{
    public class CreateDietaHandler : IRequestHandler<CreateDietaComand, Result<Guid>>
    {
        private readonly IDietaRepository _dietaRepository;
        public CreateDietaHandler(IDietaRepository dietaRepository)
        {
            _dietaRepository = dietaRepository;
        }

        public async Task<Result<Guid>> Handle(CreateDietaComand request, CancellationToken cancellationToken)
        {
            //var dieta = new Dieta(request.Id, request.PlanId, request.Platillos, request.NDiasPlan, request.Fecha);

            //await _dietaRepository.AddAsync(dieta);

            //return Result.Success(dieta.Id);
           
            

            
            //await _dietaRepository.AddAsync(dieta);

            //return Result.Success(dieta.Id);
            return Result.Success(Guid.NewGuid());
        }

    }
}
