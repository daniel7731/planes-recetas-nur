using Joseco.DDD.Core.Abstractions;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.EFCore.Persistence;
using Microsoft.EntityFrameworkCore;
using PlanesRecetas.domain.Care;
using PlanesRecetas.domain.Metrics;
using PlanesRecetas.domain.Persons;
using PlanesRecetas.domain.Plan;
using PlanesRecetas.domain.Recipe;
using System.Reflection;

namespace PlanesRecetas.infraestructure.Persistence.DomainModel
{
    public class DomainDbContext : DbContext, IDatabase
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
        public DbSet<OutboxMessage<DomainEvent>> OutboxMessages { get; internal set; }

        public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.AddOutboxModel<DomainEvent>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<DomainEvent>();
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<UnidadMedida>().HasData(
            new { Id = 1, Nombre = "Gramos", Simbolo = "g" },
            new { Id = 2, Nombre = "Kilogramos", Simbolo = "kg" },
            new { Id = 3, Nombre = "Mililitro", Simbolo = "Ml" },
            new { Id = 4, Nombre = "Litro", Simbolo = "L" });

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

            modelBuilder.Entity<Categoria>().HasData(
            new { Id = Guid.Parse("63F881A9-B533-410B-BC4C-0093990F9DA1"), Nombre = "Verdura de raíz", TipoAlimentoId = 1 },
            new { Id = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), Nombre = "Cebada", TipoAlimentoId = 6 },
            new { Id = Guid.Parse("A185F37C-EC77-4037-BF7F-0DF4555E58B9"), Nombre = "Cereal", TipoAlimentoId = 7 },
            new { Id = Guid.Parse("5F46DCC2-156D-4EC4-8854-0E16FB78308B"), Nombre = "Arroz", TipoAlimentoId = 6 },
            new { Id = Guid.Parse("313DE149-AA99-48FF-8D2A-1F6A778469AD"), Nombre = "Cordero", TipoAlimentoId = 4 },
            new { Id = Guid.Parse("3B96F79B-7488-479F-8A43-263EBA3C9077"), Nombre = "Papa", TipoAlimentoId = 7 },
            new { Id = Guid.Parse("D9128E81-1D35-4418-824D-34432C572DFF"), Nombre = "Pescado blanco", TipoAlimentoId = 5 },
            new { Id = Guid.Parse("399A37BB-4703-40E1-941F-369B2D51D952"), Nombre = "Pistachos", TipoAlimentoId = 3 },
            new { Id = Guid.Parse("C8EE09E0-8EEC-4F25-95AF-3AB6235578DA"), Nombre = "Avena", TipoAlimentoId = 6 },
            new { Id = Guid.Parse("0AC3EC26-2F75-46DC-A13C-3DB9A4CA6AB6"), Nombre = "Nueces", TipoAlimentoId = 3 },
            new { Id = Guid.Parse("F6FE0472-FDE5-439F-9921-3F7DDBCA926E"), Nombre = "Maní", TipoAlimentoId = 3 },
            new { Id = Guid.Parse("CB120002-33C9-45EB-B475-437E96E8DA49"), Nombre = "Carne de ave", TipoAlimentoId = 5 },
            new { Id = Guid.Parse("A22302FA-057B-4603-A68D-49A7816CE1FE"), Nombre = "Pavo", TipoAlimentoId = 5 },
            new { Id = Guid.Parse("08082E0E-3DB0-4FC5-82AD-6BE43D1D8B96"), Nombre = "Almendras", TipoAlimentoId = 3 },
            new { Id = Guid.Parse("4F0D9CF1-1805-4F37-BBEF-788144D527BA"), Nombre = "Verdura congelada", TipoAlimentoId = 1 },
            new { Id = Guid.Parse("B7DC2A55-AC7A-4DDF-A2E0-7BF8D603D660"), Nombre = "Conejo", TipoAlimentoId = 5 },
            new { Id = Guid.Parse("6C7BCE6B-E56E-482C-8499-7E2CE9FBB7E4"), Nombre = "Carne molida", TipoAlimentoId = 4 },
            new { Id = Guid.Parse("D6F06539-7E82-40D3-B29F-86B8941375BC"), Nombre = "Castañas", TipoAlimentoId = 3 },
            new { Id = Guid.Parse("7028B05F-9A06-4DAD-8049-93900C85A4F0"), Nombre = "Cerdo", TipoAlimentoId = 4 },
            new { Id = Guid.Parse("C3D3C887-A7E8-4679-87BC-956F55401C74"), Nombre = "Fruta de estación", TipoAlimentoId = 2 },
            new { Id = Guid.Parse("482D1425-CECE-43BE-903C-9896902BAFC1"), Nombre = "Res", TipoAlimentoId = 4 },
            new { Id = Guid.Parse("6FD10062-3080-4F9F-889D-A5A48688711E"), Nombre = "Fruta cítrica", TipoAlimentoId = 2 },
            new { Id = Guid.Parse("8B3369CE-284E-46F6-91C6-A6A5789E289B"), Nombre = "Trigo", TipoAlimentoId = 6 },
            new { Id = Guid.Parse("C0BCE9D3-0E49-4438-B7AF-AC305E6300B0"), Nombre = "Fruta tropical", TipoAlimentoId = 2 },
            new { Id = Guid.Parse("83F2DEA9-5A6C-43A0-9DAC-B4790611B524"), Nombre = "Verdura fresca", TipoAlimentoId = 1 },
            new { Id = Guid.Parse("7A487298-591F-4732-BCC6-C155A3CB71BA"), Nombre = "Yuca", TipoAlimentoId = 7 },
            new { Id = Guid.Parse("D5EE7BF1-C75C-4490-8820-C2CEB374E846"), Nombre = "Pasta", TipoAlimentoId = 7 },
            new { Id = Guid.Parse("840DCD1D-F4E8-4156-A18C-CEC437D0F805"), Nombre = "Fruta congelada", TipoAlimentoId = 2 },
            new { Id = Guid.Parse("D8DC275B-FF4F-44ED-A291-DF3ED7CCC760"), Nombre = "Pollo", TipoAlimentoId = 5 },
            new { Id = Guid.Parse("19596E02-F8CB-443A-B039-E0D86C213E68"), Nombre = "Maíz", TipoAlimentoId = 6 },
            new { Id = Guid.Parse("C9B6508F-91A3-4A3D-8DF7-E3D3AA7E2F02"), Nombre = "Fruta seca", TipoAlimentoId = 2 },
            new { Id = Guid.Parse("7C068691-0B72-485B-AA2B-E6D85CE5B656"), Nombre = "Pan", TipoAlimentoId = 7 },
            new { Id = Guid.Parse("8E0181DA-6328-4BE1-8CD1-E88C29F1B6C2"), Nombre = "Verdura orgánica", TipoAlimentoId = 1 },
            new { Id = Guid.Parse("7CE7F003-BCEB-4A9B-B879-EB2DEEBAA373"), Nombre = "Carne curada", TipoAlimentoId = 4 });


            modelBuilder.Entity<Ingrediente>().HasData(
                new { Id = Guid.Parse("A46B3498-1488-4EB8-AA15-0068C4D596F1"), Calorias = 400m, Nombre = "Estofado de cordero (porción)", CategoriaId = Guid.Parse("313DE149-AA99-48FF-8D2A-1F6A778469AD"), UnidadId = 2, CantidadValor = 1m },
                new { Id = Guid.Parse("EC6B6EAF-54F5-4485-B451-03448A943F74"), Calorias = 366m, Nombre = "Harina de arroz", CategoriaId = Guid.Parse("5F46DCC2-156D-4EC4-8854-0E16FB78308B"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("3AC58F2F-734E-46C6-836B-04AF33DECCBD"), Calorias = 354m, Nombre = "Cebada perlada", CategoriaId = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("EE4940B2-B603-4C83-9741-0643511215A0"), Calorias = 140m, Nombre = "Pavo deshuesado (crudo)", CategoriaId = Guid.Parse("A22302FA-057B-4603-A68D-49A7816CE1FE"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("18A81599-C01A-443F-BC37-07CD62CCA9A5"), Calorias = 135m, Nombre = "Filete de pavo (cocido)", CategoriaId = Guid.Parse("A22302FA-057B-4603-A68D-49A7816CE1FE"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("2CAFBCAC-E4EF-4353-B9AA-07D9D73B3D08"), Calorias = 400m, Nombre = "Harina de avena", CategoriaId = Guid.Parse("C8EE09E0-8EEC-4F25-95AF-3AB6235578DA"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("A5356075-69DE-465A-ACCA-0CD25CA72B81"), Calorias = 330m, Nombre = "Harina de papa", CategoriaId = Guid.Parse("3B96F79B-7488-479F-8A43-263EBA3C9077"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("A603D9FD-6BEF-466E-9F0A-0CD9D148BDDA"), Calorias = 110m, Nombre = "Fletán (halibut)", CategoriaId = Guid.Parse("D9128E81-1D35-4418-824D-34432C572DFF"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("C2EFE0AF-1950-42F2-A2FD-0CE6ADCC83A5"), Calorias = 135m, Nombre = "Hígado de cordero", CategoriaId = Guid.Parse("313DE149-AA99-48FF-8D2A-1F6A778469AD"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("9DDFF76B-D8C4-450D-AD00-0DDB152CDF91"), Calorias = 150m, Nombre = "Pavo molido magro", CategoriaId = Guid.Parse("A22302FA-057B-4603-A68D-49A7816CE1FE"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("5F46DCC2-156D-4EC4-8854-0E16FB78308B"), Calorias = 220m, Nombre = "porcion de arroz", CategoriaId = Guid.Parse("5F46DCC2-156D-4EC4-8854-0E16FB78308B"), UnidadId = 1, CantidadValor = 220m },
                new { Id = Guid.Parse("2682A7C3-2972-4A94-9C29-1275A5A125E2"), Calorias = 10m, Nombre = "Raices rojas", CategoriaId = Guid.Parse("63F881A9-B533-410B-BC4C-0093990F9DA1"), UnidadId = 1, CantidadValor = 100m },
                new { Id = Guid.Parse("0BFD95B9-BB30-493D-AD04-12CD72102366"), Calorias = 340m, Nombre = "Arroz jazmín crudo", CategoriaId = Guid.Parse("5F46DCC2-156D-4EC4-8854-0E16FB78308B"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("ABB20811-4D69-4C4D-9AF5-14EA3516CD7B"), Calorias = 166m, Nombre = "Arroz salvaje cocido", CategoriaId = Guid.Parse("5F46DCC2-156D-4EC4-8854-0E16FB78308B"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("8742D4E0-382B-47D6-B9E3-1617D57C11AD"), Calorias = 86m, Nombre = "Boniato (camote)", CategoriaId = Guid.Parse("63F881A9-B533-410B-BC4C-0093990F9DA1"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("5A8971DA-18AE-44C8-8413-177FE0F6708B"), Calorias = 30m, Nombre = "Embutido de pavo (rebanada)", CategoriaId = Guid.Parse("CB120002-33C9-45EB-B475-437E96E8DA49"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("A2340BA7-6AD4-4A5A-96B1-17F1AA7A0E48"), Calorias = 40m, Nombre = "Leche de almendra (sin azúcar, vaso)", CategoriaId = Guid.Parse("08082E0E-3DB0-4FC5-82AD-6BE43D1D8B96"), UnidadId = 2, CantidadValor = 1m },
                new { Id = Guid.Parse("175950FB-17FC-42AC-B08A-18EA09184246"), Calorias = 95m, Nombre = "Rape (cola)", CategoriaId = Guid.Parse("D9128E81-1D35-4418-824D-34432C572DFF"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("68A43539-0FD9-430A-A673-19161A744413"), Calorias = 10m, Nombre = "Zapallo", CategoriaId = Guid.Parse("4F0D9CF1-1805-4F37-BBEF-788144D527BA"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("D5918F21-1388-48FA-8D65-1AFC18767DD5"), Calorias = 562m, Nombre = "Pistachos crudos (sin cáscara)", CategoriaId = Guid.Parse("399A37BB-4703-40E1-941F-369B2D51D952"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("B3C1F091-C3A9-492A-A617-1B0791C984DF"), Calorias = 75m, Nombre = "Papa al vapor", CategoriaId = Guid.Parse("3B96F79B-7488-479F-8A43-263EBA3C9077"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("4FCD46F2-68B6-41B4-AB2F-1F10687571B4"), Calorias = 500m, Nombre = "Maní japonés (con cáscara)", CategoriaId = Guid.Parse("F6FE0472-FDE5-439F-9921-3F7DDBCA926E"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("BC099A87-D608-486A-A131-20A70B53BC5E"), Calorias = 320m, Nombre = "Cebada en copos", CategoriaId = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("31661016-1C2F-49E0-B11B-22434DE2D349"), Calorias = 105m, Nombre = "Bacalao fresco (cocido)", CategoriaId = Guid.Parse("D9128E81-1D35-4418-824D-34432C572DFF"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("E0F38C54-82BB-40DD-A5E3-22716B2E20AD"), Calorias = 100m, Nombre = "Mantequilla de almendra (cda)", CategoriaId = Guid.Parse("08082E0E-3DB0-4FC5-82AD-6BE43D1D8B96"), UnidadId = 4, CantidadValor = 1m },
                new { Id = Guid.Parse("19B41BF7-CBF2-4F0E-88D5-2293486E3C39"), Calorias = 575m, Nombre = "Pistachos tostados y salados", CategoriaId = Guid.Parse("399A37BB-4703-40E1-941F-369B2D51D952"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("F62EAA37-2290-44E8-8966-24CA70D9A96E"), Calorias = 370m, Nombre = "Maíz molido", CategoriaId = Guid.Parse("A185F37C-EC77-4037-BF7F-0DF4555E58B9"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("3E363C48-09FC-4A35-8F18-24F10770B180"), Calorias = 590m, Nombre = "Almendras laminadas", CategoriaId = Guid.Parse("08082E0E-3DB0-4FC5-82AD-6BE43D1D8B96"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("05D2D337-16D8-43A9-AB48-2B7D61A2D320"), Calorias = 250m, Nombre = "Salsa de maní (porción)", CategoriaId = Guid.Parse("F6FE0472-FDE5-439F-9921-3F7DDBCA926E"), UnidadId = 2, CantidadValor = 1m },
                new { Id = Guid.Parse("01B9DD28-4238-46ED-B97D-2C3332307F04"), Calorias = 339m, Nombre = "Trigo integral", CategoriaId = Guid.Parse("A185F37C-EC77-4037-BF7F-0DF4555E58B9"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("AC8B7AFF-A871-4271-BAB5-2CB5FA48C4E0"), Calorias = 300m, Nombre = "Extracto de malta", CategoriaId = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), UnidadId = 4, CantidadValor = 1m },
                new { Id = Guid.Parse("4593F1F9-EBCE-45DD-8577-2DB912476EF7"), Calorias = 338m, Nombre = "Centeno en grano", CategoriaId = Guid.Parse("A185F37C-EC77-4037-BF7F-0DF4555E58B9"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("35FA6E8E-8217-442A-A376-3179D9E14E1D"), Calorias = 150m, Nombre = "Alas de pollo (fritas, unidad)", CategoriaId = Guid.Parse("CB120002-33C9-45EB-B475-437E96E8DA49"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("E984D4E9-F9D8-46B7-B078-3375237E2865"), Calorias = 149m, Nombre = "Ajo (cabeza)", CategoriaId = Guid.Parse("63F881A9-B533-410B-BC4C-0093990F9DA1"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("B02563F3-5AC0-4724-851A-34F038E6181A"), Calorias = 450m, Nombre = "Granola de avena", CategoriaId = Guid.Parse("C8EE09E0-8EEC-4F25-95AF-3AB6235578DA"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("9ABA557F-378F-4ADD-9613-3547D5EF99F1"), Calorias = 130m, Nombre = "Riñones de cordero", CategoriaId = Guid.Parse("313DE149-AA99-48FF-8D2A-1F6A778469AD"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("9E4887E1-9191-4D9F-8004-370F6BEE057F"), Calorias = 280m, Nombre = "Helado de pistacho (taza)", CategoriaId = Guid.Parse("399A37BB-4703-40E1-941F-369B2D51D952"), UnidadId = 2, CantidadValor = 1m },
                new { Id = Guid.Parse("137F99B9-1892-4D8B-AE5B-374000A8609D"), Calorias = 200m, Nombre = "Muslo de pollo (con piel, asado)", CategoriaId = Guid.Parse("CB120002-33C9-45EB-B475-437E96E8DA49"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("F56C77A2-5D9F-415B-8DF4-39DF372072D0"), Calorias = 90m, Nombre = "Abadejo (cocido)", CategoriaId = Guid.Parse("D9128E81-1D35-4418-824D-34432C572DFF"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("9551C29E-7286-4B8D-AAC1-3B517E4006DE"), Calorias = 580m, Nombre = "Pistachos molidos (harina)", CategoriaId = Guid.Parse("399A37BB-4703-40E1-941F-369B2D51D952"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("584FB1AC-97D2-423E-940E-3BD2904D087B"), Calorias = 360m, Nombre = "Granos de cebada integral", CategoriaId = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("E2763D60-37F6-4B22-8335-3DE96DC739E5"), Calorias = 80m, Nombre = "Panga (cocida)", CategoriaId = Guid.Parse("D9128E81-1D35-4418-824D-34432C572DFF"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("4D781E95-713E-4B77-BBB8-3E45ADD02D5E"), Calorias = 120m, Nombre = "Aceite de pistacho (cda)", CategoriaId = Guid.Parse("399A37BB-4703-40E1-941F-369B2D51D952"), UnidadId = 4, CantidadValor = 1m },
                new { Id = Guid.Parse("FEA6F29C-BA79-4928-B698-3F79624A3FE2"), Calorias = 600m, Nombre = "Harina de almendra", CategoriaId = Guid.Parse("08082E0E-3DB0-4FC5-82AD-6BE43D1D8B96"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("60642821-B6B3-442F-BDFB-4130BD166674"), Calorias = 150m, Nombre = "Arroz para sushi (cocido)", CategoriaId = Guid.Parse("5F46DCC2-156D-4EC4-8854-0E16FB78308B"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("4BBCF803-0372-4515-B700-42749A77E1BA"), Calorias = 35m, Nombre = "Galletas de arroz inflado", CategoriaId = Guid.Parse("5F46DCC2-156D-4EC4-8854-0E16FB78308B"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("39CF5260-C2DE-4C76-9A52-429E9613440C"), Calorias = 20m, Nombre = "Cereal en caja", CategoriaId = Guid.Parse("A185F37C-EC77-4037-BF7F-0DF4555E58B9"), UnidadId = 1, CantidadValor = 10m },
                new { Id = Guid.Parse("9B725F6C-7263-4918-B8EF-45E60F7DB85D"), Calorias = 167m, Nombre = "Hígado de pollo", CategoriaId = Guid.Parse("CB120002-33C9-45EB-B475-437E96E8DA49"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("B81BCA56-AA99-4493-821A-46D149023DA0"), Calorias = 579m, Nombre = "Almendras crudas (enteras)", CategoriaId = Guid.Parse("08082E0E-3DB0-4FC5-82AD-6BE43D1D8B96"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("C1B572FE-F086-41D0-B0F3-498C36FC536F"), Calorias = 250m, Nombre = "Papa pre-frita congelada", CategoriaId = Guid.Parse("3B96F79B-7488-479F-8A43-263EBA3C9077"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("348D47AB-6CE8-40B6-B219-4B757240370F"), Calorias = 205m, Nombre = "Caballa (en conserva)", CategoriaId = Guid.Parse("D9128E81-1D35-4418-824D-34432C572DFF"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("D7F586C8-FBCF-40A8-A30C-51CFF2745371"), Calorias = 1m, Nombre = "Cebada virtual", CategoriaId = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), UnidadId = 1, CantidadValor = 0m },
                new { Id = Guid.Parse("09103B00-9D4C-49C6-995A-51E8928FF585"), Calorias = 90m, Nombre = "Lenguado a la plancha", CategoriaId = Guid.Parse("D9128E81-1D35-4418-824D-34432C572DFF"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("0E6F9613-7CA8-4D16-ACA9-520C17561219"), Calorias = 87m, Nombre = "Papa cocida (con piel)", CategoriaId = Guid.Parse("3B96F79B-7488-479F-8A43-263EBA3C9077"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("69E956B2-565F-4415-A271-542AAF3A2742"), Calorias = 111m, Nombre = "Arroz integral cocido", CategoriaId = Guid.Parse("5F46DCC2-156D-4EC4-8854-0E16FB78308B"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("38E19AA6-10BE-4988-AF91-545190DF1990"), Calorias = 90m, Nombre = "Leche de pistacho (vaso)", CategoriaId = Guid.Parse("399A37BB-4703-40E1-941F-369B2D51D952"), UnidadId = 2, CantidadValor = 1m },
                new { Id = Guid.Parse("B354BFB1-BBD0-476D-9F4B-5464664EF818"), Calorias = 160m, Nombre = "Yuca (mandioca)", CategoriaId = Guid.Parse("63F881A9-B533-410B-BC4C-0093990F9DA1"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("18018540-A0EC-4A86-806F-5772F99B58DF"), Calorias = 61m, Nombre = "Puerro", CategoriaId = Guid.Parse("63F881A9-B533-410B-BC4C-0093990F9DA1"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("48555EB5-6F17-4321-82AE-5A032A3C6CB5"), Calorias = 150m, Nombre = "Turrón de pistacho (porción)", CategoriaId = Guid.Parse("399A37BB-4703-40E1-941F-369B2D51D952"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("819A5089-8949-488B-ACB2-5A6A9732F116"), Calorias = 150m, Nombre = "Cerveza de cebada (1 lata)", CategoriaId = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), UnidadId = 2, CantidadValor = 1m },
                new { Id = Guid.Parse("339F2B58-A695-49FA-B82F-5FEA93BCBC8B"), Calorias = 165m, Nombre = "Pechuga de pollo (sin piel, cocida)", CategoriaId = Guid.Parse("CB120002-33C9-45EB-B475-437E96E8DA49"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("6FEB328C-3581-444D-A243-60E8E07ED959"), Calorias = 120m, Nombre = "Leche de arroz (vaso)", CategoriaId = Guid.Parse("5F46DCC2-156D-4EC4-8854-0E16FB78308B"), UnidadId = 2, CantidadValor = 1m },
                new { Id = Guid.Parse("C8F3360A-AE34-47A3-BAF9-61E279AAE849"), Calorias = 86m, Nombre = "Merluza (cocida)", CategoriaId = Guid.Parse("D9128E81-1D35-4418-824D-34432C572DFF"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("42BFB9AA-C46D-4117-A56E-632A8B16BAF6"), Calorias = 150m, Nombre = "Hojuelas de maíz (ración)", CategoriaId = Guid.Parse("A185F37C-EC77-4037-BF7F-0DF4555E58B9"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("80D53D51-96D7-495C-9DC2-63AE37DE67CA"), Calorias = 585m, Nombre = "Almendras tostadas con sal", CategoriaId = Guid.Parse("08082E0E-3DB0-4FC5-82AD-6BE43D1D8B96"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("0666D264-6842-4ECF-AD0C-646EB15B9BB1"), Calorias = 410m, Nombre = "Muesli con avena", CategoriaId = Guid.Parse("C8EE09E0-8EEC-4F25-95AF-3AB6235578DA"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("136E2AEC-02E9-4A3E-913E-65DDE5901C3C"), Calorias = 1m, Nombre = "Cebada Generica", CategoriaId = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("0CF67FC8-BF21-4999-988C-661475261A9A"), Calorias = 380m, Nombre = "Cebada tostada", CategoriaId = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("C5FF1F22-1075-4D6D-8AEF-696B7DB6EF9C"), Calorias = 96m, Nombre = "Tilapia (filete)", CategoriaId = Guid.Parse("D9128E81-1D35-4418-824D-34432C572DFF"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("B37FACA1-86E1-4269-B01B-69AB2EFA0040"), Calorias = 378m, Nombre = "Mijo", CategoriaId = Guid.Parse("A185F37C-EC77-4037-BF7F-0DF4555E58B9"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("4C8825B9-B0FA-4595-A242-6AF8B4B0D41C"), Calorias = 120m, Nombre = "Dorada (a la sal)", CategoriaId = Guid.Parse("D9128E81-1D35-4418-824D-34432C572DFF"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("2EBE85CF-A3DC-4C82-9E6C-6BF65039F5CE"), Calorias = 135m, Nombre = "Pavo (pechuga, cocida)", CategoriaId = Guid.Parse("CB120002-33C9-45EB-B475-437E96E8DA49"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("975B0575-F99A-42A9-BE76-715A509FEA59"), Calorias = 20m, Nombre = "Lechuga", CategoriaId = Guid.Parse("83F2DEA9-5A6C-43A0-9DAC-B4790611B524"), UnidadId = 1, CantidadValor = 5m },
                new { Id = Guid.Parse("E7D93511-CE8F-4F1F-BF76-72C002406C5C"), Calorias = 1m, Nombre = "Cebada especial", CategoriaId = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("02E196F1-1B17-45FE-9A8F-731651F53F8B"), Calorias = 43m, Nombre = "Remolacha cruda", CategoriaId = Guid.Parse("63F881A9-B533-410B-BC4C-0093990F9DA1"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("427DF732-0307-4788-BBB9-7681C88E2B6E"), Calorias = 250m, Nombre = "Paletilla de cordero (cruda)", CategoriaId = Guid.Parse("313DE149-AA99-48FF-8D2A-1F6A778469AD"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("A7723DEA-D5DF-4DCA-ABFA-768D0F0296A7"), Calorias = 100m, Nombre = "Raiz seca", CategoriaId = Guid.Parse("63F881A9-B533-410B-BC4C-0093990F9DA1"), UnidadId = 1, CantidadValor = 10m },
                new { Id = Guid.Parse("24E77645-5D8F-4F08-B020-76AB6C73D428"), Calorias = 10m, Nombre = "Raices verdes", CategoriaId = Guid.Parse("63F881A9-B533-410B-BC4C-0093990F9DA1"), UnidadId = 1, CantidadValor = 100m },
                new { Id = Guid.Parse("398D2C02-7603-4FBF-B81F-78073B22367E"), Calorias = 90m, Nombre = "Papa dulce (batata) asada", CategoriaId = Guid.Parse("3B96F79B-7488-479F-8A43-263EBA3C9077"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("10D5C593-C1CA-40C9-A487-790B65CE1CCD"), Calorias = 100m, Nombre = "Avena instantánea (sobre)", CategoriaId = Guid.Parse("A185F37C-EC77-4037-BF7F-0DF4555E58B9"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("B48F2CB9-9BB1-41B7-80C0-795D33739166"), Calorias = 550m, Nombre = "Chocolate con almendras (barra)", CategoriaId = Guid.Parse("08082E0E-3DB0-4FC5-82AD-6BE43D1D8B96"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("F49A0893-349D-4D2B-A8DB-7B4088D9CF48"), Calorias = 338m, Nombre = "Espelta", CategoriaId = Guid.Parse("A185F37C-EC77-4037-BF7F-0DF4555E58B9"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("20774D7B-7BF1-4B4C-B4A9-7E17F1509634"), Calorias = 280m, Nombre = "Pierna de cordero asada", CategoriaId = Guid.Parse("313DE149-AA99-48FF-8D2A-1F6A778469AD"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("37070AD9-EB9F-4B41-BC98-7E49671FF88B"), Calorias = 1m, Nombre = "Cebada Generica", CategoriaId = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("A98F6A71-C06E-4B59-807B-7F46389D50C2"), Calorias = 370m, Nombre = "Avena cortada (steel-cut)", CategoriaId = Guid.Parse("C8EE09E0-8EEC-4F25-95AF-3AB6235578DA"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("41F248DA-9992-400F-824B-8462FB03F9B8"), Calorias = 120m, Nombre = "Barra de cereal de avena", CategoriaId = Guid.Parse("C8EE09E0-8EEC-4F25-95AF-3AB6235578DA"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("3C8ADDF2-9894-42B0-9047-86029D2BE60C"), Calorias = 370m, Nombre = "Avena instantánea", CategoriaId = Guid.Parse("C8EE09E0-8EEC-4F25-95AF-3AB6235578DA"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("EC83D6D6-24DD-4EF8-A21F-89BA0F07BE3B"), Calorias = 520m, Nombre = "Snack de maní con miel", CategoriaId = Guid.Parse("F6FE0472-FDE5-439F-9921-3F7DDBCA926E"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("418F611F-C6D9-4A28-B1FD-8A0385CBBE77"), Calorias = 587m, Nombre = "Maní tostado y salado", CategoriaId = Guid.Parse("F6FE0472-FDE5-439F-9921-3F7DDBCA926E"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("DE511E70-D867-4D08-8C5E-8A59429390ED"), Calorias = 350m, Nombre = "Chuleta de cordero (cocida)", CategoriaId = Guid.Parse("313DE149-AA99-48FF-8D2A-1F6A778469AD"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("01CEE8AD-D60C-43FD-9337-8AB3CC3820F2"), Calorias = 48m, Nombre = "Rábano picante", CategoriaId = Guid.Parse("63F881A9-B533-410B-BC4C-0093990F9DA1"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("1B7BCDA4-F957-4CF7-986A-8CA671735DF5"), Calorias = 200m, Nombre = "Sopa de cebada (ración)", CategoriaId = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), UnidadId = 2, CantidadValor = 1m },
                new { Id = Guid.Parse("40A55F73-494B-432B-A4B9-938DCA1D9244"), Calorias = 180m, Nombre = "Barra energética con pistachos", CategoriaId = Guid.Parse("399A37BB-4703-40E1-941F-369B2D51D952"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("39B34FF7-7B3A-43F1-B7D1-9B8D8DA63503"), Calorias = 567m, Nombre = "Maní crudo", CategoriaId = Guid.Parse("F6FE0472-FDE5-439F-9921-3F7DDBCA926E"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("885EFEEB-024D-4E01-8C26-9FF261B8FB53"), Calorias = 345m, Nombre = "Harina de cebada", CategoriaId = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("B031B461-AC43-4F27-91D0-A0A938E9A931"), Calorias = 150m, Nombre = "Carne de pavo ahumada", CategoriaId = Guid.Parse("A22302FA-057B-4603-A68D-49A7816CE1FE"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("6EE11798-09AF-4F2C-A299-A21BF749161F"), Calorias = 83m, Nombre = "Puré de papa", CategoriaId = Guid.Parse("3B96F79B-7488-479F-8A43-263EBA3C9077"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("35566955-B8E8-42EF-962D-A3190BC045EC"), Calorias = 1m, Nombre = "Cebada mediana", CategoriaId = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), UnidadId = 1, CantidadValor = 0m },
                new { Id = Guid.Parse("4F7F1592-8AD0-40DC-98D9-A33CAF68D91B"), Calorias = 50m, Nombre = "Consomé de pavo (taza)", CategoriaId = Guid.Parse("A22302FA-057B-4603-A68D-49A7816CE1FE"), UnidadId = 2, CantidadValor = 1m },
                new { Id = Guid.Parse("0A5B2076-F3E6-4AAD-8106-A3C385108531"), Calorias = 371m, Nombre = "Amaranto (semilla)", CategoriaId = Guid.Parse("A185F37C-EC77-4037-BF7F-0DF4555E58B9"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("88CEF06B-ADF4-4802-A9D6-A3CDFB46629B"), Calorias = 120m, Nombre = "Aceite de maní (cda)", CategoriaId = Guid.Parse("F6FE0472-FDE5-439F-9921-3F7DDBCA926E"), UnidadId = 4, CantidadValor = 1m },
                new { Id = Guid.Parse("1159F281-612A-4C07-A955-A4EEB8A11CC8"), Calorias = 450m, Nombre = "Mazapán (porción)", CategoriaId = Guid.Parse("08082E0E-3DB0-4FC5-82AD-6BE43D1D8B96"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("16D7CC90-88AD-4F7B-A514-A7EF03302E31"), Calorias = 220m, Nombre = "Brocheta de cordero", CategoriaId = Guid.Parse("313DE149-AA99-48FF-8D2A-1F6A778469AD"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("F32C48D8-60B2-440B-A8AD-AAC2F6C04991"), Calorias = 337m, Nombre = "Carne de pato (asada)", CategoriaId = Guid.Parse("CB120002-33C9-45EB-B475-437E96E8DA49"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("8AB040A4-1F4E-4FFF-824F-ACDC88C27DE7"), Calorias = 570m, Nombre = "Pistachos verdes (pelados)", CategoriaId = Guid.Parse("399A37BB-4703-40E1-941F-369B2D51D952"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("4E0F040B-A05A-40B9-B4FC-AD957EA99F64"), Calorias = 180m, Nombre = "Gajo de papa especiado", CategoriaId = Guid.Parse("3B96F79B-7488-479F-8A43-263EBA3C9077"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("C815BFA5-1C3A-420F-B013-B1EBB8096AF8"), Calorias = 370m, Nombre = "Malta de cebada", CategoriaId = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("C430D1D8-AD26-4AA6-8304-B3B0EE00D60B"), Calorias = 290m, Nombre = "Lomo de cordero", CategoriaId = Guid.Parse("313DE149-AA99-48FF-8D2A-1F6A778469AD"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("A8BEA2E8-E92D-4029-95D6-B559DB6D2A42"), Calorias = 389m, Nombre = "Hojuelas de avena tradicional", CategoriaId = Guid.Parse("C8EE09E0-8EEC-4F25-95AF-3AB6235578DA"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("58BA849A-FD32-44C5-A321-B70C740A9247"), Calorias = 350m, Nombre = "Papa deshidratada (copos)", CategoriaId = Guid.Parse("3B96F79B-7488-479F-8A43-263EBA3C9077"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("3BC78B5B-8A70-480C-A372-B7D150DCE38D"), Calorias = 30m, Nombre = "Jamón de pavo (rebanada)", CategoriaId = Guid.Parse("A22302FA-057B-4603-A68D-49A7816CE1FE"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("C225537E-7A21-47E3-AD91-B944B13AF3A2"), Calorias = 50m, Nombre = "Galletas de avena (unidad)", CategoriaId = Guid.Parse("C8EE09E0-8EEC-4F25-95AF-3AB6235578DA"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("B8B4F105-FF73-4DD1-A0F6-BB0BE28884A0"), Calorias = 110m, Nombre = "Bombón de maní y chocolate", CategoriaId = Guid.Parse("F6FE0472-FDE5-439F-9921-3F7DDBCA926E"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("E4F52E8D-F0F3-4325-AC38-BC2F7FF39E09"), Calorias = 350m, Nombre = "Arroz basmati crudo", CategoriaId = Guid.Parse("5F46DCC2-156D-4EC4-8854-0E16FB78308B"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("73189ACF-C313-44DC-9778-BD3795D134C3"), Calorias = 130m, Nombre = "Leche de avena (vaso)", CategoriaId = Guid.Parse("C8EE09E0-8EEC-4F25-95AF-3AB6235578DA"), UnidadId = 2, CantidadValor = 1m },
                new { Id = Guid.Parse("D79EBF22-BBF0-4343-9DB9-BF6953AE83F7"), Calorias = 270m, Nombre = "Carne picada de cordero", CategoriaId = Guid.Parse("313DE149-AA99-48FF-8D2A-1F6A778469AD"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("103B33E9-F1A3-431C-BF3A-C279926384B7"), Calorias = 380m, Nombre = "Harina de maní desgrasada", CategoriaId = Guid.Parse("F6FE0472-FDE5-439F-9921-3F7DDBCA926E"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("45F81D79-EC4C-4464-94E7-C81405DDC38C"), Calorias = 250m, Nombre = "Taco de pavo (unidad)", CategoriaId = Guid.Parse("A22302FA-057B-4603-A68D-49A7816CE1FE"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("07C33A71-D9A3-48A0-976E-C85A70BD6889"), Calorias = 180m, Nombre = "Pavo molido", CategoriaId = Guid.Parse("CB120002-33C9-45EB-B475-437E96E8DA49"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("FA20105E-58BD-4BA2-983C-CEC6FCF75E52"), Calorias = 130m, Nombre = "Arroz blanco cocido", CategoriaId = Guid.Parse("5F46DCC2-156D-4EC4-8854-0E16FB78308B"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("D11916B6-2F8D-45F8-BE2D-CED10CEF4D3F"), Calorias = 90m, Nombre = "Pan de cebada (rebanada)", CategoriaId = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("B159EC00-D462-4AF4-B78B-D0714AA29D19"), Calorias = 120m, Nombre = "Sopa de pollo (taza)", CategoriaId = Guid.Parse("CB120002-33C9-45EB-B475-437E96E8DA49"), UnidadId = 2, CantidadValor = 1m },
                new { Id = Guid.Parse("BC4C3944-2ECC-46E2-B4D0-D40F39ADAE62"), Calorias = 480m, Nombre = "Maní confitado", CategoriaId = Guid.Parse("F6FE0472-FDE5-439F-9921-3F7DDBCA926E"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("AA440882-F4C5-426E-ABB0-D4D95A8AE689"), Calorias = 200m, Nombre = "Pavo mechado (carne oscura)", CategoriaId = Guid.Parse("A22302FA-057B-4603-A68D-49A7816CE1FE"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("D70553CA-F0D4-4E26-B731-D5BDA3E3432B"), Calorias = 380m, Nombre = "Costillar de cordero", CategoriaId = Guid.Parse("313DE149-AA99-48FF-8D2A-1F6A778469AD"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("4200520D-DCF4-424B-8B07-D9459E3E2F11"), Calorias = 41m, Nombre = "Zanahoria fresca", CategoriaId = Guid.Parse("63F881A9-B533-410B-BC4C-0093990F9DA1"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("EC852F5C-9666-482F-A2BD-D952FDC53548"), Calorias = 246m, Nombre = "Salvado de avena", CategoriaId = Guid.Parse("C8EE09E0-8EEC-4F25-95AF-3AB6235578DA"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("4D091F12-1A1D-4EE3-9D80-E2E7F5AC135C"), Calorias = 120m, Nombre = "Aceite de almendra (cda)", CategoriaId = Guid.Parse("08082E0E-3DB0-4FC5-82AD-6BE43D1D8B96"), UnidadId = 4, CantidadValor = 1m },
                new { Id = Guid.Parse("ECE53D46-E579-4F04-A7C7-E62E51FB6FE3"), Calorias = 70m, Nombre = "Albóndiga de pavo (unidad)", CategoriaId = Guid.Parse("A22302FA-057B-4603-A68D-49A7816CE1FE"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("CD365A29-D84F-4CA1-A854-E6A6887E9DA4"), Calorias = 40m, Nombre = "Cebolla blanca", CategoriaId = Guid.Parse("63F881A9-B533-410B-BC4C-0093990F9DA1"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("A723CBEB-2733-4559-8590-E946072010C6"), Calorias = 28m, Nombre = "Nabo cocido", CategoriaId = Guid.Parse("63F881A9-B533-410B-BC4C-0093990F9DA1"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("98746E75-69FC-4C53-A7B5-EA56DF561BBD"), Calorias = 80m, Nombre = "Jengibre (raíz)", CategoriaId = Guid.Parse("63F881A9-B533-410B-BC4C-0093990F9DA1"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("62511AE8-8BD6-4A2C-9F4F-EBCFE6E34F44"), Calorias = 180m, Nombre = "Salchicha de pavo (unidad)", CategoriaId = Guid.Parse("A22302FA-057B-4603-A68D-49A7816CE1FE"), UnidadId = 3, CantidadValor = 1m },
                new { Id = Guid.Parse("2403700C-3916-4855-830E-F2687EB0A84F"), Calorias = 93m, Nombre = "Papa asada", CategoriaId = Guid.Parse("3B96F79B-7488-479F-8A43-263EBA3C9077"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("D56568E1-EEAF-4D43-9BD5-F2E0B24892C9"), Calorias = 580m, Nombre = "Granos de almendra (pelados)", CategoriaId = Guid.Parse("08082E0E-3DB0-4FC5-82AD-6BE43D1D8B96"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("BB2704CA-6B63-4992-8033-F346A14002DF"), Calorias = 342m, Nombre = "Bulgur", CategoriaId = Guid.Parse("A185F37C-EC77-4037-BF7F-0DF4555E58B9"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("F9C2F136-8388-4166-88D3-F66079C6384E"), Calorias = 250m, Nombre = "Arroz frito (porción)", CategoriaId = Guid.Parse("5F46DCC2-156D-4EC4-8854-0E16FB78308B"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("C5C6992A-08C3-471B-865F-F67A7EEBE748"), Calorias = 1m, Nombre = "Cebada Generica", CategoriaId = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), UnidadId = 1, CantidadValor = 0m },
                new { Id = Guid.Parse("92E2A7CA-BBE9-4941-9688-F79C21B07FA0"), Calorias = 230m, Nombre = "Gallina cocida", CategoriaId = Guid.Parse("CB120002-33C9-45EB-B475-437E96E8DA49"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("8AC032D5-7AA2-400B-B0AA-F969FE44A99B"), Calorias = 368m, Nombre = "Quinoa cruda", CategoriaId = Guid.Parse("A185F37C-EC77-4037-BF7F-0DF4555E58B9"), UnidadId = 1, CantidadValor = 1m },
                new { Id = Guid.Parse("D45B59F8-A4B1-4F87-B044-FAD5A958EB38"), Calorias = 90m, Nombre = "Mantequilla de pistacho (cda)", CategoriaId = Guid.Parse("399A37BB-4703-40E1-941F-369B2D51D952"), UnidadId = 4, CantidadValor = 1m },
                new { Id = Guid.Parse("F6576E63-AB6D-4796-BB8B-FB476545EFFD"), Calorias = 1m, Nombre = "Cebada virtual", CategoriaId = Guid.Parse("47C51B84-BB85-49F6-96A1-0701D7144A0E"), UnidadId = 1, CantidadValor = 0m },
                new { Id = Guid.Parse("8D5C9A34-BB53-4B8C-88BF-FC399B1D04B1"), Calorias = 190m, Nombre = "Mantequilla de maní (2 cdas)", CategoriaId = Guid.Parse("F6FE0472-FDE5-439F-9921-3F7DDBCA926E"), UnidadId = 4, CantidadValor = 1m },
                new { Id = Guid.Parse("E6FB9734-804F-45F9-8141-FD18D274BF10"), Calorias = 312m, Nombre = "Papa frita (patatas fritas)", CategoriaId = Guid.Parse("3B96F79B-7488-479F-8A43-263EBA3C9077"), UnidadId = 1, CantidadValor = 1m }
             );

            modelBuilder.Entity<Nutricionista>().HasData(
                new { Id = Guid.Parse("4224DE05-C1B1-424D-4D22-08DE14A4DB50"), Nombre = "Daniel Román", Activo = false, FechaCreacion = new DateTime(2024, 1, 1) },
                new { Id = Guid.Parse("4CE7D823-87F3-4181-B936-08DE14A787B0"), Nombre = "Ricardo Mena", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 15, 51, 11) },
                new { Id = Guid.Parse("7A918AFC-94BB-46D7-7155-08DE1746E629"), Nombre = "Jorge Parra", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 23, 57, 10) },
                new { Id = Guid.Parse("A0C733F5-3F18-4176-114E-08DE181E81FB"), Nombre = "Carlos Javier", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 01, 40, 34) },
                new { Id = Guid.Parse("D1F1DEA7-5596-4399-DAF3-08DE350D21EE"), Nombre = "Enzo Fernández", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 21, 19, 05) },
                new { Id = Guid.Parse("87BA4A90-3EFA-4D27-8692-0BFD86DAF6BD"), Nombre = "Valeria Gómez", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 03, 25, 15) },
                new { Id = Guid.Parse("6C54B71F-4A1B-47C1-ACA4-0CECA607D5EB"), Nombre = "Andrés Castro", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 21, 47, 20) },
                new { Id = Guid.Parse("1C2F7051-D0E9-454D-8653-19542C7AA5BF"), Nombre = "Mariana Ríos", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 02, 53, 19) },
                new { Id = Guid.Parse("5A2FE591-701B-4324-ACFE-252DE1EFDD9D"), Nombre = "Lucía Méndez", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 02, 24, 43) },
                new { Id = Guid.Parse("46D0DFDF-5687-4042-88BA-37E7B2EBBE0B"), Nombre = "Sergio Torres", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 14, 55, 08) },
                new { Id = Guid.Parse("3237E879-17CD-4F03-A625-41A572DB6BE7"), Nombre = "Beatriz Luna", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 21, 45, 21) },
                new { Id = Guid.Parse("090B93FF-E07C-4187-BFC9-54493B6CDFBF"), Nombre = "Fernando Soto", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 21, 36, 53) },
                new { Id = Guid.Parse("54F96058-C83F-4681-8D30-65A53CF86BD2"), Nombre = "Karla Espinoza", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 21, 37, 52) },
                new { Id = Guid.Parse("8163C819-2F48-47CD-AA47-7BC4C7FF5D98"), Nombre = "Chef Ranci", Activo = true, FechaCreacion = new DateTime(2024, 1, 1) },
                new { Id = Guid.Parse("D3B13C4A-67FA-4FCE-B915-83EB26E1C939"), Nombre = "Chef Ranci Professional", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 18, 20, 54) },
                new { Id = Guid.Parse("185132BE-5513-4653-B8B6-93625438D04E"), Nombre = "Paola Argüello", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 15, 42, 05) },
                new { Id = Guid.Parse("40278D23-3911-48D5-9E6D-9B26DC61E93B"), Nombre = "Roberto Villalba", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 15, 00, 41) },
                new { Id = Guid.Parse("B608BCCE-4290-4789-81E9-CDFA957402BB"), Nombre = "María Delgado", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 16, 09, 12) },
                new { Id = Guid.Parse("12BF19A5-B30C-49EE-B75C-D9E8769FB05F"), Nombre = "Luigi Vampa", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 16, 05, 51) },
                new { Id = Guid.Parse("AB971254-D489-452A-B809-F3D29D7CEB88"), Nombre = "Elena Martínez", Activo = true, FechaCreacion = new DateTime(2024, 1, 1, 02, 53, 19) }
            );
            modelBuilder.Entity<Paciente>().HasData(
                new { Id = Guid.Parse("DE44922E-B41A-46A3-0245-08DE14C94AA7"), Nombre = "Ariana", Apellido = "Grande", FechaNacimiento = new DateTime(2000, 10, 26), Peso = 76m, Altura = 167m },
                new { Id = Guid.Parse("D2740F5A-ABE1-4A0B-A300-08DE1746D733"), Nombre = "Pedro", Apellido = "Chavez", FechaNacimiento = new DateTime(2019, 10, 29), Peso = 110m, Altura = 180m },
                new { Id = Guid.Parse("9F972A0E-3153-4B9B-98CC-08DE181E9EFC"), Nombre = "Juan", Apellido = "Carlos Duran", FechaNacimiento = new DateTime(2005, 10, 31), Peso = 110m, Altura = 170m },
                new { Id = Guid.Parse("7CD40A9B-5BF2-4DBF-B9EE-0C1093B306C0"), Nombre = "Ricardo", Apellido = "Sosa", FechaNacimiento = new DateTime(2026, 3, 6), Peso = 3.5m, Altura = 50m },
                new { Id = Guid.Parse("7F1C4C1E-5A54-4C4C-9E15-0C6C1B8D4D55"), Nombre = "Maria", Apellido = "Elena", FechaNacimiento = new DateTime(1985, 11, 22), Peso = 65m, Altura = 160m },
                new { Id = Guid.Parse("9D1E822C-FFB9-4009-B16E-0F4259B2FA1A"), Nombre = "Juan", Apellido = "Perez", FechaNacimiento = new DateTime(2026, 1, 19), Peso = 50m, Altura = 70m },
                new { Id = Guid.Parse("12B9C6E2-34D7-4FB2-B5D1-119773A6834F"), Nombre = "Roberto", Apellido = "Guzman", FechaNacimiento = new DateTime(2026, 2, 19), Peso = 4.2m, Altura = 52m },
                new { Id = Guid.Parse("E4B8E3D5-63F3-482F-979A-1256427B09E1"), Nombre = "Chris", Apellido = "Walker", FechaNacimiento = new DateTime(1987, 06, 18), Peso = 74m, Altura = 185m },
                new { Id = Guid.Parse("C06A5A28-4B9A-4812-BB37-1658BFB58B7E"), Nombre = "Layne", Apellido = "Fahey", FechaNacimiento = new DateTime(1979, 02, 13), Peso = 103m, Altura = 207m },
                new { Id = Guid.Parse("8FE998C5-BF70-49F9-BA34-17149B9AEF8D"), Nombre = "Esteban", Apellido = "Quito", FechaNacimiento = new DateTime(2026, 01, 20), Peso = 100m, Altura = 160m },
                new { Id = Guid.Parse("1183D1B1-6C9A-4264-AFE1-28B020225B39"), Nombre = "Juan", Apellido = "Calisalla", FechaNacimiento = new DateTime(2026, 02, 28), Peso = 70m, Altura = 175m },
                new { Id = Guid.Parse("90EECB08-DA56-48B3-917D-2F80F55701F5"), Nombre = "Monica", Apellido = "Suarez", FechaNacimiento = new DateTime(2026, 03, 06), Peso = 58m, Altura = 162m },
                new { Id = Guid.Parse("A2B10DB8-AAD2-4F62-81FF-309B24B53402"), Nombre = "Layne", Apellido = "Fahey", FechaNacimiento = new DateTime(1979, 02, 13), Peso = 103m, Altura = 207m },
                new { Id = Guid.Parse("41234E45-8FD3-4992-A7FF-3330FFEA60AD"), Nombre = "Davos", Apellido = "Villarruel", FechaNacimiento = new DateTime(2026, 03, 05), Peso = 10m, Altura = 10m },
                new { Id = Guid.Parse("1C6BBD23-A625-4439-BB7C-34E52B7AE2C2"), Nombre = "Gino", Apellido = "Spinka", FechaNacimiento = new DateTime(1957, 11, 09), Peso = 108m, Altura = 201m }
            );

            modelBuilder.Entity<Receta>().HasData(
             // Lunch / Almuerzo (TiempoId 2)
                new { Id = Guid.Parse("A1111111-1111-1111-1111-111111111111"), Nombre = "Majadito de Charque", Instrucciones = "Sofreír el charque, añadir arroz y colorante, cocinar hasta que esté seco.", TiempoId = 2 },
                new { Id = Guid.Parse("B2222222-2222-2222-2222-222222222222"), Nombre = "Pollo Frito Crujiente", Instrucciones = "Sazonar el pollo, pasar por harina y freír en abundante aceite.", TiempoId = 2 },
                new { Id = Guid.Parse("C3333333-3333-3333-3333-333333333333"), Nombre = "Arroz con Pollo", Instrucciones = "Cocinar el arroz con presas de pollo y vegetales picados.", TiempoId = 2 },

                // Breakfast / Desayuno (TiempoId 1)
                new { Id = Guid.Parse("D4444444-4444-4444-4444-444444444444"), Nombre = "Avena con Manzana", Instrucciones = "Cocer la avena en leche de avena con trozos de manzana.", TiempoId = 1 },
                new { Id = Guid.Parse("E5555555-5555-5555-5555-555555555555"), Nombre = "Tostadas de Cebada", Instrucciones = "Tostar pan de cebada y untar con mantequilla de maní.", TiempoId = 1 },

                // Half Morning / Media Mañana (TiempoId 3)
                new { Id = Guid.Parse("F6666666-6666-6666-6666-666666666666"), Nombre = "Snack de Frutos Secos", Instrucciones = "Mezclar pistachos, maní tostado y almendras.", TiempoId = 3 },
                new { Id = Guid.Parse("17777777-7777-7777-7777-777777777777"), Nombre = "Barrita Energética Casera", Instrucciones = "Mezclar avena tradicional con miel y trozos de chocolate.", TiempoId = 3 },

                // Dinner / Cena (TiempoId 4)
                new { Id = Guid.Parse("28888888-8888-8888-8888-888888888888"), Nombre = "Chuletas de Cordero al Horno", Instrucciones = "Marinar el cordero con especias y hornear con papas.", TiempoId = 4 },
                new { Id = Guid.Parse("39999999-9999-9999-9999-999999999999"), Nombre = "Sopa de Cebada y Pollo", Instrucciones = "Hervir el pollo con cebada y vegetales hasta ablandar.", TiempoId = 4 },
                new { Id = Guid.Parse("4AAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"), Nombre = "Pavo Mechado Liviano", Instrucciones = "Desmenuzar pavo cocido y servir con puré de papa.", TiempoId = 4 }
            );
            modelBuilder.Entity<RecetaIngrediente>().HasData(
                // Ingredients for Majadito
                new { Id = 1, RecetaId = Guid.Parse("A1111111-1111-1111-1111-111111111111"), IngredienteId = Guid.Parse("FA20105E-58BD-4BA2-983C-CEC6FCF75E52"), CantidadValor = 200m }, // Arroz blanco

                // Ingredients for Pollo Frito
                new { Id = 2, RecetaId = Guid.Parse("B2222222-2222-2222-2222-222222222222"), IngredienteId = Guid.Parse("88CEF06B-ADF4-4802-A9D6-A3CDFB46629B"), CantidadValor = 2m },   // Aceite de maní

                // Ingredients for Avena con Manzana (Breakfast)
                new { Id = 3, RecetaId = Guid.Parse("D4444444-4444-4444-4444-444444444444"), IngredienteId = Guid.Parse("3C8ADDF2-9894-42B0-9047-86029D2BE60C"), CantidadValor = 50m },  // Avena instantánea
                new { Id = 4, RecetaId = Guid.Parse("D4444444-4444-4444-4444-444444444444"), IngredienteId = Guid.Parse("73189ACF-C313-44DC-9778-BD3795D134C3"), CantidadValor = 1m },   // Leche de avena

                // Ingredients for Snack de Frutos Secos (Half Morning)
                new { Id = 5, RecetaId = Guid.Parse("F6666666-6666-6666-6666-666666666666"), IngredienteId = Guid.Parse("8AB040A4-1F4E-4FFF-824F-ACDC88C27DE7"), CantidadValor = 30m },  // Pistachos
                new { Id = 6, RecetaId = Guid.Parse("F6666666-6666-6666-6666-666666666666"), IngredienteId = Guid.Parse("418F611F-C6D9-4A28-B1FD-8A0385CBBE77"), CantidadValor = 20m },  // Maní tostado

                // Ingredients for Dinner (Chuletas)
                new { Id = 7, RecetaId = Guid.Parse("28888888-8888-8888-8888-888888888888"), IngredienteId = Guid.Parse("DE511E70-D867-4D08-8C5E-8A59429390ED"), CantidadValor = 2m },   // Chuleta de cordero
                new { Id = 8, RecetaId = Guid.Parse("28888888-8888-8888-8888-888888888888"), IngredienteId = Guid.Parse("2403700C-3916-4855-830E-F2687EB0A84F"), CantidadValor = 2m }    // Papa asada
            );
        }
        public void Migrate()
        {
            Database.Migrate();
        }
    }
}
