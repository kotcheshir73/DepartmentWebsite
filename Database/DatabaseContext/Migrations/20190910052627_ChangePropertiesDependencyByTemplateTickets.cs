using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class ChangePropertiesDependencyByTemplateTickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateTableRowProperties_TicketTemplateTableRowId",
                table: "TicketTemplateTableRowProperties");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateTableProperties_TicketTemplateTableId",
                table: "TicketTemplateTableProperties");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateTableCellProperties_TicketTemplateTableCellId",
                table: "TicketTemplateTableCellProperties");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateParagraphRunProperties_TicketTemplateParagraphRunId",
                table: "TicketTemplateParagraphRunProperties");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateParagraphProperties_TicketTemplateParagraphId",
                table: "TicketTemplateParagraphProperties");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateBodyProperties_TicketTemplateBodyId",
                table: "TicketTemplateBodyProperties");

            migrationBuilder.DropColumn(
                name: "TicketTemplateTablePropertiesId",
                table: "TicketTemplateTables");

            migrationBuilder.DropColumn(
                name: "TicketTemplateTableRowPropertiesId",
                table: "TicketTemplateTableRows");

            migrationBuilder.DropColumn(
                name: "TicketTemplateTableCellPropertiesId",
                table: "TicketTemplateTableCells");

            migrationBuilder.DropColumn(
                name: "TicketTemplateParagraphPropertiesId",
                table: "TicketTemplateParagraphs");

            migrationBuilder.DropColumn(
                name: "TicketTemplateParagraphRunPropertiesId",
                table: "TicketTemplateParagraphRuns");

            migrationBuilder.DropColumn(
                name: "TicketTemplateBodyPropertiesId",
                table: "TicketTemplateBodies");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTableRowProperties_TicketTemplateTableRowId",
                table: "TicketTemplateTableRowProperties",
                column: "TicketTemplateTableRowId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTableProperties_TicketTemplateTableId",
                table: "TicketTemplateTableProperties",
                column: "TicketTemplateTableId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTableCellProperties_TicketTemplateTableCellId",
                table: "TicketTemplateTableCellProperties",
                column: "TicketTemplateTableCellId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphRunProperties_TicketTemplateParagraphRunId",
                table: "TicketTemplateParagraphRunProperties",
                column: "TicketTemplateParagraphRunId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphProperties_TicketTemplateParagraphId",
                table: "TicketTemplateParagraphProperties",
                column: "TicketTemplateParagraphId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateBodyProperties_TicketTemplateBodyId",
                table: "TicketTemplateBodyProperties",
                column: "TicketTemplateBodyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateTableRowProperties_TicketTemplateTableRowId",
                table: "TicketTemplateTableRowProperties");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateTableProperties_TicketTemplateTableId",
                table: "TicketTemplateTableProperties");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateTableCellProperties_TicketTemplateTableCellId",
                table: "TicketTemplateTableCellProperties");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateParagraphRunProperties_TicketTemplateParagraphRunId",
                table: "TicketTemplateParagraphRunProperties");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateParagraphProperties_TicketTemplateParagraphId",
                table: "TicketTemplateParagraphProperties");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateBodyProperties_TicketTemplateBodyId",
                table: "TicketTemplateBodyProperties");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketTemplateTablePropertiesId",
                table: "TicketTemplateTables",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TicketTemplateTableRowPropertiesId",
                table: "TicketTemplateTableRows",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TicketTemplateTableCellPropertiesId",
                table: "TicketTemplateTableCells",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TicketTemplateParagraphPropertiesId",
                table: "TicketTemplateParagraphs",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TicketTemplateParagraphRunPropertiesId",
                table: "TicketTemplateParagraphRuns",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TicketTemplateBodyPropertiesId",
                table: "TicketTemplateBodies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTableRowProperties_TicketTemplateTableRowId",
                table: "TicketTemplateTableRowProperties",
                column: "TicketTemplateTableRowId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTableProperties_TicketTemplateTableId",
                table: "TicketTemplateTableProperties",
                column: "TicketTemplateTableId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTableCellProperties_TicketTemplateTableCellId",
                table: "TicketTemplateTableCellProperties",
                column: "TicketTemplateTableCellId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphRunProperties_TicketTemplateParagraphRunId",
                table: "TicketTemplateParagraphRunProperties",
                column: "TicketTemplateParagraphRunId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphProperties_TicketTemplateParagraphId",
                table: "TicketTemplateParagraphProperties",
                column: "TicketTemplateParagraphId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateBodyProperties_TicketTemplateBodyId",
                table: "TicketTemplateBodyProperties",
                column: "TicketTemplateBodyId",
                unique: true);
        }
    }
}
