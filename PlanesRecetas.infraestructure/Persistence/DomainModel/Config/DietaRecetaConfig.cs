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

            builder.Property(dr => dr.Id)
                   .UseIdentityColumn(1, 2); // matches IDENTITY(1,2)

            builder.Property(dr => dr.DietaId)
                   .IsRequired();

            builder.Property(dr => dr.RecetaId)
                   .IsRequired();

            builder.Property(dr => dr.TiempoId)
                   .IsRequired();

            builder.Property(dr => dr.Orden)
                   .IsRequired(false);

            builder.HasOne(dr => dr.Dieta)
                   .WithMany(d => d.DietaRecetas)
                   .HasForeignKey(dr => dr.DietaId)
                   .OnDelete(DeleteBehavior.Cascade);

            /*builder.HasOne(dr => dr.Receta)
                   .WithMany(r => r.DietaRecetas)
                   .HasForeignKey(dr => dr.RecetaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(dr => dr.Tiempo)
                   .WithMany(t => t.DietaRecetas)
                   .HasForeignKey(dr => dr.TiempoId)
                   .OnDelete(DeleteBehavior.Restrict);*/
        }
    }
}
