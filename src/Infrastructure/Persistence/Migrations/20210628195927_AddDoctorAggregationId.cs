using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DocHelper.Infrastructure.Persistence.Migrations
{
    public partial class AddDoctorAggregationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AggregateId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AggregateId",
                table: "Doctors");
        }
    }
}
