using Joseco.DDD.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using PlanesRecetas.domain.Care;
using PlanesRecetas.domain.Metrics;
using PlanesRecetas.domain.Persons;
using PlanesRecetas.infraestructure.Persistence.DomainModel.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Persistence.DomainModel
{
    public class DomainDbContext : DbContext
    {
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Nutricionista> Nutricionista { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<TipoAlimento> TipoAlimento { get; set; }
        public DbSet<Unidad> Unidad { get; set; }
        public DbSet<Ingrediente> Ingrediente{ get; set; }
        public DbSet<Receta> Receta { get; set; }
        public DbSet<Tiempo> Tiempo { get; set; }
        public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PacienteConfig());
            modelBuilder.Ignore<DomainEvent>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Receta>()
                  .HasMany(e => e.Ingredientes)
                  .WithMany(e => e.Recetas)
                  .UsingEntity("RecetaIngrediente");
        } 
    }
}
