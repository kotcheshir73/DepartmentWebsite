using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class UpdateTicketTemplateBody2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateBodies_TicketTemplateId",
                table: "TicketTemplateBodies");

            migrationBuilder.DropColumn(
                name: "XML",
                table: "TicketTemplates");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketTemplateBodyId",
                table: "TicketTemplates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "TicketTemplateParagraphRun",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PageSizeOrient",
                table: "TicketTemplateBodyProperties",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateBodies_TicketTemplateId",
                table: "TicketTemplateBodies",
                column: "TicketTemplateId",
                unique: true,
                filter: "[TicketTemplateId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateBodies_TicketTemplateId",
                table: "TicketTemplateBodies");

            migrationBuilder.DropColumn(
                name: "TicketTemplateBodyId",
                table: "TicketTemplates");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "TicketTemplateParagraphRun");

            migrationBuilder.DropColumn(
                name: "PageSizeOrient",
                table: "TicketTemplateBodyProperties");

            migrationBuilder.AddColumn<string>(
                name: "XML",
                table: "TicketTemplates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateBodies_TicketTemplateId",
                table: "TicketTemplateBodies",
                column: "TicketTemplateId");
        }
    }
}
