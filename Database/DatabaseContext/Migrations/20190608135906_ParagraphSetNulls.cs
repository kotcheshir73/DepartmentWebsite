using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class ParagraphSetNulls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "TicketTemplateParagraphPropertiesId",
                table: "TicketTemplateParagraphs",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketTemplateParagraphRunPropertiesId",
                table: "TicketTemplateParagraphRuns",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketTemplateBodyPropertiesId",
                table: "TicketTemplateBodies",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "TicketTemplateParagraphPropertiesId",
                table: "TicketTemplateParagraphs",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketTemplateParagraphRunPropertiesId",
                table: "TicketTemplateParagraphRuns",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketTemplateBodyPropertiesId",
                table: "TicketTemplateBodies",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
