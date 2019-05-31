using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class AddTabChar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TabChar",
                table: "TicketTemplateParagraphRuns",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TabChar",
                table: "TicketTemplateParagraphRuns");
        }
    }
}
