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
    public class IngredienteConfig : IEntityTypeConfiguration<Ingrediente>
    {
        public void Configure(EntityTypeBuilder<Ingrediente> builder)
        {
            builder.ToTable("Ingrediente");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nombre).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Calorias).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.CantidadValor).HasColumnType("decimal(10,2)").IsRequired();

            builder.HasOne(x => x.Categoria)
                       .WithMany()
                       .HasForeignKey(x => x.Id)
                       .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Unidad)
                       .WithMany()
                       .HasForeignKey(x => x.Unidad.Id)
                       .OnDelete(DeleteBehavior.Restrict);
          


        }
    }
}
