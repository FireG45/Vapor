using Microsoft.EntityFrameworkCore.Migrations;

namespace Vapor.Migrations
{
    public partial class absihou263 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Keys",
                table: "Item",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Keys",
                table: "Item");
        }
    }
}
