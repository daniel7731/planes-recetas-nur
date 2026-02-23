using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanesRecetas.domain.Recipe;

namespace PlanesRecetas.infraestructure.Persistence.DomainModel.Config
{
    public class RecetaIngredienteConfig : IEntityTypeConfiguration<RecetaIngrediente>
    {
        public void Configure(EntityTypeBuilder<RecetaIngrediente> builder)
        {
            builder.ToTable("RecetaIngrediente");

            builder.HasKey(ri => ri.Id);

            builder.Property(ri => ri.CantidadValor)
                   .HasColumnType("decimal(10,2)");

            builder.Property(ri => ri.RecetaId).IsRequired();

            builder.Property(ri => ri.IngredienteId).IsRequired();       
        }
    }
}
