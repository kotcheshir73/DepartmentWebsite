using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class ChangeEventToNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Events_EventId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropColumn(
                name: "DepartmentUser",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Comments",
                newName: "NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_EventId",
                table: "Comments",
                newName: "IX_Comments_NewsId");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentUserId",
                table: "Comments",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "Newses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DepartmentUserId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Body = table.Column<string>(nullable: false),
                    Tag = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Newses_DepartmentUsers_DepartmentUserId",
                        column: x => x.DepartmentUserId,
                        principalTable: "DepartmentUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_DepartmentUserId",
                table: "Comments",
                column: "DepartmentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Newses_DepartmentUserId",
                table: "Newses",
                column: "DepartmentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_DepartmentUsers_DepartmentUserId",
                table: "Comments",
                column: "DepartmentUserId",
                principalTable: "DepartmentUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Newses_NewsId",
                table: "Comments",
                column: "NewsId",
                principalTable: "Newses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_DepartmentUsers_DepartmentUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Newses_NewsId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "Newses");

            migrationBuilder.DropIndex(
                name: "IX_Comments_DepartmentUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "DepartmentUserId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "NewsId",
                table: "Comments",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_NewsId",
                table: "Comments",
                newName: "IX_Comments_EventId");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentUser",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    DepartmentUser = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Tag = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Events_EventId",
                table: "Comments",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
