using Microsoft.EntityFrameworkCore.Migrations;

namespace DocHelper.Infrastructure.Persistence.Migrations
{
    public partial class AddRelationBetweenCityAndSpecialty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Specialties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_CityId",
                table: "Specialties",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specialties_Cities_CityId",
                table: "Specialties",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specialties_Cities_CityId",
                table: "Specialties");

            migrationBuilder.DropIndex(
                name: "IX_Specialties_CityId",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Specialties");
        }
    }
}
