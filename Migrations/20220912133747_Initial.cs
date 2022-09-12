using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iTech.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pet1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pet1Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pet2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pet2Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pet3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pet3Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
