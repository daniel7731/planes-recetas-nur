using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanesRecetas.domain.Care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Persistence.DomainModel.Config
{
    public class RecetaConfig : IEntityTypeConfiguration<Receta>
    {
        public void Configure(EntityTypeBuilder<Receta> builder)
        {
            builder.ToTable("Receta");

            // --- Primary Key and Properties ---
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                    .IsRequired();

            builder.Property(r => r.Nombre)
                    .IsRequired()
                    .HasMaxLength(150);

            builder.Property(r => r.TiempoId)
                    .IsRequired();

            // --- Relationships ---

            // 1. One-to-Many: Receta (Recipe) to Tiempo (Time/Duration)
            builder.HasOne(r => r.Tiempo)
                    .WithMany()
                    .HasForeignKey(r => r.TiempoId)
                    .OnDelete(DeleteBehavior.Restrict);

       

            // --- Ignored Properties ---
            builder.Ignore(r => r.IngredienteIds); // No mapea propiedad calculada/transitoria
            builder.Ignore(r => r.CalcularCalorias()); // Evita errores si se usa como propiedad
        }
    }
}
