using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class AddLectureDepartmentPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Post",
                table: "Lecturers");

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "Lecturers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Lecturers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Lecturers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AddColumn<Guid>(
                name: "LecturerDepartmentPostId",
                table: "Lecturers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LecturerDepartmentPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DepartmentPostTitle = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerDepartmentPosts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_LecturerDepartmentPostId",
                table: "Lecturers",
                column: "LecturerDepartmentPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_LecturerDepartmentPosts_LecturerDepartmentPostId",
                table: "Lecturers",
                column: "LecturerDepartmentPostId",
                principalTable: "LecturerDepartmentPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_LecturerDepartmentPosts_LecturerDepartmentPostId",
                table: "Lecturers");

            migrationBuilder.DropTable(
                name: "LecturerDepartmentPosts");

            migrationBuilder.DropIndex(
                name: "IX_Lecturers_LecturerDepartmentPostId",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "LecturerDepartmentPostId",
                table: "Lecturers");

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "Lecturers",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Lecturers",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Lecturers",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "Post",
                table: "Lecturers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
