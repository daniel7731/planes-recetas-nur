using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PlanesRecetas.infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.EnsureSchema(
                name: "outbox");

            migrationBuilder.CreateTable(
                name: "Nutricionista",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutricionista", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "outboxMessage",
                schema: "outbox",
                columns: table => new
                {
                    outboxId = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    processed = table.Column<bool>(type: "boolean", nullable: false),
                    processedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    correlationId = table.Column<string>(type: "text", nullable: true),
                    traceId = table.Column<string>(type: "text", nullable: true),
                    spanId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_outboxMessage", x => x.outboxId);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Telefono = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Peso = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Altura = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tiempo",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiempo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoAlimento",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAlimento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadMedida",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Simbolo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadMedida", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanAlimentacion",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    NutricionistaId = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "DATE", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "DATE", nullable: false),
                    DuracionDias = table.Column<int>(type: "integer", nullable: false, computedColumnSql: "\"FechaFin\" - \"FechaInicio\"", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanAlimentacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanAlimentacion_Nutricionista_NutricionistaId",
                        column: x => x.NutricionistaId,
                        principalSchema: "public",
                        principalTable: "Nutricionista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanAlimentacion_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalSchema: "public",
                        principalTable: "Paciente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receta",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Instrucciones = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    TiempoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receta_Tiempo_TiempoId",
                        column: x => x.TiempoId,
                        principalSchema: "public",
                        principalTable: "Tiempo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TipoAlimentoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categoria_TipoAlimento_TipoAlimentoId",
                        column: x => x.TipoAlimentoId,
                        principalSchema: "public",
                        principalTable: "TipoAlimento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dieta",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanAlimentacionId = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaConsumo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dieta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dieta_PlanAlimentacion_PlanAlimentacionId",
                        column: x => x.PlanAlimentacionId,
                        principalSchema: "public",
                        principalTable: "PlanAlimentacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingrediente",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Calorias = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnidadId = table.Column<int>(type: "integer", nullable: false),
                    CantidadValor = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingrediente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingrediente_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalSchema: "public",
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingrediente_UnidadMedida_UnidadId",
                        column: x => x.UnidadId,
                        principalSchema: "public",
                        principalTable: "UnidadMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DietaReceta",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DietaId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecetaId = table.Column<Guid>(type: "uuid", nullable: false),
                    TiempoId = table.Column<int>(type: "integer", nullable: false),
                    Orden = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaReceta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietaReceta_Dieta_DietaId",
                        column: x => x.DietaId,
                        principalSchema: "public",
                        principalTable: "Dieta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DietaReceta_Receta_RecetaId",
                        column: x => x.RecetaId,
                        principalSchema: "public",
                        principalTable: "Receta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DietaReceta_Tiempo_TiempoId",
                        column: x => x.TiempoId,
                        principalSchema: "public",
                        principalTable: "Tiempo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecetaIngrediente",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecetaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IngredienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    CantidadValor = table.Column<decimal>(type: "numeric(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaIngrediente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecetaIngrediente_Ingrediente_IngredienteId",
                        column: x => x.IngredienteId,
                        principalSchema: "public",
                        principalTable: "Ingrediente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaIngrediente_Receta_RecetaId",
                        column: x => x.RecetaId,
                        principalSchema: "public",
                        principalTable: "Receta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Nutricionista",
                columns: new[] { "Id", "Activo", "FechaCreacion", "Nombre" },
                values: new object[,]
                {
                    { new Guid("090b93ff-e07c-4187-bfc9-54493b6cdfbf"), true, new DateTime(2024, 1, 1, 21, 36, 53, 0, DateTimeKind.Utc), "Fernando Soto" },
                    { new Guid("12bf19a5-b30c-49ee-b75c-d9e8769fb05f"), true, new DateTime(2024, 1, 1, 16, 5, 51, 0, DateTimeKind.Utc), "Luigi Vampa" },
                    { new Guid("185132be-5513-4653-b8b6-93625438d04e"), true, new DateTime(2024, 1, 1, 15, 42, 5, 0, DateTimeKind.Utc), "Paola Argüello" },
                    { new Guid("1c2f7051-d0e9-454d-8653-19542c7aa5bf"), true, new DateTime(2024, 1, 1, 2, 53, 19, 0, DateTimeKind.Utc), "Mariana Ríos" },
                    { new Guid("3237e879-17cd-4f03-a625-41a572db6be7"), true, new DateTime(2024, 1, 1, 21, 45, 21, 0, DateTimeKind.Utc), "Beatriz Luna" },
                    { new Guid("40278d23-3911-48d5-9e6d-9b26dc61e93b"), true, new DateTime(2024, 1, 1, 15, 0, 41, 0, DateTimeKind.Utc), "Roberto Villalba" },
                    { new Guid("4224de05-c1b1-424d-4d22-08de14a4db50"), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Daniel Román" },
                    { new Guid("46d0dfdf-5687-4042-88ba-37e7b2ebbe0b"), true, new DateTime(2024, 1, 1, 14, 55, 8, 0, DateTimeKind.Utc), "Sergio Torres" },
                    { new Guid("4ce7d823-87f3-4181-b936-08de14a787b0"), true, new DateTime(2024, 1, 1, 15, 51, 11, 0, DateTimeKind.Utc), "Ricardo Mena" },
                    { new Guid("54f96058-c83f-4681-8d30-65a53cf86bd2"), true, new DateTime(2024, 1, 1, 21, 37, 52, 0, DateTimeKind.Utc), "Karla Espinoza" },
                    { new Guid("5a2fe591-701b-4324-acfe-252de1efdd9d"), true, new DateTime(2024, 1, 1, 2, 24, 43, 0, DateTimeKind.Utc), "Lucía Méndez" },
                    { new Guid("6c54b71f-4a1b-47c1-aca4-0ceca607d5eb"), true, new DateTime(2024, 1, 1, 21, 47, 20, 0, DateTimeKind.Utc), "Andrés Castro" },
                    { new Guid("7a918afc-94bb-46d7-7155-08de1746e629"), true, new DateTime(2024, 1, 1, 23, 57, 10, 0, DateTimeKind.Utc), "Jorge Parra" },
                    { new Guid("8163c819-2f48-47cd-aa47-7bc4c7ff5d98"), true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Chef Ranci" },
                    { new Guid("87ba4a90-3efa-4d27-8692-0bfd86daf6bd"), true, new DateTime(2024, 1, 1, 3, 25, 15, 0, DateTimeKind.Utc), "Valeria Gómez" },
                    { new Guid("a0c733f5-3f18-4176-114e-08de181e81fb"), true, new DateTime(2024, 1, 1, 1, 40, 34, 0, DateTimeKind.Utc), "Carlos Javier" },
                    { new Guid("ab971254-d489-452a-b809-f3d29d7ceb88"), true, new DateTime(2024, 1, 1, 2, 53, 19, 0, DateTimeKind.Utc), "Elena Martínez" },
                    { new Guid("b608bcce-4290-4789-81e9-cdfa957402bb"), true, new DateTime(2024, 1, 1, 16, 9, 12, 0, DateTimeKind.Utc), "María Delgado" },
                    { new Guid("d1f1dea7-5596-4399-daf3-08de350d21ee"), true, new DateTime(2024, 1, 1, 21, 19, 5, 0, DateTimeKind.Utc), "Enzo Fernández" },
                    { new Guid("d3b13c4a-67fa-4fce-b915-83eb26e1c939"), true, new DateTime(2024, 1, 1, 18, 20, 54, 0, DateTimeKind.Utc), "Chef Ranci Professional" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Paciente",
                columns: new[] { "Id", "Altura", "Apellido", "Email", "FechaNacimiento", "Nombre", "Peso", "Telefono" },
                values: new object[,]
                {
                    { new Guid("1183d1b1-6c9a-4264-afe1-28b020225b39"), 175m, "Calisalla", null, new DateTime(2026, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Juan", 70m, null },
                    { new Guid("12b9c6e2-34d7-4fb2-b5d1-119773a6834f"), 52m, "Guzman", null, new DateTime(2026, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Roberto", 4.2m, null },
                    { new Guid("1c6bbd23-a625-4439-bb7c-34e52b7ae2c2"), 201m, "Spinka", null, new DateTime(1957, 11, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Gino", 108m, null },
                    { new Guid("41234e45-8fd3-4992-a7ff-3330ffea60ad"), 10m, "Villarruel", null, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Davos", 10m, null },
                    { new Guid("7cd40a9b-5bf2-4dbf-b9ee-0c1093b306c0"), 50m, "Sosa", null, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Ricardo", 3.5m, null },
                    { new Guid("7f1c4c1e-5a54-4c4c-9e15-0c6c1b8d4d55"), 160m, "Elena", null, new DateTime(1985, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Maria", 65m, null },
                    { new Guid("8fe998c5-bf70-49f9-ba34-17149b9aef8d"), 160m, "Quito", null, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Esteban", 100m, null },
                    { new Guid("90eecb08-da56-48b3-917d-2f80f55701f5"), 162m, "Suarez", null, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Monica", 58m, null },
                    { new Guid("9d1e822c-ffb9-4009-b16e-0f4259b2fa1a"), 70m, "Perez", null, new DateTime(2026, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Juan", 50m, null },
                    { new Guid("9f972a0e-3153-4b9b-98cc-08de181e9efc"), 170m, "Carlos Duran", null, new DateTime(2005, 10, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Juan", 110m, null },
                    { new Guid("a2b10db8-aad2-4f62-81ff-309b24b53402"), 207m, "Fahey", null, new DateTime(1979, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Layne", 103m, null },
                    { new Guid("c06a5a28-4b9a-4812-bb37-1658bfb58b7e"), 207m, "Fahey", null, new DateTime(1979, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Layne", 103m, null },
                    { new Guid("d2740f5a-abe1-4a0b-a300-08de1746d733"), 180m, "Chavez", null, new DateTime(2019, 10, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Pedro", 110m, null },
                    { new Guid("de44922e-b41a-46a3-0245-08de14c94aa7"), 167m, "Grande", null, new DateTime(2000, 10, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Ariana", 76m, null },
                    { new Guid("e4b8e3d5-63f3-482f-979a-1256427b09e1"), 185m, "Walker", null, new DateTime(1987, 6, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Chris", 74m, null }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Tiempo",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Breakfast" },
                    { 2, "HalfMorning" },
                    { 3, "Lunch" },
                    { 4, "HalfAfternoon" },
                    { 5, "Dinner" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "TipoAlimento",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Verdura" },
                    { 2, "Fruta" },
                    { 3, "FrutoSeco" },
                    { 4, "CarneRoja" },
                    { 5, "CarneBlanca" },
                    { 6, "Grano" },
                    { 7, "Carbohidrato" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "UnidadMedida",
                columns: new[] { "Id", "Nombre", "Simbolo" },
                values: new object[,]
                {
                    { 1, "Gramos", "g" },
                    { 2, "Kilogramos", "kg" },
                    { 3, "Mililitro", "Ml" },
                    { 4, "Litro", "L" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Categoria",
                columns: new[] { "Id", "Nombre", "TipoAlimentoId" },
                values: new object[,]
                {
                    { new Guid("08082e0e-3db0-4fc5-82ad-6be43d1d8b96"), "Almendras", 3 },
                    { new Guid("0ac3ec26-2f75-46dc-a13c-3db9a4ca6ab6"), "Nueces", 3 },
                    { new Guid("19596e02-f8cb-443a-b039-e0d86c213e68"), "Maíz", 6 },
                    { new Guid("313de149-aa99-48ff-8d2a-1f6a778469ad"), "Cordero", 4 },
                    { new Guid("399a37bb-4703-40e1-941f-369b2d51d952"), "Pistachos", 3 },
                    { new Guid("3b96f79b-7488-479f-8a43-263eba3c9077"), "Papa", 7 },
                    { new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Cebada", 6 },
                    { new Guid("482d1425-cece-43be-903c-9896902bafc1"), "Res", 4 },
                    { new Guid("4f0d9cf1-1805-4f37-bbef-788144d527ba"), "Verdura congelada", 1 },
                    { new Guid("5f46dcc2-156d-4ec4-8854-0e16fb78308b"), "Arroz", 6 },
                    { new Guid("63f881a9-b533-410b-bc4c-0093990f9da1"), "Verdura de raíz", 1 },
                    { new Guid("6c7bce6b-e56e-482c-8499-7e2ce9fbb7e4"), "Carne molida", 4 },
                    { new Guid("6fd10062-3080-4f9f-889d-a5a48688711e"), "Fruta cítrica", 2 },
                    { new Guid("7028b05f-9a06-4dad-8049-93900c85a4f0"), "Cerdo", 4 },
                    { new Guid("7a487298-591f-4732-bcc6-c155a3cb71ba"), "Yuca", 7 },
                    { new Guid("7c068691-0b72-485b-aa2b-e6d85ce5b656"), "Pan", 7 },
                    { new Guid("7ce7f003-bceb-4a9b-b879-eb2deebaa373"), "Carne curada", 4 },
                    { new Guid("83f2dea9-5a6c-43a0-9dac-b4790611b524"), "Verdura fresca", 1 },
                    { new Guid("840dcd1d-f4e8-4156-a18c-cec437d0f805"), "Fruta congelada", 2 },
                    { new Guid("8b3369ce-284e-46f6-91c6-a6a5789e289b"), "Trigo", 6 },
                    { new Guid("8e0181da-6328-4be1-8cd1-e88c29f1b6c2"), "Verdura orgánica", 1 },
                    { new Guid("a185f37c-ec77-4037-bf7f-0df4555e58b9"), "Cereal", 7 },
                    { new Guid("a22302fa-057b-4603-a68d-49a7816ce1fe"), "Pavo", 5 },
                    { new Guid("b7dc2a55-ac7a-4ddf-a2e0-7bf8d603d660"), "Conejo", 5 },
                    { new Guid("c0bce9d3-0e49-4438-b7af-ac305e6300b0"), "Fruta tropical", 2 },
                    { new Guid("c3d3c887-a7e8-4679-87bc-956f55401c74"), "Fruta de estación", 2 },
                    { new Guid("c8ee09e0-8eec-4f25-95af-3ab6235578da"), "Avena", 6 },
                    { new Guid("c9b6508f-91a3-4a3d-8df7-e3d3aa7e2f02"), "Fruta seca", 2 },
                    { new Guid("cb120002-33c9-45eb-b475-437e96e8da49"), "Carne de ave", 5 },
                    { new Guid("d5ee7bf1-c75c-4490-8820-c2ceb374e846"), "Pasta", 7 },
                    { new Guid("d6f06539-7e82-40d3-b29f-86b8941375bc"), "Castañas", 3 },
                    { new Guid("d8dc275b-ff4f-44ed-a291-df3ed7ccc760"), "Pollo", 5 },
                    { new Guid("d9128e81-1d35-4418-824d-34432c572dff"), "Pescado blanco", 5 },
                    { new Guid("f6fe0472-fde5-439f-9921-3f7ddbca926e"), "Maní", 3 }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Receta",
                columns: new[] { "Id", "Instrucciones", "Nombre", "TiempoId" },
                values: new object[,]
                {
                    { new Guid("17777777-7777-7777-7777-777777777777"), "Mezclar avena tradicional con miel y trozos de chocolate.", "Barrita Energética Casera", 3 },
                    { new Guid("28888888-8888-8888-8888-888888888888"), "Marinar el cordero con especias y hornear con papas.", "Chuletas de Cordero al Horno", 4 },
                    { new Guid("39999999-9999-9999-9999-999999999999"), "Hervir el pollo con cebada y vegetales hasta ablandar.", "Sopa de Cebada y Pollo", 4 },
                    { new Guid("4aaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Desmenuzar pavo cocido y servir con puré de papa.", "Pavo Mechado Liviano", 4 },
                    { new Guid("a1111111-1111-1111-1111-111111111111"), "Sofreír el charque, añadir arroz y colorante, cocinar hasta que esté seco.", "Majadito de Charque", 2 },
                    { new Guid("b2222222-2222-2222-2222-222222222222"), "Sazonar el pollo, pasar por harina y freír en abundante aceite.", "Pollo Frito Crujiente", 2 },
                    { new Guid("c3333333-3333-3333-3333-333333333333"), "Cocinar el arroz con presas de pollo y vegetales picados.", "Arroz con Pollo", 2 },
                    { new Guid("d4444444-4444-4444-4444-444444444444"), "Cocer la avena en leche de avena con trozos de manzana.", "Avena con Manzana", 1 },
                    { new Guid("e5555555-5555-5555-5555-555555555555"), "Tostar pan de cebada y untar con mantequilla de maní.", "Tostadas de Cebada", 1 },
                    { new Guid("f6666666-6666-6666-6666-666666666666"), "Mezclar pistachos, maní tostado y almendras.", "Snack de Frutos Secos", 3 }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Ingrediente",
                columns: new[] { "Id", "Calorias", "CantidadValor", "CategoriaId", "Nombre", "UnidadId" },
                values: new object[,]
                {
                    { new Guid("01b9dd28-4238-46ed-b97d-2c3332307f04"), 339m, 1m, new Guid("a185f37c-ec77-4037-bf7f-0df4555e58b9"), "Trigo integral", 1 },
                    { new Guid("01cee8ad-d60c-43fd-9337-8ab3cc3820f2"), 48m, 1m, new Guid("63f881a9-b533-410b-bc4c-0093990f9da1"), "Rábano picante", 1 },
                    { new Guid("02e196f1-1b17-45fe-9a8f-731651f53f8b"), 43m, 1m, new Guid("63f881a9-b533-410b-bc4c-0093990f9da1"), "Remolacha cruda", 1 },
                    { new Guid("05d2d337-16d8-43a9-ab48-2b7d61a2d320"), 250m, 1m, new Guid("f6fe0472-fde5-439f-9921-3f7ddbca926e"), "Salsa de maní (porción)", 2 },
                    { new Guid("0666d264-6842-4ecf-ad0c-646eb15b9bb1"), 410m, 1m, new Guid("c8ee09e0-8eec-4f25-95af-3ab6235578da"), "Muesli con avena", 1 },
                    { new Guid("07c33a71-d9a3-48a0-976e-c85a70bd6889"), 180m, 1m, new Guid("cb120002-33c9-45eb-b475-437e96e8da49"), "Pavo molido", 1 },
                    { new Guid("09103b00-9d4c-49c6-995a-51e8928ff585"), 90m, 1m, new Guid("d9128e81-1d35-4418-824d-34432c572dff"), "Lenguado a la plancha", 1 },
                    { new Guid("0a5b2076-f3e6-4aad-8106-a3c385108531"), 371m, 1m, new Guid("a185f37c-ec77-4037-bf7f-0df4555e58b9"), "Amaranto (semilla)", 1 },
                    { new Guid("0bfd95b9-bb30-493d-ad04-12cd72102366"), 340m, 1m, new Guid("5f46dcc2-156d-4ec4-8854-0e16fb78308b"), "Arroz jazmín crudo", 1 },
                    { new Guid("0cf67fc8-bf21-4999-988c-661475261a9a"), 380m, 1m, new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Cebada tostada", 1 },
                    { new Guid("0e6f9613-7ca8-4d16-aca9-520c17561219"), 87m, 1m, new Guid("3b96f79b-7488-479f-8a43-263eba3c9077"), "Papa cocida (con piel)", 1 },
                    { new Guid("103b33e9-f1a3-431c-bf3a-c279926384b7"), 380m, 1m, new Guid("f6fe0472-fde5-439f-9921-3f7ddbca926e"), "Harina de maní desgrasada", 1 },
                    { new Guid("10d5c593-c1ca-40c9-a487-790b65ce1ccd"), 100m, 1m, new Guid("a185f37c-ec77-4037-bf7f-0df4555e58b9"), "Avena instantánea (sobre)", 3 },
                    { new Guid("1159f281-612a-4c07-a955-a4eeb8a11cc8"), 450m, 1m, new Guid("08082e0e-3db0-4fc5-82ad-6be43d1d8b96"), "Mazapán (porción)", 1 },
                    { new Guid("136e2aec-02e9-4a3e-913e-65dde5901c3c"), 1m, 1m, new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Cebada Generica", 1 },
                    { new Guid("137f99b9-1892-4d8b-ae5b-374000a8609d"), 200m, 1m, new Guid("cb120002-33c9-45eb-b475-437e96e8da49"), "Muslo de pollo (con piel, asado)", 1 },
                    { new Guid("16d7cc90-88ad-4f7b-a514-a7ef03302e31"), 220m, 1m, new Guid("313de149-aa99-48ff-8d2a-1f6a778469ad"), "Brocheta de cordero", 3 },
                    { new Guid("175950fb-17fc-42ac-b08a-18ea09184246"), 95m, 1m, new Guid("d9128e81-1d35-4418-824d-34432c572dff"), "Rape (cola)", 1 },
                    { new Guid("18018540-a0ec-4a86-806f-5772f99b58df"), 61m, 1m, new Guid("63f881a9-b533-410b-bc4c-0093990f9da1"), "Puerro", 3 },
                    { new Guid("18a81599-c01a-443f-bc37-07cd62cca9a5"), 135m, 1m, new Guid("a22302fa-057b-4603-a68d-49a7816ce1fe"), "Filete de pavo (cocido)", 1 },
                    { new Guid("19b41bf7-cbf2-4f0e-88d5-2293486e3c39"), 575m, 1m, new Guid("399a37bb-4703-40e1-941f-369b2d51d952"), "Pistachos tostados y salados", 1 },
                    { new Guid("1b7bcda4-f957-4cf7-986a-8ca671735df5"), 200m, 1m, new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Sopa de cebada (ración)", 2 },
                    { new Guid("20774d7b-7bf1-4b4c-b4a9-7e17f1509634"), 280m, 1m, new Guid("313de149-aa99-48ff-8d2a-1f6a778469ad"), "Pierna de cordero asada", 1 },
                    { new Guid("2403700c-3916-4855-830e-f2687eb0a84f"), 93m, 1m, new Guid("3b96f79b-7488-479f-8a43-263eba3c9077"), "Papa asada", 1 },
                    { new Guid("24e77645-5d8f-4f08-b020-76ab6c73d428"), 10m, 100m, new Guid("63f881a9-b533-410b-bc4c-0093990f9da1"), "Raices verdes", 1 },
                    { new Guid("2682a7c3-2972-4a94-9c29-1275a5a125e2"), 10m, 100m, new Guid("63f881a9-b533-410b-bc4c-0093990f9da1"), "Raices rojas", 1 },
                    { new Guid("2cafbcac-e4ef-4353-b9aa-07d9d73b3d08"), 400m, 1m, new Guid("c8ee09e0-8eec-4f25-95af-3ab6235578da"), "Harina de avena", 1 },
                    { new Guid("2ebe85cf-a3dc-4c82-9e6c-6bf65039f5ce"), 135m, 1m, new Guid("cb120002-33c9-45eb-b475-437e96e8da49"), "Pavo (pechuga, cocida)", 1 },
                    { new Guid("31661016-1c2f-49e0-b11b-22434de2d349"), 105m, 1m, new Guid("d9128e81-1d35-4418-824d-34432c572dff"), "Bacalao fresco (cocido)", 1 },
                    { new Guid("339f2b58-a695-49fa-b82f-5fea93bcbc8b"), 165m, 1m, new Guid("cb120002-33c9-45eb-b475-437e96e8da49"), "Pechuga de pollo (sin piel, cocida)", 1 },
                    { new Guid("348d47ab-6ce8-40b6-b219-4b757240370f"), 205m, 1m, new Guid("d9128e81-1d35-4418-824d-34432c572dff"), "Caballa (en conserva)", 1 },
                    { new Guid("35566955-b8e8-42ef-962d-a3190bc045ec"), 1m, 0m, new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Cebada mediana", 1 },
                    { new Guid("35fa6e8e-8217-442a-a376-3179d9e14e1d"), 150m, 1m, new Guid("cb120002-33c9-45eb-b475-437e96e8da49"), "Alas de pollo (fritas, unidad)", 3 },
                    { new Guid("37070ad9-eb9f-4b41-bc98-7e49671ff88b"), 1m, 1m, new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Cebada Generica", 1 },
                    { new Guid("38e19aa6-10be-4988-af91-545190df1990"), 90m, 1m, new Guid("399a37bb-4703-40e1-941f-369b2d51d952"), "Leche de pistacho (vaso)", 2 },
                    { new Guid("398d2c02-7603-4fbf-b81f-78073b22367e"), 90m, 1m, new Guid("3b96f79b-7488-479f-8a43-263eba3c9077"), "Papa dulce (batata) asada", 1 },
                    { new Guid("39b34ff7-7b3a-43f1-b7d1-9b8d8da63503"), 567m, 1m, new Guid("f6fe0472-fde5-439f-9921-3f7ddbca926e"), "Maní crudo", 1 },
                    { new Guid("39cf5260-c2de-4c76-9a52-429e9613440c"), 20m, 10m, new Guid("a185f37c-ec77-4037-bf7f-0df4555e58b9"), "Cereal en caja", 1 },
                    { new Guid("3ac58f2f-734e-46c6-836b-04af33deccbd"), 354m, 1m, new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Cebada perlada", 1 },
                    { new Guid("3bc78b5b-8a70-480c-a372-b7d150dce38d"), 30m, 1m, new Guid("a22302fa-057b-4603-a68d-49a7816ce1fe"), "Jamón de pavo (rebanada)", 3 },
                    { new Guid("3c8addf2-9894-42b0-9047-86029d2be60c"), 370m, 1m, new Guid("c8ee09e0-8eec-4f25-95af-3ab6235578da"), "Avena instantánea", 1 },
                    { new Guid("3e363c48-09fc-4a35-8f18-24f10770b180"), 590m, 1m, new Guid("08082e0e-3db0-4fc5-82ad-6be43d1d8b96"), "Almendras laminadas", 1 },
                    { new Guid("40a55f73-494b-432b-a4b9-938dca1d9244"), 180m, 1m, new Guid("399a37bb-4703-40e1-941f-369b2d51d952"), "Barra energética con pistachos", 3 },
                    { new Guid("418f611f-c6d9-4a28-b1fd-8a0385cbbe77"), 587m, 1m, new Guid("f6fe0472-fde5-439f-9921-3f7ddbca926e"), "Maní tostado y salado", 1 },
                    { new Guid("41f248da-9992-400f-824b-8462fb03f9b8"), 120m, 1m, new Guid("c8ee09e0-8eec-4f25-95af-3ab6235578da"), "Barra de cereal de avena", 3 },
                    { new Guid("4200520d-dcf4-424b-8b07-d9459e3e2f11"), 41m, 1m, new Guid("63f881a9-b533-410b-bc4c-0093990f9da1"), "Zanahoria fresca", 1 },
                    { new Guid("427df732-0307-4788-bbb9-7681c88e2b6e"), 250m, 1m, new Guid("313de149-aa99-48ff-8d2a-1f6a778469ad"), "Paletilla de cordero (cruda)", 1 },
                    { new Guid("42bfb9aa-c46d-4117-a56e-632a8b16baf6"), 150m, 1m, new Guid("a185f37c-ec77-4037-bf7f-0df4555e58b9"), "Hojuelas de maíz (ración)", 1 },
                    { new Guid("4593f1f9-ebce-45dd-8577-2db912476ef7"), 338m, 1m, new Guid("a185f37c-ec77-4037-bf7f-0df4555e58b9"), "Centeno en grano", 1 },
                    { new Guid("45f81d79-ec4c-4464-94e7-c81405ddc38c"), 250m, 1m, new Guid("a22302fa-057b-4603-a68d-49a7816ce1fe"), "Taco de pavo (unidad)", 3 },
                    { new Guid("48555eb5-6f17-4321-82ae-5a032a3c6cb5"), 150m, 1m, new Guid("399a37bb-4703-40e1-941f-369b2d51d952"), "Turrón de pistacho (porción)", 1 },
                    { new Guid("4bbcf803-0372-4515-b700-42749a77e1ba"), 35m, 1m, new Guid("5f46dcc2-156d-4ec4-8854-0e16fb78308b"), "Galletas de arroz inflado", 3 },
                    { new Guid("4c8825b9-b0fa-4595-a242-6af8b4b0d41c"), 120m, 1m, new Guid("d9128e81-1d35-4418-824d-34432c572dff"), "Dorada (a la sal)", 1 },
                    { new Guid("4d091f12-1a1d-4ee3-9d80-e2e7f5ac135c"), 120m, 1m, new Guid("08082e0e-3db0-4fc5-82ad-6be43d1d8b96"), "Aceite de almendra (cda)", 4 },
                    { new Guid("4d781e95-713e-4b77-bbb8-3e45add02d5e"), 120m, 1m, new Guid("399a37bb-4703-40e1-941f-369b2d51d952"), "Aceite de pistacho (cda)", 4 },
                    { new Guid("4e0f040b-a05a-40b9-b4fc-ad957ea99f64"), 180m, 1m, new Guid("3b96f79b-7488-479f-8a43-263eba3c9077"), "Gajo de papa especiado", 1 },
                    { new Guid("4f7f1592-8ad0-40dc-98d9-a33caf68d91b"), 50m, 1m, new Guid("a22302fa-057b-4603-a68d-49a7816ce1fe"), "Consomé de pavo (taza)", 2 },
                    { new Guid("4fcd46f2-68b6-41b4-ab2f-1f10687571b4"), 500m, 1m, new Guid("f6fe0472-fde5-439f-9921-3f7ddbca926e"), "Maní japonés (con cáscara)", 1 },
                    { new Guid("584fb1ac-97d2-423e-940e-3bd2904d087b"), 360m, 1m, new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Granos de cebada integral", 1 },
                    { new Guid("58ba849a-fd32-44c5-a321-b70c740a9247"), 350m, 1m, new Guid("3b96f79b-7488-479f-8a43-263eba3c9077"), "Papa deshidratada (copos)", 1 },
                    { new Guid("5a8971da-18ae-44c8-8413-177fe0f6708b"), 30m, 1m, new Guid("cb120002-33c9-45eb-b475-437e96e8da49"), "Embutido de pavo (rebanada)", 3 },
                    { new Guid("5f46dcc2-156d-4ec4-8854-0e16fb78308b"), 220m, 220m, new Guid("5f46dcc2-156d-4ec4-8854-0e16fb78308b"), "porcion de arroz", 1 },
                    { new Guid("60642821-b6b3-442f-bdfb-4130bd166674"), 150m, 1m, new Guid("5f46dcc2-156d-4ec4-8854-0e16fb78308b"), "Arroz para sushi (cocido)", 1 },
                    { new Guid("62511ae8-8bd6-4a2c-9f4f-ebcfe6e34f44"), 180m, 1m, new Guid("a22302fa-057b-4603-a68d-49a7816ce1fe"), "Salchicha de pavo (unidad)", 3 },
                    { new Guid("68a43539-0fd9-430a-a673-19161a744413"), 10m, 1m, new Guid("4f0d9cf1-1805-4f37-bbef-788144d527ba"), "Zapallo", 1 },
                    { new Guid("69e956b2-565f-4415-a271-542aaf3a2742"), 111m, 1m, new Guid("5f46dcc2-156d-4ec4-8854-0e16fb78308b"), "Arroz integral cocido", 1 },
                    { new Guid("6ee11798-09af-4f2c-a299-a21bf749161f"), 83m, 1m, new Guid("3b96f79b-7488-479f-8a43-263eba3c9077"), "Puré de papa", 1 },
                    { new Guid("6feb328c-3581-444d-a243-60e8e07ed959"), 120m, 1m, new Guid("5f46dcc2-156d-4ec4-8854-0e16fb78308b"), "Leche de arroz (vaso)", 2 },
                    { new Guid("73189acf-c313-44dc-9778-bd3795d134c3"), 130m, 1m, new Guid("c8ee09e0-8eec-4f25-95af-3ab6235578da"), "Leche de avena (vaso)", 2 },
                    { new Guid("80d53d51-96d7-495c-9dc2-63ae37de67ca"), 585m, 1m, new Guid("08082e0e-3db0-4fc5-82ad-6be43d1d8b96"), "Almendras tostadas con sal", 1 },
                    { new Guid("819a5089-8949-488b-acb2-5a6a9732f116"), 150m, 1m, new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Cerveza de cebada (1 lata)", 2 },
                    { new Guid("8742d4e0-382b-47d6-b9e3-1617d57c11ad"), 86m, 1m, new Guid("63f881a9-b533-410b-bc4c-0093990f9da1"), "Boniato (camote)", 1 },
                    { new Guid("885efeeb-024d-4e01-8c26-9ff261b8fb53"), 345m, 1m, new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Harina de cebada", 1 },
                    { new Guid("88cef06b-adf4-4802-a9d6-a3cdfb46629b"), 120m, 1m, new Guid("f6fe0472-fde5-439f-9921-3f7ddbca926e"), "Aceite de maní (cda)", 4 },
                    { new Guid("8ab040a4-1f4e-4fff-824f-acdc88c27de7"), 570m, 1m, new Guid("399a37bb-4703-40e1-941f-369b2d51d952"), "Pistachos verdes (pelados)", 1 },
                    { new Guid("8ac032d5-7aa2-400b-b0aa-f969fe44a99b"), 368m, 1m, new Guid("a185f37c-ec77-4037-bf7f-0df4555e58b9"), "Quinoa cruda", 1 },
                    { new Guid("8d5c9a34-bb53-4b8c-88bf-fc399b1d04b1"), 190m, 1m, new Guid("f6fe0472-fde5-439f-9921-3f7ddbca926e"), "Mantequilla de maní (2 cdas)", 4 },
                    { new Guid("92e2a7ca-bbe9-4941-9688-f79c21b07fa0"), 230m, 1m, new Guid("cb120002-33c9-45eb-b475-437e96e8da49"), "Gallina cocida", 1 },
                    { new Guid("9551c29e-7286-4b8d-aac1-3b517e4006de"), 580m, 1m, new Guid("399a37bb-4703-40e1-941f-369b2d51d952"), "Pistachos molidos (harina)", 1 },
                    { new Guid("975b0575-f99a-42a9-be76-715a509fea59"), 20m, 5m, new Guid("83f2dea9-5a6c-43a0-9dac-b4790611b524"), "Lechuga", 1 },
                    { new Guid("98746e75-69fc-4c53-a7b5-ea56df561bbd"), 80m, 1m, new Guid("63f881a9-b533-410b-bc4c-0093990f9da1"), "Jengibre (raíz)", 1 },
                    { new Guid("9aba557f-378f-4add-9613-3547d5ef99f1"), 130m, 1m, new Guid("313de149-aa99-48ff-8d2a-1f6a778469ad"), "Riñones de cordero", 1 },
                    { new Guid("9b725f6c-7263-4918-b8ef-45e60f7db85d"), 167m, 1m, new Guid("cb120002-33c9-45eb-b475-437e96e8da49"), "Hígado de pollo", 1 },
                    { new Guid("9ddff76b-d8c4-450d-ad00-0ddb152cdf91"), 150m, 1m, new Guid("a22302fa-057b-4603-a68d-49a7816ce1fe"), "Pavo molido magro", 1 },
                    { new Guid("9e4887e1-9191-4d9f-8004-370f6bee057f"), 280m, 1m, new Guid("399a37bb-4703-40e1-941f-369b2d51d952"), "Helado de pistacho (taza)", 2 },
                    { new Guid("a2340ba7-6ad4-4a5a-96b1-17f1aa7a0e48"), 40m, 1m, new Guid("08082e0e-3db0-4fc5-82ad-6be43d1d8b96"), "Leche de almendra (sin azúcar, vaso)", 2 },
                    { new Guid("a46b3498-1488-4eb8-aa15-0068c4d596f1"), 400m, 1m, new Guid("313de149-aa99-48ff-8d2a-1f6a778469ad"), "Estofado de cordero (porción)", 2 },
                    { new Guid("a5356075-69de-465a-acca-0cd25ca72b81"), 330m, 1m, new Guid("3b96f79b-7488-479f-8a43-263eba3c9077"), "Harina de papa", 1 },
                    { new Guid("a603d9fd-6bef-466e-9f0a-0cd9d148bdda"), 110m, 1m, new Guid("d9128e81-1d35-4418-824d-34432c572dff"), "Fletán (halibut)", 1 },
                    { new Guid("a723cbeb-2733-4559-8590-e946072010c6"), 28m, 1m, new Guid("63f881a9-b533-410b-bc4c-0093990f9da1"), "Nabo cocido", 1 },
                    { new Guid("a7723dea-d5df-4dca-abfa-768d0f0296a7"), 100m, 10m, new Guid("63f881a9-b533-410b-bc4c-0093990f9da1"), "Raiz seca", 1 },
                    { new Guid("a8bea2e8-e92d-4029-95d6-b559db6d2a42"), 389m, 1m, new Guid("c8ee09e0-8eec-4f25-95af-3ab6235578da"), "Hojuelas de avena tradicional", 1 },
                    { new Guid("a98f6a71-c06e-4b59-807b-7f46389d50c2"), 370m, 1m, new Guid("c8ee09e0-8eec-4f25-95af-3ab6235578da"), "Avena cortada (steel-cut)", 1 },
                    { new Guid("aa440882-f4c5-426e-abb0-d4d95a8ae689"), 200m, 1m, new Guid("a22302fa-057b-4603-a68d-49a7816ce1fe"), "Pavo mechado (carne oscura)", 1 },
                    { new Guid("abb20811-4d69-4c4d-9af5-14ea3516cd7b"), 166m, 1m, new Guid("5f46dcc2-156d-4ec4-8854-0e16fb78308b"), "Arroz salvaje cocido", 1 },
                    { new Guid("ac8b7aff-a871-4271-bab5-2cb5fa48c4e0"), 300m, 1m, new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Extracto de malta", 4 },
                    { new Guid("b02563f3-5ac0-4724-851a-34f038e6181a"), 450m, 1m, new Guid("c8ee09e0-8eec-4f25-95af-3ab6235578da"), "Granola de avena", 1 },
                    { new Guid("b031b461-ac43-4f27-91d0-a0a938e9a931"), 150m, 1m, new Guid("a22302fa-057b-4603-a68d-49a7816ce1fe"), "Carne de pavo ahumada", 1 },
                    { new Guid("b159ec00-d462-4af4-b78b-d0714aa29d19"), 120m, 1m, new Guid("cb120002-33c9-45eb-b475-437e96e8da49"), "Sopa de pollo (taza)", 2 },
                    { new Guid("b354bfb1-bbd0-476d-9f4b-5464664ef818"), 160m, 1m, new Guid("63f881a9-b533-410b-bc4c-0093990f9da1"), "Yuca (mandioca)", 1 },
                    { new Guid("b37faca1-86e1-4269-b01b-69ab2efa0040"), 378m, 1m, new Guid("a185f37c-ec77-4037-bf7f-0df4555e58b9"), "Mijo", 1 },
                    { new Guid("b3c1f091-c3a9-492a-a617-1b0791c984df"), 75m, 1m, new Guid("3b96f79b-7488-479f-8a43-263eba3c9077"), "Papa al vapor", 1 },
                    { new Guid("b48f2cb9-9bb1-41b7-80c0-795d33739166"), 550m, 1m, new Guid("08082e0e-3db0-4fc5-82ad-6be43d1d8b96"), "Chocolate con almendras (barra)", 1 },
                    { new Guid("b81bca56-aa99-4493-821a-46d149023da0"), 579m, 1m, new Guid("08082e0e-3db0-4fc5-82ad-6be43d1d8b96"), "Almendras crudas (enteras)", 1 },
                    { new Guid("b8b4f105-ff73-4dd1-a0f6-bb0be28884a0"), 110m, 1m, new Guid("f6fe0472-fde5-439f-9921-3f7ddbca926e"), "Bombón de maní y chocolate", 3 },
                    { new Guid("bb2704ca-6b63-4992-8033-f346a14002df"), 342m, 1m, new Guid("a185f37c-ec77-4037-bf7f-0df4555e58b9"), "Bulgur", 1 },
                    { new Guid("bc099a87-d608-486a-a131-20a70b53bc5e"), 320m, 1m, new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Cebada en copos", 1 },
                    { new Guid("bc4c3944-2ecc-46e2-b4d0-d40f39adae62"), 480m, 1m, new Guid("f6fe0472-fde5-439f-9921-3f7ddbca926e"), "Maní confitado", 1 },
                    { new Guid("c1b572fe-f086-41d0-b0f3-498c36fc536f"), 250m, 1m, new Guid("3b96f79b-7488-479f-8a43-263eba3c9077"), "Papa pre-frita congelada", 1 },
                    { new Guid("c225537e-7a21-47e3-ad91-b944b13af3a2"), 50m, 1m, new Guid("c8ee09e0-8eec-4f25-95af-3ab6235578da"), "Galletas de avena (unidad)", 3 },
                    { new Guid("c2efe0af-1950-42f2-a2fd-0ce6adcc83a5"), 135m, 1m, new Guid("313de149-aa99-48ff-8d2a-1f6a778469ad"), "Hígado de cordero", 1 },
                    { new Guid("c430d1d8-ad26-4aa6-8304-b3b0ee00d60b"), 290m, 1m, new Guid("313de149-aa99-48ff-8d2a-1f6a778469ad"), "Lomo de cordero", 1 },
                    { new Guid("c5c6992a-08c3-471b-865f-f67a7eebe748"), 1m, 0m, new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Cebada Generica", 1 },
                    { new Guid("c5ff1f22-1075-4d6d-8aef-696b7db6ef9c"), 96m, 1m, new Guid("d9128e81-1d35-4418-824d-34432c572dff"), "Tilapia (filete)", 1 },
                    { new Guid("c815bfa5-1c3a-420f-b013-b1ebb8096af8"), 370m, 1m, new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Malta de cebada", 1 },
                    { new Guid("c8f3360a-ae34-47a3-baf9-61e279aae849"), 86m, 1m, new Guid("d9128e81-1d35-4418-824d-34432c572dff"), "Merluza (cocida)", 1 },
                    { new Guid("cd365a29-d84f-4ca1-a854-e6a6887e9da4"), 40m, 1m, new Guid("63f881a9-b533-410b-bc4c-0093990f9da1"), "Cebolla blanca", 3 },
                    { new Guid("d11916b6-2f8d-45f8-be2d-ced10cef4d3f"), 90m, 1m, new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Pan de cebada (rebanada)", 3 },
                    { new Guid("d45b59f8-a4b1-4f87-b044-fad5a958eb38"), 90m, 1m, new Guid("399a37bb-4703-40e1-941f-369b2d51d952"), "Mantequilla de pistacho (cda)", 4 },
                    { new Guid("d56568e1-eeaf-4d43-9bd5-f2e0b24892c9"), 580m, 1m, new Guid("08082e0e-3db0-4fc5-82ad-6be43d1d8b96"), "Granos de almendra (pelados)", 1 },
                    { new Guid("d5918f21-1388-48fa-8d65-1afc18767dd5"), 562m, 1m, new Guid("399a37bb-4703-40e1-941f-369b2d51d952"), "Pistachos crudos (sin cáscara)", 1 },
                    { new Guid("d70553ca-f0d4-4e26-b731-d5bda3e3432b"), 380m, 1m, new Guid("313de149-aa99-48ff-8d2a-1f6a778469ad"), "Costillar de cordero", 1 },
                    { new Guid("d79ebf22-bbf0-4343-9db9-bf6953ae83f7"), 270m, 1m, new Guid("313de149-aa99-48ff-8d2a-1f6a778469ad"), "Carne picada de cordero", 1 },
                    { new Guid("d7f586c8-fbcf-40a8-a30c-51cff2745371"), 1m, 0m, new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Cebada virtual", 1 },
                    { new Guid("de511e70-d867-4d08-8c5e-8a59429390ed"), 350m, 1m, new Guid("313de149-aa99-48ff-8d2a-1f6a778469ad"), "Chuleta de cordero (cocida)", 3 },
                    { new Guid("e0f38c54-82bb-40dd-a5e3-22716b2e20ad"), 100m, 1m, new Guid("08082e0e-3db0-4fc5-82ad-6be43d1d8b96"), "Mantequilla de almendra (cda)", 4 },
                    { new Guid("e2763d60-37f6-4b22-8335-3de96dc739e5"), 80m, 1m, new Guid("d9128e81-1d35-4418-824d-34432c572dff"), "Panga (cocida)", 1 },
                    { new Guid("e4f52e8d-f0f3-4325-ac38-bc2f7ff39e09"), 350m, 1m, new Guid("5f46dcc2-156d-4ec4-8854-0e16fb78308b"), "Arroz basmati crudo", 1 },
                    { new Guid("e6fb9734-804f-45f9-8141-fd18d274bf10"), 312m, 1m, new Guid("3b96f79b-7488-479f-8a43-263eba3c9077"), "Papa frita (patatas fritas)", 1 },
                    { new Guid("e7d93511-ce8f-4f1f-bf76-72c002406c5c"), 1m, 1m, new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Cebada especial", 1 },
                    { new Guid("e984d4e9-f9d8-46b7-b078-3375237e2865"), 149m, 1m, new Guid("63f881a9-b533-410b-bc4c-0093990f9da1"), "Ajo (cabeza)", 3 },
                    { new Guid("ec6b6eaf-54f5-4485-b451-03448a943f74"), 366m, 1m, new Guid("5f46dcc2-156d-4ec4-8854-0e16fb78308b"), "Harina de arroz", 1 },
                    { new Guid("ec83d6d6-24dd-4ef8-a21f-89ba0f07be3b"), 520m, 1m, new Guid("f6fe0472-fde5-439f-9921-3f7ddbca926e"), "Snack de maní con miel", 1 },
                    { new Guid("ec852f5c-9666-482f-a2bd-d952fdc53548"), 246m, 1m, new Guid("c8ee09e0-8eec-4f25-95af-3ab6235578da"), "Salvado de avena", 1 },
                    { new Guid("ece53d46-e579-4f04-a7c7-e62e51fb6fe3"), 70m, 1m, new Guid("a22302fa-057b-4603-a68d-49a7816ce1fe"), "Albóndiga de pavo (unidad)", 3 },
                    { new Guid("ee4940b2-b603-4c83-9741-0643511215a0"), 140m, 1m, new Guid("a22302fa-057b-4603-a68d-49a7816ce1fe"), "Pavo deshuesado (crudo)", 1 },
                    { new Guid("f32c48d8-60b2-440b-a8ad-aac2f6c04991"), 337m, 1m, new Guid("cb120002-33c9-45eb-b475-437e96e8da49"), "Carne de pato (asada)", 1 },
                    { new Guid("f49a0893-349d-4d2b-a8db-7b4088d9cf48"), 338m, 1m, new Guid("a185f37c-ec77-4037-bf7f-0df4555e58b9"), "Espelta", 1 },
                    { new Guid("f56c77a2-5d9f-415b-8df4-39df372072d0"), 90m, 1m, new Guid("d9128e81-1d35-4418-824d-34432c572dff"), "Abadejo (cocido)", 1 },
                    { new Guid("f62eaa37-2290-44e8-8966-24ca70d9a96e"), 370m, 1m, new Guid("a185f37c-ec77-4037-bf7f-0df4555e58b9"), "Maíz molido", 1 },
                    { new Guid("f6576e63-ab6d-4796-bb8b-fb476545effd"), 1m, 0m, new Guid("47c51b84-bb85-49f6-96a1-0701d7144a0e"), "Cebada virtual", 1 },
                    { new Guid("f9c2f136-8388-4166-88d3-f66079c6384e"), 250m, 1m, new Guid("5f46dcc2-156d-4ec4-8854-0e16fb78308b"), "Arroz frito (porción)", 1 },
                    { new Guid("fa20105e-58bd-4ba2-983c-cec6fcf75e52"), 130m, 1m, new Guid("5f46dcc2-156d-4ec4-8854-0e16fb78308b"), "Arroz blanco cocido", 1 },
                    { new Guid("fea6f29c-ba79-4928-b698-3f79624a3fe2"), 600m, 1m, new Guid("08082e0e-3db0-4fc5-82ad-6be43d1d8b96"), "Harina de almendra", 1 }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "RecetaIngrediente",
                columns: new[] { "Id", "CantidadValor", "IngredienteId", "RecetaId" },
                values: new object[,]
                {
                    { 1, 200m, new Guid("fa20105e-58bd-4ba2-983c-cec6fcf75e52"), new Guid("a1111111-1111-1111-1111-111111111111") },
                    { 2, 2m, new Guid("88cef06b-adf4-4802-a9d6-a3cdfb46629b"), new Guid("b2222222-2222-2222-2222-222222222222") },
                    { 3, 50m, new Guid("3c8addf2-9894-42b0-9047-86029d2be60c"), new Guid("d4444444-4444-4444-4444-444444444444") },
                    { 4, 1m, new Guid("73189acf-c313-44dc-9778-bd3795d134c3"), new Guid("d4444444-4444-4444-4444-444444444444") },
                    { 5, 30m, new Guid("8ab040a4-1f4e-4fff-824f-acdc88c27de7"), new Guid("f6666666-6666-6666-6666-666666666666") },
                    { 6, 20m, new Guid("418f611f-c6d9-4a28-b1fd-8a0385cbbe77"), new Guid("f6666666-6666-6666-6666-666666666666") },
                    { 7, 2m, new Guid("de511e70-d867-4d08-8c5e-8a59429390ed"), new Guid("28888888-8888-8888-8888-888888888888") },
                    { 8, 2m, new Guid("2403700c-3916-4855-830e-f2687eb0a84f"), new Guid("28888888-8888-8888-8888-888888888888") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_TipoAlimentoId",
                schema: "public",
                table: "Categoria",
                column: "TipoAlimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Dieta_PlanAlimentacionId",
                schema: "public",
                table: "Dieta",
                column: "PlanAlimentacionId");

            migrationBuilder.CreateIndex(
                name: "IX_DietaReceta_DietaId",
                schema: "public",
                table: "DietaReceta",
                column: "DietaId");

            migrationBuilder.CreateIndex(
                name: "IX_DietaReceta_RecetaId",
                schema: "public",
                table: "DietaReceta",
                column: "RecetaId");

            migrationBuilder.CreateIndex(
                name: "IX_DietaReceta_TiempoId",
                schema: "public",
                table: "DietaReceta",
                column: "TiempoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingrediente_CategoriaId",
                schema: "public",
                table: "Ingrediente",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingrediente_UnidadId",
                schema: "public",
                table: "Ingrediente",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanAlimentacion_NutricionistaId",
                schema: "public",
                table: "PlanAlimentacion",
                column: "NutricionistaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanAlimentacion_PacienteId",
                schema: "public",
                table: "PlanAlimentacion",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Receta_TiempoId",
                schema: "public",
                table: "Receta",
                column: "TiempoId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaIngrediente_IngredienteId",
                schema: "public",
                table: "RecetaIngrediente",
                column: "IngredienteId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaIngrediente_RecetaId",
                schema: "public",
                table: "RecetaIngrediente",
                column: "RecetaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietaReceta",
                schema: "public");

            migrationBuilder.DropTable(
                name: "outboxMessage",
                schema: "outbox");

            migrationBuilder.DropTable(
                name: "RecetaIngrediente",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Dieta",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Ingrediente",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Receta",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PlanAlimentacion",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Categoria",
                schema: "public");

            migrationBuilder.DropTable(
                name: "UnidadMedida",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Tiempo",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Nutricionista",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Paciente",
                schema: "public");

            migrationBuilder.DropTable(
                name: "TipoAlimento",
                schema: "public");
        }
    }
}
