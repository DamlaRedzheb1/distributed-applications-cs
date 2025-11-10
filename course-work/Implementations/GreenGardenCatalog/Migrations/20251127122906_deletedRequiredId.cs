using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenGardenCatalog.Migrations
{
    /// <inheritdoc />
    public partial class deletedRequiredId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Fertilizers_FertilizerId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "FertilizedId",
                table: "Plants");

            migrationBuilder.AlterColumn<int>(
                name: "FertilizerId",
                table: "Plants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Fertilizers_FertilizerId",
                table: "Plants",
                column: "FertilizerId",
                principalTable: "Fertilizers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Fertilizers_FertilizerId",
                table: "Plants");

            migrationBuilder.AlterColumn<int>(
                name: "FertilizerId",
                table: "Plants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FertilizedId",
                table: "Plants",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Fertilizers_FertilizerId",
                table: "Plants",
                column: "FertilizerId",
                principalTable: "Fertilizers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
