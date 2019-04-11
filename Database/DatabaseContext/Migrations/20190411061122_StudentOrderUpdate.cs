using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class StudentOrderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentGromFromId",
                table: "StudentOrderBlockStudents",
                newName: "StudentGroupFromId");

            migrationBuilder.AddColumn<Guid>(
                name: "EducationDirectionId",
                table: "StudentOrderBlocks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentOrderBlocks_EducationDirectionId",
                table: "StudentOrderBlocks",
                column: "EducationDirectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentOrderBlocks_EducationDirections_EducationDirectionId",
                table: "StudentOrderBlocks",
                column: "EducationDirectionId",
                principalTable: "EducationDirections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentOrderBlocks_EducationDirections_EducationDirectionId",
                table: "StudentOrderBlocks");

            migrationBuilder.DropIndex(
                name: "IX_StudentOrderBlocks_EducationDirectionId",
                table: "StudentOrderBlocks");

            migrationBuilder.DropColumn(
                name: "EducationDirectionId",
                table: "StudentOrderBlocks");

            migrationBuilder.RenameColumn(
                name: "StudentGroupFromId",
                table: "StudentOrderBlockStudents",
                newName: "StudentGromFromId");
        }
    }
}
