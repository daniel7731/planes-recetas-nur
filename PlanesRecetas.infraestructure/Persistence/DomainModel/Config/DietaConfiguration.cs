using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Persistence.DomainModel.Config
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PlanesRecetas.domain.Plan;

    public class DietaConfiguration : IEntityTypeConfiguration<Dieta>
    {
        public void Configure(EntityTypeBuilder<Dieta> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Dieta");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                   .IsRequired();

            builder.Property(d => d.FechaConsumo)
                   .IsRequired()
                   .HasColumnType("datetime");

            builder.Property(d => d.PlanAlimentacionId)
                   .IsRequired();

            builder.HasOne(d => d.PlanAlimentacion)
                   .WithMany(p => p.Dietas)
                   .HasForeignKey(d => d.PlanAlimentacionId)
                   .OnDelete(DeleteBehavior.Cascade);   
        }
    }
}
