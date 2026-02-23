using PlanesRecetas.application.Medicos;
using PlanesRecetas.application.Pacientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Plan
{
    public class PlanAlimentacionDto
    {
        public Guid Id { get; set; }
        public PacienteDto Paciente { get; set; }
        public NutricionistaDto Nutricionista { get; set; }
        public List<DietaDto> Dietas { get; set; } = new();
        public DateTime FechaInicio { get; set; }
        public int DuracionDias { get; set; }
    }
}
