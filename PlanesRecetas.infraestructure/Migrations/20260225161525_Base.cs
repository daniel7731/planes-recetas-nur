using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PlanesRecetas.infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Base : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "outbox");

            migrationBuilder.CreateTable(
                name: "Nutricionista",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    outboxId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    processed = table.Column<bool>(type: "bit", nullable: false),
                    processedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    correlationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    traceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    spanId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_outboxMessage", x => x.outboxId);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Peso = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Altura = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tiempo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiempo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoAlimento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAlimento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadMedida",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Simbolo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadMedida", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanAlimentacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NutricionistaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "DATE", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "DATE", nullable: false),
                    DuracionDias = table.Column<int>(type: "int", nullable: false, computedColumnSql: "DATEDIFF(DAY, [FechaInicio], [FechaFin])", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanAlimentacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanAlimentacion_Nutricionista_NutricionistaId",
                        column: x => x.NutricionistaId,
                        principalTable: "Nutricionista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanAlimentacion_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Instrucciones = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TiempoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receta_Tiempo_TiempoId",
                        column: x => x.TiempoId,
                        principalTable: "Tiempo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoAlimentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categoria_TipoAlimento_TipoAlimentoId",
                        column: x => x.TipoAlimentoId,
                        principalTable: "TipoAlimento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dieta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanAlimentacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaConsumo = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dieta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dieta_PlanAlimentacion_PlanAlimentacionId",
                        column: x => x.PlanAlimentacionId,
                        principalTable: "PlanAlimentacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingrediente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Calorias = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnidadId = table.Column<int>(type: "int", nullable: false),
                    CantidadValor = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingrediente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingrediente_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingrediente_UnidadMedida_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "UnidadMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DietaReceta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 2"),
                    DietaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecetaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TiempoId = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaReceta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietaReceta_Dieta_DietaId",
                        column: x => x.DietaId,
                        principalTable: "Dieta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DietaReceta_Receta_RecetaId",
                        column: x => x.RecetaId,
                        principalTable: "Receta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DietaReceta_Tiempo_TiempoId",
                        column: x => x.TiempoId,
                        principalTable: "Tiempo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecetaIngrediente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngredienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CantidadValor = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaIngrediente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecetaIngrediente_Ingrediente_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingrediente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaIngrediente_Receta_RecetaId",
                        column: x => x.RecetaId,
                        principalTable: "Receta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
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
                table: "UnidadMedida",
                columns: new[] { "Id", "Nombre", "Simbolo" },
                values: new object[,]
                {
                    { 1, "Gramos", "g" },
                    { 2, "Kilogramos", "kg" },
                    { 3, "Mililitro", "Ml" },
                    { 4, "Litro", "L" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_TipoAlimentoId",
                table: "Categoria",
                column: "TipoAlimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Dieta_PlanAlimentacionId",
                table: "Dieta",
                column: "PlanAlimentacionId");

            migrationBuilder.CreateIndex(
                name: "IX_DietaReceta_DietaId",
                table: "DietaReceta",
                column: "DietaId");

            migrationBuilder.CreateIndex(
                name: "IX_DietaReceta_RecetaId",
                table: "DietaReceta",
                column: "RecetaId");

            migrationBuilder.CreateIndex(
                name: "IX_DietaReceta_TiempoId",
                table: "DietaReceta",
                column: "TiempoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingrediente_CategoriaId",
                table: "Ingrediente",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingrediente_UnidadId",
                table: "Ingrediente",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanAlimentacion_NutricionistaId",
                table: "PlanAlimentacion",
                column: "NutricionistaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanAlimentacion_PacienteId",
                table: "PlanAlimentacion",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Receta_TiempoId",
                table: "Receta",
                column: "TiempoId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaIngrediente_IngredienteId",
                table: "RecetaIngrediente",
                column: "IngredienteId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaIngrediente_RecetaId",
                table: "RecetaIngrediente",
                column: "RecetaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietaReceta");

            migrationBuilder.DropTable(
                name: "outboxMessage",
                schema: "outbox");

            migrationBuilder.DropTable(
                name: "RecetaIngrediente");

            migrationBuilder.DropTable(
                name: "Dieta");

            migrationBuilder.DropTable(
                name: "Ingrediente");

            migrationBuilder.DropTable(
                name: "Receta");

            migrationBuilder.DropTable(
                name: "PlanAlimentacion");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "UnidadMedida");

            migrationBuilder.DropTable(
                name: "Tiempo");

            migrationBuilder.DropTable(
                name: "Nutricionista");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "TipoAlimento");
        }
    }
}
