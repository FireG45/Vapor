using Microsoft.EntityFrameworkCore.Migrations;

namespace Vapor.Migrations
{
    public partial class fff4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "ScoreCount",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0ul);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoreCount",
                table: "Items");
        }
    }
}
