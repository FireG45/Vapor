using Microsoft.EntityFrameworkCore.Migrations;

namespace Vapor.Migrations
{
    public partial class asasd24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Img2",
                table: "Item",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Img3",
                table: "Item",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Img4",
                table: "Item",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Img5",
                table: "Item",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img2",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Img3",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Img4",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Img5",
                table: "Item");
        }
    }
}
