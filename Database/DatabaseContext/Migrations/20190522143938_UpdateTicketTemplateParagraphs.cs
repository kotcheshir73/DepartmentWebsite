using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class UpdateTicketTemplateParagraphs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateParagraphs_TicketTemplateElementaryUnits_ParagraphFormatId",
                table: "TicketTemplateParagraphs");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateParagraphs_ParagraphFormatId",
                table: "TicketTemplateParagraphs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TicketTemplateParagraphs");

            migrationBuilder.RenameColumn(
                name: "ParagraphFormatId",
                table: "TicketTemplateParagraphs",
                newName: "TicketTemplateParagraphPropertiesId");

            migrationBuilder.CreateTable(
                name: "TicketTemplateParagraphProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TicketTemplateParagraphId = table.Column<Guid>(nullable: false),
                    Justification = table.Column<string>(nullable: true),
                    SpacingBetweenLinesLine = table.Column<string>(nullable: true),
                    SpacingBetweenLinesLineRule = table.Column<string>(nullable: true),
                    SpacingBetweenLinesBefore = table.Column<string>(nullable: true),
                    SpacingBetweenLinesAfter = table.Column<string>(nullable: true),
                    IndentationFirstLine = table.Column<string>(nullable: true),
                    IndentationHanging = table.Column<string>(nullable: true),
                    IndentationLeft = table.Column<string>(nullable: true),
                    IndentationRight = table.Column<string>(nullable: true),
                    RunBold = table.Column<bool>(nullable: false),
                    RunItalic = table.Column<bool>(nullable: false),
                    RunUnderline = table.Column<bool>(nullable: false),
                    RunSize = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateParagraphProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateParagraphProperties_TicketTemplateParagraphs_TicketTemplateParagraphId",
                        column: x => x.TicketTemplateParagraphId,
                        principalTable: "TicketTemplateParagraphs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketTemplateParagraphRun",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TicketTemplateParagraphId = table.Column<Guid>(nullable: false),
                    TicketTemplateRunPropertiesId = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateParagraphRun", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateParagraphRun_TicketTemplateParagraphs_TicketTemplateParagraphId",
                        column: x => x.TicketTemplateParagraphId,
                        principalTable: "TicketTemplateParagraphs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketTemplateParagraphRunProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TicketTemplateParagraphRunId = table.Column<Guid>(nullable: false),
                    RunBold = table.Column<bool>(nullable: false),
                    RunItalic = table.Column<bool>(nullable: false),
                    RunUnderline = table.Column<bool>(nullable: false),
                    RunSize = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateParagraphRunProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateParagraphRunProperties_TicketTemplateParagraphRun_TicketTemplateParagraphRunId",
                        column: x => x.TicketTemplateParagraphRunId,
                        principalTable: "TicketTemplateParagraphRun",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphProperties_TicketTemplateParagraphId",
                table: "TicketTemplateParagraphProperties",
                column: "TicketTemplateParagraphId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphRun_TicketTemplateParagraphId",
                table: "TicketTemplateParagraphRun",
                column: "TicketTemplateParagraphId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphRunProperties_TicketTemplateParagraphRunId",
                table: "TicketTemplateParagraphRunProperties",
                column: "TicketTemplateParagraphRunId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketTemplateParagraphProperties");

            migrationBuilder.DropTable(
                name: "TicketTemplateParagraphRunProperties");

            migrationBuilder.DropTable(
                name: "TicketTemplateParagraphRun");

            migrationBuilder.RenameColumn(
                name: "TicketTemplateParagraphPropertiesId",
                table: "TicketTemplateParagraphs",
                newName: "ParagraphFormatId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TicketTemplateParagraphs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphs_ParagraphFormatId",
                table: "TicketTemplateParagraphs",
                column: "ParagraphFormatId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateParagraphs_TicketTemplateElementaryUnits_ParagraphFormatId",
                table: "TicketTemplateParagraphs",
                column: "ParagraphFormatId",
                principalTable: "TicketTemplateElementaryUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
