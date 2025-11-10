using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenGardenCatalog.Migrations
{
    /// <inheritdoc />
    public partial class SoilEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoilId",
                table: "Plants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Soils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ph = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soils", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plants_SoilId",
                table: "Plants",
                column: "SoilId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Soils_SoilId",
                table: "Plants",
                column: "SoilId",
                principalTable: "Soils",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Soils_SoilId",
                table: "Plants");

            migrationBuilder.DropTable(
                name: "Soils");

            migrationBuilder.DropIndex(
                name: "IX_Plants_SoilId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "SoilId",
                table: "Plants");
        }
    }
}
