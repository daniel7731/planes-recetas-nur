using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanesRecetas.domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Persistence.DomainModel.Config
{
    internal class NutricionistaConfig : IEntityTypeConfiguration<Nutricionista>
    {
        public void Configure(EntityTypeBuilder<Nutricionista> builder)
        {
            builder.ToTable("Nutricionista");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nombre)
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(x => x.Activo)
                   .IsRequired();
            builder.Property(x => x.FechaCreacion);
        }
    }
}
