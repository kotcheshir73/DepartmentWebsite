using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class SetQualificationToEducationDirection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeNormAcademicLevel",
                table: "TimeNorms");

            migrationBuilder.DropColumn(
                name: "AcademicLevel",
                table: "AcademicPlans");

            migrationBuilder.AddColumn<int>(
                name: "TimeNormEducationDirectionQualification",
                table: "TimeNorms",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Qualification",
                table: "EducationDirections",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeNormEducationDirectionQualification",
                table: "TimeNorms");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "EducationDirections");

            migrationBuilder.AddColumn<int>(
                name: "TimeNormAcademicLevel",
                table: "TimeNorms",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AcademicLevel",
                table: "AcademicPlans",
                nullable: false,
                defaultValue: 0);
        }
    }
}
