using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PlanesRecetas.infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Templates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanTemplate",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Dias = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanItemTemplate",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlanTemplateId = table.Column<int>(type: "integer", nullable: false),
                    NumeroDia = table.Column<int>(type: "integer", nullable: false),
                    RecetaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanItemTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanItemTemplate_PlanTemplate_PlanTemplateId",
                        column: x => x.PlanTemplateId,
                        principalSchema: "public",
                        principalTable: "PlanTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanItemTemplate_Receta_RecetaId",
                        column: x => x.RecetaId,
                        principalSchema: "public",
                        principalTable: "Receta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "PlanTemplate",
                columns: new[] { "Id", "Dias", "Nombre" },
                values: new object[,]
                {
                    { 1, 15, "Plan Detox Raíces y Frescura" },
                    { 2, 15, "Plan Muscle & Power" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Receta",
                columns: new[] { "Id", "Instrucciones", "Nombre", "TiempoId" },
                values: new object[,]
                {
                    { new Guid("e5555555-5555-5555-5555-555555555551"), "Rallar la remolacha cruda y la zanahoria fresca. Mezclar sobre una base de lechuga. Aliñar con jengibre rallado, rábano picante y un toque de puerro picado.", "Ensalada Detox Raíces y Frescura", 2 },
                    { new Guid("e5555555-5555-5555-5555-555555555552"), "Cocinar la quinoa con ajo picado. Saltear el pavo molido magro hasta dorar. Servir la carne sobre la quinoa y añadir una cucharada de mantequilla de maní como fuente de energía.", "Bowl Muscle de Pavo y Quinoa", 3 },
                    { new Guid("e5555555-5555-5555-5555-555555555553"), "Sellar el lomo de cordero con cebolla blanca. Añadir la cebada perlada, nabo y agua. Cocinar a fuego lento y agregar papas cocidas con piel al final.", "Estofado de Cordero y Cebada", 2 },
                    { new Guid("e5555555-5555-5555-5555-555555555554"), "Cocinar el lenguado al vapor utilizando el consomé de pavo para aromatizar. Servir acompañado de zapallo y raíces verdes al vapor.", "Lenguado al Vapor Light", 3 },
                    { new Guid("e5555555-5555-5555-5555-555555555556"), "Colocar una rebanada de jamón de pavo sobre la galleta de arroz inflado. Añadir una hoja de lechuga y tiras de zanahoria fresca para dar crocancia.", "Snack de Arroz y Pavo", 4 }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "PlanItemTemplate",
                columns: new[] { "Id", "NumeroDia", "PlanTemplateId", "RecetaId" },
                values: new object[,]
                {
                    { 104, 1, 1, new Guid("d4444444-4444-4444-4444-444444444444") },
                    { 105, 1, 1, new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 106, 1, 1, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 107, 1, 1, new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 108, 2, 1, new Guid("d4444444-4444-4444-4444-444444444444") },
                    { 109, 2, 1, new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 110, 2, 1, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 111, 2, 1, new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 112, 3, 1, new Guid("d4444444-4444-4444-4444-444444444444") },
                    { 113, 3, 1, new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 114, 3, 1, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 115, 3, 1, new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 116, 4, 1, new Guid("d4444444-4444-4444-4444-444444444444") },
                    { 117, 4, 1, new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 118, 4, 1, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 119, 4, 1, new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 120, 5, 1, new Guid("d4444444-4444-4444-4444-444444444444") },
                    { 121, 5, 1, new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 122, 5, 1, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 123, 5, 1, new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 124, 6, 1, new Guid("d4444444-4444-4444-4444-444444444444") },
                    { 125, 6, 1, new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 126, 6, 1, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 127, 6, 1, new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 128, 7, 1, new Guid("d4444444-4444-4444-4444-444444444444") },
                    { 129, 7, 1, new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 130, 7, 1, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 131, 7, 1, new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 132, 8, 1, new Guid("d4444444-4444-4444-4444-444444444444") },
                    { 133, 8, 1, new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 134, 8, 1, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 135, 8, 1, new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 136, 9, 1, new Guid("d4444444-4444-4444-4444-444444444444") },
                    { 137, 9, 1, new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 138, 9, 1, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 139, 9, 1, new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 140, 10, 1, new Guid("d4444444-4444-4444-4444-444444444444") },
                    { 141, 10, 1, new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 142, 10, 1, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 143, 10, 1, new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 144, 11, 1, new Guid("d4444444-4444-4444-4444-444444444444") },
                    { 145, 11, 1, new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 146, 11, 1, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 147, 11, 1, new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 148, 12, 1, new Guid("d4444444-4444-4444-4444-444444444444") },
                    { 149, 12, 1, new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 150, 12, 1, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 151, 12, 1, new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 152, 13, 1, new Guid("d4444444-4444-4444-4444-444444444444") },
                    { 153, 13, 1, new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 154, 13, 1, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 155, 13, 1, new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 156, 14, 1, new Guid("d4444444-4444-4444-4444-444444444444") },
                    { 157, 14, 1, new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 158, 14, 1, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 159, 14, 1, new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 160, 15, 1, new Guid("d4444444-4444-4444-4444-444444444444") },
                    { 161, 15, 1, new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 162, 15, 1, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 163, 15, 1, new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 204, 1, 2, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 205, 1, 2, new Guid("e5555555-5555-5555-5555-555555555553") },
                    { 206, 1, 2, new Guid("f6666666-6666-6666-6666-666666666666") },
                    { 207, 1, 2, new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 208, 2, 2, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 209, 2, 2, new Guid("a1111111-1111-1111-1111-111111111111") },
                    { 210, 2, 2, new Guid("f6666666-6666-6666-6666-666666666666") },
                    { 211, 2, 2, new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 212, 3, 2, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 213, 3, 2, new Guid("e5555555-5555-5555-5555-555555555553") },
                    { 214, 3, 2, new Guid("f6666666-6666-6666-6666-666666666666") },
                    { 215, 3, 2, new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 216, 4, 2, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 217, 4, 2, new Guid("a1111111-1111-1111-1111-111111111111") },
                    { 218, 4, 2, new Guid("f6666666-6666-6666-6666-666666666666") },
                    { 219, 4, 2, new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 220, 5, 2, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 221, 5, 2, new Guid("e5555555-5555-5555-5555-555555555553") },
                    { 222, 5, 2, new Guid("f6666666-6666-6666-6666-666666666666") },
                    { 223, 5, 2, new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 224, 6, 2, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 225, 6, 2, new Guid("a1111111-1111-1111-1111-111111111111") },
                    { 226, 6, 2, new Guid("f6666666-6666-6666-6666-666666666666") },
                    { 227, 6, 2, new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 228, 7, 2, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 229, 7, 2, new Guid("e5555555-5555-5555-5555-555555555553") },
                    { 230, 7, 2, new Guid("f6666666-6666-6666-6666-666666666666") },
                    { 231, 7, 2, new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 232, 8, 2, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 233, 8, 2, new Guid("a1111111-1111-1111-1111-111111111111") },
                    { 234, 8, 2, new Guid("f6666666-6666-6666-6666-666666666666") },
                    { 235, 8, 2, new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 236, 9, 2, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 237, 9, 2, new Guid("e5555555-5555-5555-5555-555555555553") },
                    { 238, 9, 2, new Guid("f6666666-6666-6666-6666-666666666666") },
                    { 239, 9, 2, new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 240, 10, 2, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 241, 10, 2, new Guid("a1111111-1111-1111-1111-111111111111") },
                    { 242, 10, 2, new Guid("f6666666-6666-6666-6666-666666666666") },
                    { 243, 10, 2, new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 244, 11, 2, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 245, 11, 2, new Guid("e5555555-5555-5555-5555-555555555553") },
                    { 246, 11, 2, new Guid("f6666666-6666-6666-6666-666666666666") },
                    { 247, 11, 2, new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 248, 12, 2, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 249, 12, 2, new Guid("a1111111-1111-1111-1111-111111111111") },
                    { 250, 12, 2, new Guid("f6666666-6666-6666-6666-666666666666") },
                    { 251, 12, 2, new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 252, 13, 2, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 253, 13, 2, new Guid("e5555555-5555-5555-5555-555555555553") },
                    { 254, 13, 2, new Guid("f6666666-6666-6666-6666-666666666666") },
                    { 255, 13, 2, new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 256, 14, 2, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 257, 14, 2, new Guid("a1111111-1111-1111-1111-111111111111") },
                    { 258, 14, 2, new Guid("f6666666-6666-6666-6666-666666666666") },
                    { 259, 14, 2, new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 260, 15, 2, new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 261, 15, 2, new Guid("e5555555-5555-5555-5555-555555555553") },
                    { 262, 15, 2, new Guid("f6666666-6666-6666-6666-666666666666") },
                    { 263, 15, 2, new Guid("e5555555-5555-5555-5555-555555555552") }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "RecetaIngrediente",
                columns: new[] { "Id", "CantidadValor", "IngredienteId", "RecetaId" },
                values: new object[,]
                {
                    { 10, 1m, new Guid("02e196f1-1b17-45fe-9a8f-731651f53f8b"), new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 11, 1m, new Guid("4200520d-dcf4-424b-8b07-d9459e3e2f11"), new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 12, 5m, new Guid("975b0575-f99a-42a9-be76-715a509fea59"), new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 13, 1m, new Guid("98746e75-69fc-4c53-a7b5-ea56df561bbd"), new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 14, 1m, new Guid("01cee8ad-d60c-43fd-9337-8ab3cc3820f2"), new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 15, 1m, new Guid("18018540-a0ec-4a86-806f-5772f99b58df"), new Guid("e5555555-5555-5555-5555-555555555551") },
                    { 20, 1m, new Guid("9ddff76b-d8c4-450d-ad00-0ddb152cdf91"), new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 21, 1m, new Guid("8ac032d5-7aa2-400b-b0aa-f969fe44a99b"), new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 22, 1m, new Guid("e984d4e9-f9d8-46b7-b078-3375237e2865"), new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 23, 1m, new Guid("8d5c9a34-bb53-4b8c-88bf-fc399b1d04b1"), new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 24, 1m, new Guid("0a5b2076-f3e6-4aad-8106-a3c385108531"), new Guid("e5555555-5555-5555-5555-555555555552") },
                    { 30, 1m, new Guid("c430d1d8-ad26-4aa6-8304-b3b0ee00d60b"), new Guid("e5555555-5555-5555-5555-555555555553") },
                    { 31, 1m, new Guid("3ac58f2f-734e-46c6-836b-04af33deccbd"), new Guid("e5555555-5555-5555-5555-555555555553") },
                    { 32, 1m, new Guid("cd365a29-d84f-4ca1-a854-e6a6887e9da4"), new Guid("e5555555-5555-5555-5555-555555555553") },
                    { 33, 1m, new Guid("a723cbeb-2733-4559-8590-e946072010c6"), new Guid("e5555555-5555-5555-5555-555555555553") },
                    { 34, 1m, new Guid("0e6f9613-7ca8-4d16-aca9-520c17561219"), new Guid("e5555555-5555-5555-5555-555555555553") },
                    { 40, 1m, new Guid("09103b00-9d4c-49c6-995a-51e8928ff585"), new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 41, 1m, new Guid("4f7f1592-8ad0-40dc-98d9-a33caf68d91b"), new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 42, 1m, new Guid("68a43539-0fd9-430a-a673-19161a744413"), new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 43, 1m, new Guid("24e77645-5d8f-4f08-b020-76ab6c73d428"), new Guid("e5555555-5555-5555-5555-555555555554") },
                    { 50, 1m, new Guid("4bbcf803-0372-4515-b700-42749a77e1ba"), new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 51, 1m, new Guid("3bc78b5b-8a70-480c-a372-b7d150dce38d"), new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 52, 1m, new Guid("975b0575-f99a-42a9-be76-715a509fea59"), new Guid("e5555555-5555-5555-5555-555555555556") },
                    { 53, 1m, new Guid("4200520d-dcf4-424b-8b07-d9459e3e2f11"), new Guid("e5555555-5555-5555-5555-555555555556") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanItemTemplate_PlanTemplateId",
                schema: "public",
                table: "PlanItemTemplate",
                column: "PlanTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanItemTemplate_RecetaId",
                schema: "public",
                table: "PlanItemTemplate",
                column: "RecetaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanItemTemplate",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PlanTemplate",
                schema: "public");

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "RecetaIngrediente",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "Receta",
                keyColumn: "Id",
                keyValue: new Guid("e5555555-5555-5555-5555-555555555551"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "Receta",
                keyColumn: "Id",
                keyValue: new Guid("e5555555-5555-5555-5555-555555555552"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "Receta",
                keyColumn: "Id",
                keyValue: new Guid("e5555555-5555-5555-5555-555555555553"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "Receta",
                keyColumn: "Id",
                keyValue: new Guid("e5555555-5555-5555-5555-555555555554"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "Receta",
                keyColumn: "Id",
                keyValue: new Guid("e5555555-5555-5555-5555-555555555556"));
        }
    }
}
