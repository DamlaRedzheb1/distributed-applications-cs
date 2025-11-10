using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenGardenCatalog.Migrations
{
    /// <inheritdoc />
    public partial class FertilizerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FertilizedId",
                table: "Plants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FertilizerId",
                table: "Plants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Fertilizers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fertilizers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plants_FertilizerId",
                table: "Plants",
                column: "FertilizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Fertilizers_FertilizerId",
                table: "Plants",
                column: "FertilizerId",
                principalTable: "Fertilizers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Fertilizers_FertilizerId",
                table: "Plants");

            migrationBuilder.DropTable(
                name: "Fertilizers");

            migrationBuilder.DropIndex(
                name: "IX_Plants_FertilizerId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "FertilizedId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "FertilizerId",
                table: "Plants");
        }
    }
}
