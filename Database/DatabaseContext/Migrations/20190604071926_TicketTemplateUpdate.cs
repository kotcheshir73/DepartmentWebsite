using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class TicketTemplateUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateBodies_TicketTemplateElementaryUnits_BodyFormatId",
                table: "TicketTemplateBodies");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateBodies_TicketTemplates_TicketTemplateId",
                table: "TicketTemplateBodies");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateElementaryAttributes_TicketTemplateParagraphDatas_TicketTemplateParagraphDataId",
                table: "TicketTemplateElementaryAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateElementaryUnits_TicketTemplateParagraphDatas_TicketTemplateParagraphDataId",
                table: "TicketTemplateElementaryUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateParagraphs_TicketTemplateElementaryUnits_ParagraphFormatId",
                table: "TicketTemplateParagraphs");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateParagraphs_TicketTemplateBodies_TicketTemplateBodyId",
                table: "TicketTemplateParagraphs");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplates_ExaminationTemplates_ExaminationTemplateId",
                table: "TicketTemplates");

            migrationBuilder.DropTable(
                name: "TicketTemplateParagraphDatas");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplates_ExaminationTemplateId",
                table: "TicketTemplates");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateParagraphs_ParagraphFormatId",
                table: "TicketTemplateParagraphs");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateElementaryUnits_TicketTemplateParagraphDataId",
                table: "TicketTemplateElementaryUnits");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateElementaryAttributes_TicketTemplateParagraphDataId",
                table: "TicketTemplateElementaryAttributes");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateBodies_BodyFormatId",
                table: "TicketTemplateBodies");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateBodies_TicketTemplateId",
                table: "TicketTemplateBodies");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "TicketTemplates");

            migrationBuilder.DropColumn(
                name: "DateDelete",
                table: "TicketTemplates");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TicketTemplates");

            migrationBuilder.DropColumn(
                name: "XML",
                table: "TicketTemplates");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "TicketTemplateParagraphs");

            migrationBuilder.DropColumn(
                name: "DateDelete",
                table: "TicketTemplateParagraphs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TicketTemplateParagraphs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TicketTemplateParagraphs");

            migrationBuilder.DropColumn(
                name: "BodyFormatId",
                table: "TicketTemplateBodies");

            migrationBuilder.DropColumn(
                name: "BodyName",
                table: "TicketTemplateBodies");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "TicketTemplateBodies");

            migrationBuilder.DropColumn(
                name: "DateDelete",
                table: "TicketTemplateBodies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TicketTemplateBodies");

            migrationBuilder.DropColumn(
                name: "SectName",
                table: "TicketTemplateBodies");

            migrationBuilder.RenameColumn(
                name: "ExaminationTemplateId",
                table: "TicketTemplates",
                newName: "TicketTemplateBodyId");

            migrationBuilder.RenameColumn(
                name: "ParagraphFormatId",
                table: "TicketTemplateParagraphs",
                newName: "TicketTemplateParagraphPropertiesId");

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketTemplateBodyId",
                table: "TicketTemplateParagraphs",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketTemplateId",
                table: "TicketTemplateBodies",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TicketTemplateBodyPropertiesId",
                table: "TicketTemplateBodies",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ExaminationTemplateId",
                table: "ExaminationTemplates",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TicketTemplateId",
                table: "ExaminationTemplates",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TicketTemplateBodyProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TicketTemplateBodyId = table.Column<Guid>(nullable: false),
                    PageSizeHeight = table.Column<string>(nullable: true),
                    PageSizeWidth = table.Column<string>(nullable: true),
                    PageSizeOrient = table.Column<string>(nullable: true),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketTemplateParagraphProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
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
                name: "TicketTemplateParagraphRuns",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TicketTemplateParagraphId = table.Column<Guid>(nullable: false),
                    TicketTemplateParagraphRunPropertiesId = table.Column<Guid>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    TabChar = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateParagraphRuns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateParagraphRuns_TicketTemplateParagraphs_TicketTemplateParagraphId",
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
                        name: "FK_TicketTemplateParagraphRunProperties_TicketTemplateParagraphRuns_TicketTemplateParagraphRunId",
                        column: x => x.TicketTemplateParagraphRunId,
                        principalTable: "TicketTemplateParagraphRuns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateBodies_TicketTemplateId",
                table: "TicketTemplateBodies",
                column: "TicketTemplateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationTemplates_ExaminationTemplateId",
                table: "ExaminationTemplates",
                column: "ExaminationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateBodyProperties_TicketTemplateBodyId",
                table: "TicketTemplateBodyProperties",
                column: "TicketTemplateBodyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphProperties_TicketTemplateParagraphId",
                table: "TicketTemplateParagraphProperties",
                column: "TicketTemplateParagraphId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphRunProperties_TicketTemplateParagraphRunId",
                table: "TicketTemplateParagraphRunProperties",
                column: "TicketTemplateParagraphRunId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphRuns_TicketTemplateParagraphId",
                table: "TicketTemplateParagraphRuns",
                column: "TicketTemplateParagraphId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationTemplates_TicketTemplates_ExaminationTemplateId",
                table: "ExaminationTemplates",
                column: "ExaminationTemplateId",
                principalTable: "TicketTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateBodies_TicketTemplates_TicketTemplateId",
                table: "TicketTemplateBodies",
                column: "TicketTemplateId",
                principalTable: "TicketTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateParagraphs_TicketTemplateBodies_TicketTemplateBodyId",
                table: "TicketTemplateParagraphs",
                column: "TicketTemplateBodyId",
                principalTable: "TicketTemplateBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationTemplates_TicketTemplates_ExaminationTemplateId",
                table: "ExaminationTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateBodies_TicketTemplates_TicketTemplateId",
                table: "TicketTemplateBodies");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateParagraphs_TicketTemplateBodies_TicketTemplateBodyId",
                table: "TicketTemplateParagraphs");

            migrationBuilder.DropTable(
                name: "TicketTemplateBodyProperties");

            migrationBuilder.DropTable(
                name: "TicketTemplateParagraphProperties");

            migrationBuilder.DropTable(
                name: "TicketTemplateParagraphRunProperties");

            migrationBuilder.DropTable(
                name: "TicketTemplateParagraphRuns");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateBodies_TicketTemplateId",
                table: "TicketTemplateBodies");

            migrationBuilder.DropIndex(
                name: "IX_ExaminationTemplates_ExaminationTemplateId",
                table: "ExaminationTemplates");

            migrationBuilder.DropColumn(
                name: "TicketTemplateBodyPropertiesId",
                table: "TicketTemplateBodies");

            migrationBuilder.DropColumn(
                name: "ExaminationTemplateId",
                table: "ExaminationTemplates");

            migrationBuilder.DropColumn(
                name: "TicketTemplateId",
                table: "ExaminationTemplates");

            migrationBuilder.RenameColumn(
                name: "TicketTemplateBodyId",
                table: "TicketTemplates",
                newName: "ExaminationTemplateId");

            migrationBuilder.RenameColumn(
                name: "TicketTemplateParagraphPropertiesId",
                table: "TicketTemplateParagraphs",
                newName: "ParagraphFormatId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "TicketTemplates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDelete",
                table: "TicketTemplates",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TicketTemplates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "XML",
                table: "TicketTemplates",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketTemplateBodyId",
                table: "TicketTemplateParagraphs",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "TicketTemplateParagraphs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDelete",
                table: "TicketTemplateParagraphs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TicketTemplateParagraphs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TicketTemplateParagraphs",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketTemplateId",
                table: "TicketTemplateBodies",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "BodyFormatId",
                table: "TicketTemplateBodies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BodyName",
                table: "TicketTemplateBodies",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "TicketTemplateBodies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDelete",
                table: "TicketTemplateBodies",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TicketTemplateBodies",
                nullable: false,
                defaultValue: false);

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
                name: "IX_TicketTemplates_ExaminationTemplateId",
                table: "TicketTemplates",
                column: "ExaminationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphs_ParagraphFormatId",
                table: "TicketTemplateParagraphs",
                column: "ParagraphFormatId");

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
                name: "IX_TicketTemplateBodies_TicketTemplateId",
                table: "TicketTemplateBodies",
                column: "TicketTemplateId");

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
                name: "FK_TicketTemplateBodies_TicketTemplates_TicketTemplateId",
                table: "TicketTemplateBodies",
                column: "TicketTemplateId",
                principalTable: "TicketTemplates",
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

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateParagraphs_TicketTemplateElementaryUnits_ParagraphFormatId",
                table: "TicketTemplateParagraphs",
                column: "ParagraphFormatId",
                principalTable: "TicketTemplateElementaryUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateParagraphs_TicketTemplateBodies_TicketTemplateBodyId",
                table: "TicketTemplateParagraphs",
                column: "TicketTemplateBodyId",
                principalTable: "TicketTemplateBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
