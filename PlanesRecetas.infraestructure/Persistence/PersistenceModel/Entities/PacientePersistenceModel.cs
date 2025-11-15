using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Persistence.PersistenceModel.Entities
{
    [Table("Paciente")]
    public class PacientePersistenceModel
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Nombre")]
        [StringLength(100)]
        [Required]
        public string Nombre { get; set; }

        [Column("Apellido")]
        [StringLength(100)]
        [Required]
        public string Apellido { get; set; }

        [Column("FechaNacimiento")]
        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Column("Email")]
        [StringLength(150)]
        public string? Email { get; set; }

        [Column("Telefono")]
        [StringLength(50)]
        public string? Telefono { get; set; }

        [Column("Peso", TypeName = "decimal(10,2)")]
        public decimal? Peso { get; set; }

        [Column("Altura", TypeName = "decimal(10,2)")]
        public decimal? Altura { get; set; }
    }
}
