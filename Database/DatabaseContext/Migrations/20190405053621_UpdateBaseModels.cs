using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class UpdateBaseModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StewardName",
                table: "StudentGroups");

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "DepartmentUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "DepartmentUsers");

            migrationBuilder.AddColumn<string>(
                name: "StewardName",
                table: "StudentGroups",
                nullable: true);
        }
    }
}
