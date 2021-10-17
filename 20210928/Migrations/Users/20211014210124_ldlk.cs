using Microsoft.EntityFrameworkCore.Migrations;

namespace Vapor.Migrations.Users
{
    public partial class ldlk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "a",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "a",
                table: "AspNetUsers");
        }
    }
}
