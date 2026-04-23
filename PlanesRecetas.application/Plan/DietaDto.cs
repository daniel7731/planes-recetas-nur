using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Plan
{
    public class DietaDto
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public List<DietaRecetaDto> DietasRecetas { get; set; } = new();
    }
}