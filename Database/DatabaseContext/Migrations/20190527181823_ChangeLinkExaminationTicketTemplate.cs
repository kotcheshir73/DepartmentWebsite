using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class ChangeLinkExaminationTicketTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplates_ExaminationTemplates_ExaminationTemplateId",
                table: "TicketTemplates");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplates_ExaminationTemplateId",
                table: "TicketTemplates");

            migrationBuilder.DropColumn(
                name: "ExaminationTemplateId",
                table: "TicketTemplates");

            migrationBuilder.AddColumn<Guid>(
                name: "ExaminationTemplateId",
                table: "ExaminationTemplates",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TicketTemplateId",
                table: "ExaminationTemplates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationTemplates_ExaminationTemplateId",
                table: "ExaminationTemplates",
                column: "ExaminationTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationTemplates_TicketTemplates_ExaminationTemplateId",
                table: "ExaminationTemplates",
                column: "ExaminationTemplateId",
                principalTable: "TicketTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationTemplates_TicketTemplates_ExaminationTemplateId",
                table: "ExaminationTemplates");

            migrationBuilder.DropIndex(
                name: "IX_ExaminationTemplates_ExaminationTemplateId",
                table: "ExaminationTemplates");

            migrationBuilder.DropColumn(
                name: "ExaminationTemplateId",
                table: "ExaminationTemplates");

            migrationBuilder.DropColumn(
                name: "TicketTemplateId",
                table: "ExaminationTemplates");

            migrationBuilder.AddColumn<Guid>(
                name: "ExaminationTemplateId",
                table: "TicketTemplates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplates_ExaminationTemplateId",
                table: "TicketTemplates",
                column: "ExaminationTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplates_ExaminationTemplates_ExaminationTemplateId",
                table: "TicketTemplates",
                column: "ExaminationTemplateId",
                principalTable: "ExaminationTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
