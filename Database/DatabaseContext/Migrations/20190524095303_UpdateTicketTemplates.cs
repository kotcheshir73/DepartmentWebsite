using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class UpdateTicketTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateParagraphRun_TicketTemplateParagraphs_TicketTemplateParagraphId",
                table: "TicketTemplateParagraphRun");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateParagraphRunProperties_TicketTemplateParagraphRun_TicketTemplateParagraphRunId",
                table: "TicketTemplateParagraphRunProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketTemplateParagraphRun",
                table: "TicketTemplateParagraphRun");

            migrationBuilder.DropColumn(
                name: "TicketTemplateRunPropertiesId",
                table: "TicketTemplateParagraphRun");

            migrationBuilder.RenameTable(
                name: "TicketTemplateParagraphRun",
                newName: "TicketTemplateParagraphRuns");

            migrationBuilder.RenameIndex(
                name: "IX_TicketTemplateParagraphRun_TicketTemplateParagraphId",
                table: "TicketTemplateParagraphRuns",
                newName: "IX_TicketTemplateParagraphRuns_TicketTemplateParagraphId");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketTemplateParagraphRunPropertiesId",
                table: "TicketTemplateParagraphRuns",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketTemplateParagraphRuns",
                table: "TicketTemplateParagraphRuns",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateParagraphRunProperties_TicketTemplateParagraphRuns_TicketTemplateParagraphRunId",
                table: "TicketTemplateParagraphRunProperties",
                column: "TicketTemplateParagraphRunId",
                principalTable: "TicketTemplateParagraphRuns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateParagraphRuns_TicketTemplateParagraphs_TicketTemplateParagraphId",
                table: "TicketTemplateParagraphRuns",
                column: "TicketTemplateParagraphId",
                principalTable: "TicketTemplateParagraphs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateParagraphRunProperties_TicketTemplateParagraphRuns_TicketTemplateParagraphRunId",
                table: "TicketTemplateParagraphRunProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateParagraphRuns_TicketTemplateParagraphs_TicketTemplateParagraphId",
                table: "TicketTemplateParagraphRuns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketTemplateParagraphRuns",
                table: "TicketTemplateParagraphRuns");

            migrationBuilder.DropColumn(
                name: "TicketTemplateParagraphRunPropertiesId",
                table: "TicketTemplateParagraphRuns");

            migrationBuilder.RenameTable(
                name: "TicketTemplateParagraphRuns",
                newName: "TicketTemplateParagraphRun");

            migrationBuilder.RenameIndex(
                name: "IX_TicketTemplateParagraphRuns_TicketTemplateParagraphId",
                table: "TicketTemplateParagraphRun",
                newName: "IX_TicketTemplateParagraphRun_TicketTemplateParagraphId");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketTemplateRunPropertiesId",
                table: "TicketTemplateParagraphRun",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketTemplateParagraphRun",
                table: "TicketTemplateParagraphRun",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateParagraphRun_TicketTemplateParagraphs_TicketTemplateParagraphId",
                table: "TicketTemplateParagraphRun",
                column: "TicketTemplateParagraphId",
                principalTable: "TicketTemplateParagraphs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateParagraphRunProperties_TicketTemplateParagraphRun_TicketTemplateParagraphRunId",
                table: "TicketTemplateParagraphRunProperties",
                column: "TicketTemplateParagraphRunId",
                principalTable: "TicketTemplateParagraphRun",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
