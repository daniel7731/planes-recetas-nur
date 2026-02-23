using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanesRecetas.domain.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Persistence.DomainModel.Config
{
    public class DietaRecetaConfig : IEntityTypeConfiguration<DietaReceta>
    {
        public void Configure(EntityTypeBuilder<DietaReceta> builder)
        {
           builder.ToTable("DietaReceta");
           builder.HasKey(dr => dr.Id);
           builder.Property(dr => dr.TiempoId)
                .IsRequired();
          builder.HasOne(dr => dr.Receta)
                .WithMany()
                .HasForeignKey(dr => dr.RecetaId)
                .OnDelete(DeleteBehavior.Cascade);
           builder.HasOne(dr => dr.Dieta)
                .WithMany()
                .HasForeignKey(dr => dr.DietaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
