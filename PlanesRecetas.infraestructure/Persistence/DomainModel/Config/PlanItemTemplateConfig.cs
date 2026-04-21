using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanesRecetas.domain.Template;


namespace PlanesRecetas.infraestructure.Persistence.DomainModel.Config
{
    public class PlanItemTemplateConfig : IEntityTypeConfiguration<PlanItemTemplate>
    {
        public void Configure(EntityTypeBuilder<PlanItemTemplate> builder)
        {
            builder.ToTable("PlanItemTemplate");

            builder.HasKey(pi => pi.Id);

            builder.Property(pi => pi.NumeroDia)
                   .IsRequired();

            builder.Property(pi => pi.RecetaId)
                   .IsRequired();

            // Configuración de la relación con Receta
            builder.HasOne(pi => pi.Receta)
                   .WithMany()
                   .HasForeignKey(pi => pi.RecetaId)
                   .IsRequired();

            // Definimos explícitamente la relación inversa con el Template si fuera necesario,
            
        }
    }
}
