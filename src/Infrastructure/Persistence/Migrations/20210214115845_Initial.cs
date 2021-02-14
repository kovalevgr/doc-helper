using Microsoft.EntityFrameworkCore.Migrations;

namespace DocHelper.Infrastructure.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false),
                    Alias = table.Column<string>( "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_Cities", x => x.Id));
            
            migrationBuilder.CreateIndex(
                name: "IX_Cities_Names_Aliases_Unique",
                table: "cities",
                columns: new []{ "Name", "Alias" },
                unique: true);

            migrationBuilder.CreateTable(
                name: "specialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>( "nvarchar(128)", maxLength: 128, nullable: false),
                    Alias = table.Column<string>( "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_Specialties", x => x.Id));

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "cities");
            migrationBuilder.DropTable(name: "specialties");
        }
    }
}
