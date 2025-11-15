using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joseco.DDD.Core.Abstractions;
using PlanesRecetas.domain.Persons;
namespace PlanesRecetas.domain.Care
{
    public class PlanAlimentacion : AggregateRoot
    {
       
        public Paciente Paciente { get; set; }
        public Nutricionista Nutricionista { get; set; }
        public List<Dieta> Dietas { get; set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }

        public Guid PacienteId
        {
            get
            {
                return Paciente.Id;
            }
        }
        public Guid NutricionistaId
        {
            get
            {
                return Nutricionista.Id;
            }
        }
        public int DuracionDias
        {
            get
            {
                return (int)(FechaFin - FechaInicio).TotalDays;
            }
        }
        public PlanAlimentacion(Guid id, Paciente paciente, Nutricionista nutricionista, DateTime fechaInicio , int duracion):base(id)
        {
            Paciente = paciente;
            Nutricionista = nutricionista;
            SetFechaInicio(fechaInicio, duracion);
            Dietas = new List<Dieta>();
        }
        public void SetFechaInicio(DateTime fechaInicio , int duracion)
        {
            if (duracion != 15 || duracion != 30)
            {
                throw new ArgumentException("La duracion del plan debe ser de 15 o 30 dias.");
            }
        
            FechaFin = fechaInicio.AddDays(duracion);

            FechaInicio = fechaInicio;
        }
    }
}
