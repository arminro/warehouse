using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Data.Migrations
{
    /// <inheritdoc />
    public partial class WarehouseStateChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StateChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangeTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ComponentId = table.Column<int>(type: "int", nullable: true),
                    Direction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateChanges_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StateChanges_ComponentId",
                table: "StateChanges",
                column: "ComponentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StateChanges");
        }
    }
}
