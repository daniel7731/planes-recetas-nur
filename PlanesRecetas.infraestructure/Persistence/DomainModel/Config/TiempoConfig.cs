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
    public class TiempoConfig : IEntityTypeConfiguration<Tiempo>
    {
        public void Configure(EntityTypeBuilder<Tiempo> builder)
        {
            builder.ToTable("Tiempo");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                   .ValueGeneratedOnAdd(); // Reflects IDENTITY(1,1)

            builder.Property(t => t.Nombre)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
