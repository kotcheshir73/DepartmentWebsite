using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class EditEventAndComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_DepartmentUsers_DepartmentUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_DepartmentUsers_DepartmentUserId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_DepartmentUserId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Comments_DepartmentUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "DepartmentUserId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DepartmentUserId",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentUser",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentUser",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentUser",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DepartmentUser",
                table: "Comments");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentUserId",
                table: "Events",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentUserId",
                table: "Comments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Events_DepartmentUserId",
                table: "Events",
                column: "DepartmentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_DepartmentUserId",
                table: "Comments",
                column: "DepartmentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_DepartmentUsers_DepartmentUserId",
                table: "Comments",
                column: "DepartmentUserId",
                principalTable: "DepartmentUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_DepartmentUsers_DepartmentUserId",
                table: "Events",
                column: "DepartmentUserId",
                principalTable: "DepartmentUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
