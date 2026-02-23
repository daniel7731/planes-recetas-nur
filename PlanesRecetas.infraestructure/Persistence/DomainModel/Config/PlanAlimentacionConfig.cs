using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanesRecetas.domain.Persons;
using PlanesRecetas.domain.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Persistence.DomainModel.Config
{
    public class PlanAlimentacionConfig : IEntityTypeConfiguration<PlanAlimentacion>
    {
        public void Configure(EntityTypeBuilder<PlanAlimentacion> builder)
        {
            builder.ToTable("PlanAlimentacion");
            builder.HasKey(x => x.Id);

           // builder.Property(x => x.PacienteId).IsRequired();
           // builder.Property(x => x.NutricionistaId).IsRequired();
            builder.Property(x => x.FechaInicio).HasColumnType("DATE").IsRequired();
            builder.Property(x => x.FechaFin).HasColumnType("DATE").IsRequired();

            // Configuración de columna calculada PERSISTED
            builder.Property(x => x.DuracionDias)
                .HasComputedColumnSql("DATEDIFF(DAY, [FechaInicio], [FechaFin])", stored: true);

            // Relaciones (FKs)
            // Nota: Se asume que existen las entidades Paciente y Nutricionista en tu dominio
            builder.HasOne<Paciente>()
                .WithMany()
                .HasForeignKey(x => x.Paciente.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Nutricionista>()
                .WithMany()
                .HasForeignKey(x => x.Nutricionista.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}