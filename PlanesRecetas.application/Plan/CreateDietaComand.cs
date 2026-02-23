using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.domain.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Plan
{
    public class CreateDietaComand: IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public Guid PlanId { get; set; }
        public List<DietaRecetaComand> Platillos { get; set; }

        public DateTime FechaConsumo;

    }
}
