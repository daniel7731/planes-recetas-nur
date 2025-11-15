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
    public class TipoAlimentoConfig : IEntityTypeConfiguration<TipoAlimento>
    {
        public void Configure(EntityTypeBuilder<TipoAlimento> builder)
        {
            builder.ToTable("TipoAlimento");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nombre).HasMaxLength(50).IsRequired();
         
        }
    }
}
