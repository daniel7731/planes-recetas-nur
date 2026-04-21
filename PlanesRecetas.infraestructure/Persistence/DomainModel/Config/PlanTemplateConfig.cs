using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanesRecetas.domain.Template;

namespace PlanesRecetas.infraestructure.Persistence.DomainModel.Config
{
   

    public class PlanTemplateConfig : IEntityTypeConfiguration<PlanTemplate>
    {
        public void Configure(EntityTypeBuilder<PlanTemplate> builder)
        {
            builder.ToTable("PlanTemplate");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombre)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(p => p.Dias)
                   .IsRequired();

            // Relación: Un PlanTemplate tiene muchos Items
            builder.HasMany(p => p.Items)
                   .WithOne()
                   .HasForeignKey(pi => pi.PlanTemplateId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
