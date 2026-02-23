using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanesRecetas.domain.Recipe;

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

            // FK should point to CategoriaId (not Id)
            builder.HasOne(x => x.Categoria)
                       .WithMany()
                       .HasForeignKey(x => x.CategoriaId)
                       .OnDelete(DeleteBehavior.Restrict);

            // FK should point to UnidadId
            builder.HasOne(x => x.Unidad)
                       .WithMany()
                       .HasForeignKey(x => x.UnidadId)
                       .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
