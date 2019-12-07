using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class ChangeBodyDependencyByTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateBodies_TicketTemplateId",
                table: "TicketTemplateBodies");

            migrationBuilder.DropColumn(
                name: "TicketTemplateBodyId",
                table: "TicketTemplates");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateBodies_TicketTemplateId",
                table: "TicketTemplateBodies",
                column: "TicketTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateBodies_TicketTemplateId",
                table: "TicketTemplateBodies");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketTemplateBodyId",
                table: "TicketTemplates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateBodies_TicketTemplateId",
                table: "TicketTemplateBodies",
                column: "TicketTemplateId",
                unique: true);
        }
    }
}
