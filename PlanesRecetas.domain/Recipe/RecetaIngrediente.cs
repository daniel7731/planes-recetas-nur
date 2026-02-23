using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanesRecetas.domain.Recipe
{
    public class RecetaIngrediente
    {
        // Primary Key (Matches the SQL 'ID INT IDENTITY(1,1) PRIMARY KEY')
        public int Id { get; set; }

        // Foreign Key to Receta (Recipe)
        public Guid RecetaId { get; set; }

        // Navigation to Receta
        public Receta Receta { get; set; }

        // Foreign Key to Ingrediente (Ingredient)
        public Guid IngredienteId { get; set; }

        // Navigation to Ingrediente
        public Ingrediente Ingrediente { get; set; }

        // Additional property from the join table
        public decimal? CantidadValor { get; set; } // Nullable, as per your SQL schema
    }
}
