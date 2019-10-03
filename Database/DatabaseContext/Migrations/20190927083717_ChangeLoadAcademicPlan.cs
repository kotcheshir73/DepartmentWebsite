using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class ChangeLoadAcademicPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisciplineParentId",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "IsParent",
                table: "Disciplines");

            migrationBuilder.AddColumn<Guid>(
                name: "AcademicPlanRecordParentId",
                table: "AcademicPlanRecords",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsParent",
                table: "AcademicPlanRecords",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "AcademicPlanRecords",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Selectable",
                table: "AcademicPlanRecords",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademicPlanRecordParentId",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "IsParent",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "Selectable",
                table: "AcademicPlanRecords");

            migrationBuilder.AddColumn<Guid>(
                name: "DisciplineParentId",
                table: "Disciplines",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsParent",
                table: "Disciplines",
                nullable: false,
                defaultValue: false);
        }
    }
}
