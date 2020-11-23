using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Micro.AppRegistration.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true),
                    Secret = table.Column<string>(nullable: true),
                    ShortCode = table.Column<string>(nullable: true),
                    UseDefaultCode = table.Column<bool>(nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");
        }
    }
}
