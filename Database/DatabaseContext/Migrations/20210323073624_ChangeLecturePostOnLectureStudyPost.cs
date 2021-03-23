using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class ChangeLecturePostOnLectureStudyPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_LecturerPosts_LecturerPostId",
                table: "Lecturers");

            migrationBuilder.RenameColumn(
                name: "LecturerPostId",
                table: "Lecturers",
                newName: "LecturerStudyPostId");

            migrationBuilder.RenameIndex(
                name: "IX_Lecturers_LecturerPostId",
                table: "Lecturers",
                newName: "IX_Lecturers_LecturerStudyPostId");

            migrationBuilder.CreateTable(
                name: "LecturerStudyPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StudyPostTitle = table.Column<string>(nullable: true),
                    Hours = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerStudyPosts", x => x.Id);
                });

            migrationBuilder.Sql(@"INSERT INTO LecturerStudyPosts (Id, DateCreate, DateDelete, IsDeleted, StudyPostTitle, Hours) 
                                    SELECT Id, DateCreate, DateDelete, IsDeleted, PostTitle, Hours
                                    FROM LecturerPosts");

            migrationBuilder.DropTable(
                name: "LecturerPosts");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_LecturerStudyPosts_LecturerStudyPostId",
                table: "Lecturers",
                column: "LecturerStudyPostId",
                principalTable: "LecturerStudyPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_LecturerStudyPosts_LecturerStudyPostId",
                table: "Lecturers");

            migrationBuilder.RenameColumn(
                name: "LecturerStudyPostId",
                table: "Lecturers",
                newName: "LecturerPostId");

            migrationBuilder.RenameIndex(
                name: "IX_Lecturers_LecturerStudyPostId",
                table: "Lecturers",
                newName: "IX_Lecturers_LecturerPostId");

            migrationBuilder.CreateTable(
                name: "LecturerPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    Hours = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PostTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerPosts", x => x.Id);
                });

            migrationBuilder.Sql(@"INSERT INTO LecturerPosts (Id, DateCreate, DateDelete, IsDeleted, PostTitle, Hours) 
                                    SELECT Id, DateCreate, DateDelete, IsDeleted, StudyPostTitle, Hours
                                    FROM LecturerStudyPosts");

            migrationBuilder.DropTable(
                name: "LecturerStudyPosts");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_LecturerPosts_LecturerPostId",
                table: "Lecturers",
                column: "LecturerPostId",
                principalTable: "LecturerPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
