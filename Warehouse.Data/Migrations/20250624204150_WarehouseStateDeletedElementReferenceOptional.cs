using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Data.Migrations
{
    /// <inheritdoc />
    public partial class WarehouseStateDeletedElementReferenceOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateChanges_Components_ComponentId",
                table: "StateChanges");

            migrationBuilder.AlterColumn<int>(
                name: "ComponentId",
                table: "StateChanges",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ComponentTypeId",
                table: "StateChanges",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StateChanges_ComponentTypeId",
                table: "StateChanges",
                column: "ComponentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StateChanges_ComponentTypes_ComponentTypeId",
                table: "StateChanges",
                column: "ComponentTypeId",
                principalTable: "ComponentTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StateChanges_Components_ComponentId",
                table: "StateChanges",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateChanges_ComponentTypes_ComponentTypeId",
                table: "StateChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_StateChanges_Components_ComponentId",
                table: "StateChanges");

            migrationBuilder.DropIndex(
                name: "IX_StateChanges_ComponentTypeId",
                table: "StateChanges");

            migrationBuilder.DropColumn(
                name: "ComponentTypeId",
                table: "StateChanges");

            migrationBuilder.AlterColumn<int>(
                name: "ComponentId",
                table: "StateChanges",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StateChanges_Components_ComponentId",
                table: "StateChanges",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
