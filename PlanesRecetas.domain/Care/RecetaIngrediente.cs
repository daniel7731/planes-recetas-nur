using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Care
{
    public class RecetaIngrediente
    {
        // Primary Key (Matches the SQL 'ID INT IDENTITY(1,1) PRIMARY KEY')
        public int ID { get; set; }

        // Foreign Key to Receta (Recipe)
        public Guid RecetaId { get; set; }

        // Navigation Property back to Receta
        public Receta Receta { get; set; }

        // Foreign Key to Ingrediente (Ingredient)
        public Guid IngredienteId { get; set; }

        // Navigation Property back to Ingrediente
        public Ingrediente Ingrediente { get; set; }

        // Additional property from the join table
        public decimal? CantidadValor { get; set; } // Nullable, as per your SQL schema
    }
}
