using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreTemplate.App.Identity.Migrations
{
    public partial class firmIdAddedInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FirmId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirmId",
                table: "AspNetUsers");
        }
    }
}
