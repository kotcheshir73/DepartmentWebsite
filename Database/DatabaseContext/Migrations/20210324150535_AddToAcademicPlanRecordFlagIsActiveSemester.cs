using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class AddToAcademicPlanRecordFlagIsActiveSemester : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Selectable",
                table: "AcademicPlanRecords",
                newName: "IsUseInWorkload");

            migrationBuilder.RenameColumn(
                name: "IsSelected",
                table: "AcademicPlanRecords",
                newName: "IsChild");

            migrationBuilder.AddColumn<bool>(
                name: "IsActiveSemester",
                table: "AcademicPlanRecords",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActiveSemester",
                table: "AcademicPlanRecords");

            migrationBuilder.RenameColumn(
                name: "IsUseInWorkload",
                table: "AcademicPlanRecords",
                newName: "Selectable");

            migrationBuilder.RenameColumn(
                name: "IsChild",
                table: "AcademicPlanRecords",
                newName: "IsSelected");
        }
    }
}
