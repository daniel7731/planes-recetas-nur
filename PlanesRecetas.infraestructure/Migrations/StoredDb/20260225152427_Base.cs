using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanesRecetas.infraestructure.Migrations.StoredDb
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "outboxMessage",
                schema: "outbox");
        }
    }
}
