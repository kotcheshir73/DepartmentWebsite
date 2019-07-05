using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class ParagraphNotNulls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateParagraphs_TicketTemplateBodies_TicketTemplateBodyId",
                table: "TicketTemplateParagraphs");

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketTemplateParagraphPropertiesId",
                table: "TicketTemplateParagraphs",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketTemplateBodyId",
                table: "TicketTemplateParagraphs",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketTemplateParagraphRunPropertiesId",
                table: "TicketTemplateParagraphRuns",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateParagraphs_TicketTemplateBodies_TicketTemplateBodyId",
                table: "TicketTemplateParagraphs",
                column: "TicketTemplateBodyId",
                principalTable: "TicketTemplateBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateParagraphs_TicketTemplateBodies_TicketTemplateBodyId",
                table: "TicketTemplateParagraphs");

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketTemplateParagraphPropertiesId",
                table: "TicketTemplateParagraphs",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketTemplateBodyId",
                table: "TicketTemplateParagraphs",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketTemplateParagraphRunPropertiesId",
                table: "TicketTemplateParagraphRuns",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateParagraphs_TicketTemplateBodies_TicketTemplateBodyId",
                table: "TicketTemplateParagraphs",
                column: "TicketTemplateBodyId",
                principalTable: "TicketTemplateBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
