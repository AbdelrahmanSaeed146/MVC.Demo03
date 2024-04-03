using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Demo03.DAL.Data.Migrations
{
    public partial class ImageNameColumnForEmp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageName",
                table: "Employees");
        }
    }
}
