using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class UpdateTicketTemplateBody : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateBodies_TicketTemplateElementaryUnits_BodyFormatId",
                table: "TicketTemplateBodies");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateElementaryAttributes_TicketTemplateParagraphDatas_TicketTemplateParagraphDataId",
                table: "TicketTemplateElementaryAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateElementaryUnits_TicketTemplateParagraphDatas_TicketTemplateParagraphDataId",
                table: "TicketTemplateElementaryUnits");

            migrationBuilder.DropTable(
                name: "TicketTemplateParagraphDatas");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateElementaryUnits_TicketTemplateParagraphDataId",
                table: "TicketTemplateElementaryUnits");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateElementaryAttributes_TicketTemplateParagraphDataId",
                table: "TicketTemplateElementaryAttributes");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateBodies_BodyFormatId",
                table: "TicketTemplateBodies");

            migrationBuilder.DropColumn(
                name: "BodyName",
                table: "TicketTemplateBodies");

            migrationBuilder.DropColumn(
                name: "SectName",
                table: "TicketTemplateBodies");

            migrationBuilder.RenameColumn(
                name: "BodyFormatId",
                table: "TicketTemplateBodies",
                newName: "TicketTemplateBodyPropertiesId");

            migrationBuilder.CreateTable(
                name: "TicketTemplateBodyProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TicketTemplateBodyId = table.Column<Guid>(nullable: true),
                    PageSizeHeight = table.Column<string>(nullable: true),
                    PageSizeWidth = table.Column<string>(nullable: true),
                    PageMarginBottom = table.Column<string>(nullable: true),
                    PageMarginTop = table.Column<string>(nullable: true),
                    PageMarginLeft = table.Column<string>(nullable: true),
                    PageMarginRight = table.Column<string>(nullable: true),
                    PageMarginFooter = table.Column<string>(nullable: true),
                    PageMarginGutter = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateBodyProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateBodyProperties_TicketTemplateBodies_TicketTemplateBodyId",
                        column: x => x.TicketTemplateBodyId,
                        principalTable: "TicketTemplateBodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateBodyProperties_TicketTemplateBodyId",
                table: "TicketTemplateBodyProperties",
                column: "TicketTemplateBodyId",
                unique: true,
                filter: "[TicketTemplateBodyId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketTemplateBodyProperties");

            migrationBuilder.RenameColumn(
                name: "TicketTemplateBodyPropertiesId",
                table: "TicketTemplateBodies",
                newName: "BodyFormatId");

            migrationBuilder.AddColumn<string>(
                name: "BodyName",
                table: "TicketTemplateBodies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SectName",
                table: "TicketTemplateBodies",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TicketTemplateParagraphDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    FontId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    TextName = table.Column<string>(nullable: true),
                    TicketTemplateParagraphId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateParagraphDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateParagraphDatas_TicketTemplateElementaryUnits_FontId",
                        column: x => x.FontId,
                        principalTable: "TicketTemplateElementaryUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketTemplateParagraphDatas_TicketTemplateParagraphs_TicketTemplateParagraphId",
                        column: x => x.TicketTemplateParagraphId,
                        principalTable: "TicketTemplateParagraphs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateElementaryUnits_TicketTemplateParagraphDataId",
                table: "TicketTemplateElementaryUnits",
                column: "TicketTemplateParagraphDataId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateElementaryAttributes_TicketTemplateParagraphDataId",
                table: "TicketTemplateElementaryAttributes",
                column: "TicketTemplateParagraphDataId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateBodies_BodyFormatId",
                table: "TicketTemplateBodies",
                column: "BodyFormatId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphDatas_FontId",
                table: "TicketTemplateParagraphDatas",
                column: "FontId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphDatas_TicketTemplateParagraphId",
                table: "TicketTemplateParagraphDatas",
                column: "TicketTemplateParagraphId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateBodies_TicketTemplateElementaryUnits_BodyFormatId",
                table: "TicketTemplateBodies",
                column: "BodyFormatId",
                principalTable: "TicketTemplateElementaryUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateElementaryAttributes_TicketTemplateParagraphDatas_TicketTemplateParagraphDataId",
                table: "TicketTemplateElementaryAttributes",
                column: "TicketTemplateParagraphDataId",
                principalTable: "TicketTemplateParagraphDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateElementaryUnits_TicketTemplateParagraphDatas_TicketTemplateParagraphDataId",
                table: "TicketTemplateElementaryUnits",
                column: "TicketTemplateParagraphDataId",
                principalTable: "TicketTemplateParagraphDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
