using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class StudentOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentHistorys");

            migrationBuilder.CreateTable(
                name: "StudentOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrderNumber = table.Column<string>(nullable: false),
                    StudentOrderType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentOrderBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StudentOrderId = table.Column<Guid>(nullable: false),
                    StudentOrderType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentOrderBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentOrderBlocks_StudentOrders_StudentOrderId",
                        column: x => x.StudentOrderId,
                        principalTable: "StudentOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentOrderBlockStudents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StudentOrderBlockId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    StudentGromFromId = table.Column<Guid>(nullable: true),
                    StudentGroupToId = table.Column<Guid>(nullable: true),
                    Info = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentOrderBlockStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentOrderBlockStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentOrderBlockStudents_StudentOrderBlocks_StudentOrderBlockId",
                        column: x => x.StudentOrderBlockId,
                        principalTable: "StudentOrderBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentOrderBlocks_StudentOrderId",
                table: "StudentOrderBlocks",
                column: "StudentOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentOrderBlockStudents_StudentId",
                table: "StudentOrderBlockStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentOrderBlockStudents_StudentOrderBlockId",
                table: "StudentOrderBlockStudents",
                column: "StudentOrderBlockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentOrderBlockStudents");

            migrationBuilder.DropTable(
                name: "StudentOrderBlocks");

            migrationBuilder.DropTable(
                name: "StudentOrders");

            migrationBuilder.CreateTable(
                name: "StudentHistorys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    TextMessage = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentHistorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentHistorys_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentHistorys_StudentId",
                table: "StudentHistorys",
                column: "StudentId");
        }
    }
}
