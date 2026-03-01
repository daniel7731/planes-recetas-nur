using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joseco.DDD.Core.Abstractions;
using PlanesRecetas.domain.Persons;
using PlanesRecetas.domain.Plan.Events;
namespace PlanesRecetas.domain.Plan
{
    public class PlanAlimentacion : AggregateRoot
    {
       
        public Guid PacienteId { get; set; }
        public Guid NutricionistaId { get; set; }
        public List<Dieta> Dietas { get; set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }

        public int DuracionDias
        {
            get; set;
        }

        protected PlanAlimentacion() { }

        public PlanAlimentacion(Guid id, Guid paciente, Guid nutricionista, DateTime fechaInicio , int duracion):base(id)
        {
            PacienteId = paciente;
            NutricionistaId = nutricionista;
            SetFechaInicio(fechaInicio, duracion);
            Dietas = new List<Dieta>();
            AddDomainEvent(new PlanCreated(Id, PacienteId, NutricionistaId, FechaInicio,duracion));
        }
        public void SetFechaInicio(DateTime fechaInicio , int duracion)
        {
            if (!(duracion == 15 || duracion == 30))
            {
                throw new ArgumentException("La duracion del plan debe ser de 15 o 30 dias.");
            }
        
            FechaFin = fechaInicio.AddDays(duracion);

            FechaInicio = fechaInicio;
        }
    }
}
