using Microsoft.EntityFrameworkCore.Migrations;

namespace DocHelper.Infrastructure.Persistence.Migrations
{
    public partial class RemoveCityIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cities_Name_Alias",
                table: "Cities");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name_Alias",
                table: "Cities",
                columns: new[] { "Name", "Alias" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cities_Name_Alias",
                table: "Cities");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name_Alias",
                table: "Cities",
                columns: new[] { "Name", "Alias" },
                unique: true);
        }
    }
}
