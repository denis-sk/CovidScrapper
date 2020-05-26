using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.Infrastructure.Migrations
{
    public partial class ChangeIdToGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoronaCountries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    TotalCases = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NewCases = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDeaths = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NewDeaths = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRecovered = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoronaCountries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoronaCountries");
        }
    }
}
