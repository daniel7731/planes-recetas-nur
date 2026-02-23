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
    public class CreatePlanAlimentacionComand: IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public Guid PacienteId { get; set; }
        public Guid NutricionistaId { get; set; }
        public List<CreateDietaComand> Dieta { get; set; }
        public DateTime FechaInicio { get; set; }
        public int DuracionDias { get; set; }
    }
}
