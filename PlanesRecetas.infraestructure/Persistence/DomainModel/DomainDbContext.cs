using Joseco.DDD.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using PlanesRecetas.domain.Care;
using PlanesRecetas.domain.Metrics;
using PlanesRecetas.domain.Persons;
using PlanesRecetas.domain.Plan;
using PlanesRecetas.domain.Recipe;
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
        public DbSet<UnidadMedida> UnidadMedida { get; set; }
        public DbSet<Ingrediente> Ingrediente { get; set; }
        public DbSet<Dieta> Dieta { get; set; }
        public DbSet<PlanAlimentacion> PlanAlimentacion { get; set; }  
        public DbSet<Receta> Receta { get; set; }
        public DbSet<RecetaIngrediente> RecetaIngrediente { get; set; }

        public DbSet<DietaReceta> DietaReceta { get; set; }

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

            modelBuilder.Entity<UnidadMedida>().HasData(
            new { Id = 1, Nombre = "Gramos", Simbolo = "g" },
            new { Id = 2, Nombre = "Kilogramos", Simbolo = "kg" },
            new { Id = 3, Nombre = "Mililitro", Simbolo = "Ml" },
            new { Id = 4, Nombre = "Litro", Simbolo = "L" }
    );

            // 2. Seed TipoAlimento
            modelBuilder.Entity<TipoAlimento>().HasData(
                new { Id = 1, Nombre = "Verdura" },
                new { Id = 2, Nombre = "Fruta" },
                new { Id = 3, Nombre = "FrutoSeco" },
                new { Id = 4, Nombre = "CarneRoja" },
                new { Id = 5, Nombre = "CarneBlanca" },
                new { Id = 6, Nombre = "Grano" },
                new { Id = 7, Nombre = "Carbohidrato" }
            );

            // 3. Seed Tiempo
            modelBuilder.Entity<Tiempo>().HasData(
                new { Id = 1, Nombre = "Breakfast" }, // Fixed typo from 'Breaskfast'
                new { Id = 2, Nombre = "HalfMorning" },
                new { Id = 3, Nombre = "Lunch" },
                new { Id = 4, Nombre = "HalfAfternoon" },
                new { Id = 5, Nombre = "Dinner" }
            );

        }
    }
}
