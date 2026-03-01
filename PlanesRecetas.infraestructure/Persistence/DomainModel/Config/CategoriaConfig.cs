using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanesRecetas.domain.Care;
using PlanesRecetas.domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Persistence.DomainModel.Config
{
    internal class CategoriaConfig : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");

            // Primary Key
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .IsRequired();

            // Properties
            builder.Property(c => c.Nombre)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.TipoAlimentoId)
                   .IsRequired();

            // Relationship
            builder.HasOne(c => c.TipoAlimento)
                   .WithMany(t => t.Categorias)
                   .HasForeignKey(c => c.TipoAlimentoId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
