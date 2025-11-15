using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Persistence.PersistenceModel.Entities
{
    [Table("Nutricionista")]
    public class NutricionistaPersistenceModel
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Nombre")]
        [StringLength(100)]
        [Required]
        public string Nombre { get; set; }

        [Column("FechaCreacion")]
        [Required]
        public DateTime FechaCreacion { get; set; }

        [Column("Activo")]
        [Required]
        public bool Activo { get; set; }
    }
}
