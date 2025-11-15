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
    internal class PacienteConfig : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Paciente");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .IsRequired();

            builder.Property(p => p.Nombre)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(p => p.Apellido)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(p => p.FechaNacimiento)
                   .IsRequired();

            builder.Property(p => p.Email)
                   .HasMaxLength(150)
                   .IsRequired(false);

            builder.Property(p => p.Telefono)
                   .HasMaxLength(50)
                   .IsRequired(false);

            builder.Property(p => p.Peso)
                   .HasColumnType("decimal(10,2)");


            builder.Property(p => p.Altura)
                   .HasColumnType("decimal(10,2)");
        }
    }
}
