using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanesRecetas.domain.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Persistence.DomainModel.Config
{
    public class UnidadConfig : IEntityTypeConfiguration<Unidad>
    {
        public void Configure(EntityTypeBuilder<Unidad> builder)
        {
            // Map to table
            builder.ToTable("Unidad");

            // Primary key
            builder.HasKey(u => u.Id);

            // Identity column (auto-increment)
            builder.Property(u => u.Id)
                   .ValueGeneratedOnAdd();

            // Nombre property
            builder.Property(u => u.Nombre)
                   .HasMaxLength(50)
                   .IsRequired();

            // Simbolo property
            builder.Property(u => u.Simbolo)
                   .HasMaxLength(10)
                   .IsRequired();

            
        }
    }
}
