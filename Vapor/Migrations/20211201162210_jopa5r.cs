using Microsoft.EntityFrameworkCore.Migrations;

namespace Vapor.Migrations
{
    public partial class jopa5r : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Reviews");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Reviews",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
