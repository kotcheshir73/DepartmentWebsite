using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class UpdateTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateTableCells_TicketTemplateElementaryUnits_PropertiesId",
                table: "TicketTemplateTableCells");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateTableRows_TicketTemplateElementaryUnits_PropertiesId",
                table: "TicketTemplateTableRows");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateTables_TicketTemplateElementaryUnits_ColumnsId",
                table: "TicketTemplateTables");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateTables_TicketTemplateElementaryUnits_PropertiesId",
                table: "TicketTemplateTables");

            migrationBuilder.DropTable(
                name: "TicketTemplateElementaryAttributes");

            migrationBuilder.DropTable(
                name: "TicketTemplateElementaryUnits");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateTables_ColumnsId",
                table: "TicketTemplateTables");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateTables_PropertiesId",
                table: "TicketTemplateTables");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateTableRows_PropertiesId",
                table: "TicketTemplateTableRows");

            migrationBuilder.DropIndex(
                name: "IX_TicketTemplateTableCells_PropertiesId",
                table: "TicketTemplateTableCells");

            migrationBuilder.DropColumn(
                name: "ColumnsId",
                table: "TicketTemplateTables");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "TicketTemplateTables");

            migrationBuilder.DropColumn(
                name: "DateDelete",
                table: "TicketTemplateTables");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TicketTemplateTables");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TicketTemplateTables");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "TicketTemplateTableRows");

            migrationBuilder.DropColumn(
                name: "DateDelete",
                table: "TicketTemplateTableRows");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TicketTemplateTableRows");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TicketTemplateTableRows");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "TicketTemplateTableCells");

            migrationBuilder.DropColumn(
                name: "DateDelete",
                table: "TicketTemplateTableCells");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TicketTemplateTableCells");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TicketTemplateTableCells");

            migrationBuilder.RenameColumn(
                name: "PropertiesId",
                table: "TicketTemplateTables",
                newName: "TicketTemplateTablePropertiesId");

            migrationBuilder.RenameColumn(
                name: "PropertiesId",
                table: "TicketTemplateTableRows",
                newName: "TicketTemplateTableRowPropertiesId");

            migrationBuilder.RenameColumn(
                name: "PropertiesId",
                table: "TicketTemplateTableCells",
                newName: "TicketTemplateTableCellPropertiesId");

            migrationBuilder.AddColumn<bool>(
                name: "Break",
                table: "TicketTemplateParagraphRuns",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TicketTemplateTableCellProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TicketTemplateTableCellId = table.Column<Guid>(nullable: true),
                    TableCellWidth = table.Column<string>(nullable: true),
                    GridSpan = table.Column<string>(nullable: true),
                    VerticalMerge = table.Column<string>(nullable: true),
                    ShadingValue = table.Column<string>(nullable: true),
                    ShadingColor = table.Column<string>(nullable: true),
                    ShadingFill = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateTableCellProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateTableCellProperties_TicketTemplateTableCells_TicketTemplateTableCellId",
                        column: x => x.TicketTemplateTableCellId,
                        principalTable: "TicketTemplateTableCells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketTemplateTableGridColumn",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TicketTemplateTableId = table.Column<Guid>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Width = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateTableGridColumn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateTableGridColumn_TicketTemplateTables_TicketTemplateTableId",
                        column: x => x.TicketTemplateTableId,
                        principalTable: "TicketTemplateTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketTemplateTableProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TicketTemplateTableId = table.Column<Guid>(nullable: true),
                    Width = table.Column<string>(nullable: true),
                    LookValue = table.Column<string>(nullable: true),
                    LookFirstRow = table.Column<string>(nullable: true),
                    LookLastRow = table.Column<string>(nullable: true),
                    LookFirstColumn = table.Column<string>(nullable: true),
                    LookLastColumn = table.Column<string>(nullable: true),
                    LookNoHorizontalBand = table.Column<string>(nullable: true),
                    LookNoVerticalBand = table.Column<string>(nullable: true),
                    LayoutType = table.Column<string>(nullable: true),
                    BorderTopValue = table.Column<string>(nullable: true),
                    BorderTopColor = table.Column<string>(nullable: true),
                    BorderTopSize = table.Column<string>(nullable: true),
                    BorderTopSpace = table.Column<string>(nullable: true),
                    BorderBottomValue = table.Column<string>(nullable: true),
                    BorderBottomColor = table.Column<string>(nullable: true),
                    BorderBottomSize = table.Column<string>(nullable: true),
                    BorderBottomSpace = table.Column<string>(nullable: true),
                    BorderLeftValue = table.Column<string>(nullable: true),
                    BorderLeftColor = table.Column<string>(nullable: true),
                    BorderLeftSize = table.Column<string>(nullable: true),
                    BorderLeftSpace = table.Column<string>(nullable: true),
                    BorderRightValue = table.Column<string>(nullable: true),
                    BorderRightColor = table.Column<string>(nullable: true),
                    BorderRightSize = table.Column<string>(nullable: true),
                    BorderRightSpace = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateTableProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateTableProperties_TicketTemplateTables_TicketTemplateTableId",
                        column: x => x.TicketTemplateTableId,
                        principalTable: "TicketTemplateTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketTemplateTableRowProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TicketTemplateTableRowId = table.Column<Guid>(nullable: true),
                    CantSplit = table.Column<string>(nullable: true),
                    TableRowHeight = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateTableRowProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateTableRowProperties_TicketTemplateTableRows_TicketTemplateTableRowId",
                        column: x => x.TicketTemplateTableRowId,
                        principalTable: "TicketTemplateTableRows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTableCellProperties_TicketTemplateTableCellId",
                table: "TicketTemplateTableCellProperties",
                column: "TicketTemplateTableCellId",
                unique: true,
                filter: "[TicketTemplateTableCellId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTableGridColumn_TicketTemplateTableId",
                table: "TicketTemplateTableGridColumn",
                column: "TicketTemplateTableId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTableProperties_TicketTemplateTableId",
                table: "TicketTemplateTableProperties",
                column: "TicketTemplateTableId",
                unique: true,
                filter: "[TicketTemplateTableId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTableRowProperties_TicketTemplateTableRowId",
                table: "TicketTemplateTableRowProperties",
                column: "TicketTemplateTableRowId",
                unique: true,
                filter: "[TicketTemplateTableRowId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketTemplateTableCellProperties");

            migrationBuilder.DropTable(
                name: "TicketTemplateTableGridColumn");

            migrationBuilder.DropTable(
                name: "TicketTemplateTableProperties");

            migrationBuilder.DropTable(
                name: "TicketTemplateTableRowProperties");

            migrationBuilder.DropColumn(
                name: "Break",
                table: "TicketTemplateParagraphRuns");

            migrationBuilder.RenameColumn(
                name: "TicketTemplateTablePropertiesId",
                table: "TicketTemplateTables",
                newName: "PropertiesId");

            migrationBuilder.RenameColumn(
                name: "TicketTemplateTableRowPropertiesId",
                table: "TicketTemplateTableRows",
                newName: "PropertiesId");

            migrationBuilder.RenameColumn(
                name: "TicketTemplateTableCellPropertiesId",
                table: "TicketTemplateTableCells",
                newName: "PropertiesId");

            migrationBuilder.AddColumn<Guid>(
                name: "ColumnsId",
                table: "TicketTemplateTables",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "TicketTemplateTables",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDelete",
                table: "TicketTemplateTables",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TicketTemplateTables",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TicketTemplateTables",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "TicketTemplateTableRows",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDelete",
                table: "TicketTemplateTableRows",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TicketTemplateTableRows",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TicketTemplateTableRows",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "TicketTemplateTableCells",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDelete",
                table: "TicketTemplateTableCells",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TicketTemplateTableCells",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TicketTemplateTableCells",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TicketTemplateElementaryUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    ParentElementaryUnitId = table.Column<Guid>(nullable: true),
                    TicketTemplateParagraphDataId = table.Column<Guid>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateElementaryUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateElementaryUnits_TicketTemplateElementaryUnits_ParentElementaryUnitId",
                        column: x => x.ParentElementaryUnitId,
                        principalTable: "TicketTemplateElementaryUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketTemplateElementaryAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TicketTemplateElementaryUnitId = table.Column<Guid>(nullable: true),
                    TicketTemplateParagraphDataId = table.Column<Guid>(nullable: true),
                    TicketTemplateParagraphId = table.Column<Guid>(nullable: true),
                    TicketTemplateTableRowId = table.Column<Guid>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateElementaryAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateElementaryAttributes_TicketTemplateElementaryUnits_TicketTemplateElementaryUnitId",
                        column: x => x.TicketTemplateElementaryUnitId,
                        principalTable: "TicketTemplateElementaryUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketTemplateElementaryAttributes_TicketTemplateParagraphs_TicketTemplateParagraphId",
                        column: x => x.TicketTemplateParagraphId,
                        principalTable: "TicketTemplateParagraphs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketTemplateElementaryAttributes_TicketTemplateTableRows_TicketTemplateTableRowId",
                        column: x => x.TicketTemplateTableRowId,
                        principalTable: "TicketTemplateTableRows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTables_ColumnsId",
                table: "TicketTemplateTables",
                column: "ColumnsId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTables_PropertiesId",
                table: "TicketTemplateTables",
                column: "PropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTableRows_PropertiesId",
                table: "TicketTemplateTableRows",
                column: "PropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTableCells_PropertiesId",
                table: "TicketTemplateTableCells",
                column: "PropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateElementaryAttributes_TicketTemplateElementaryUnitId",
                table: "TicketTemplateElementaryAttributes",
                column: "TicketTemplateElementaryUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateElementaryAttributes_TicketTemplateParagraphId",
                table: "TicketTemplateElementaryAttributes",
                column: "TicketTemplateParagraphId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateElementaryAttributes_TicketTemplateTableRowId",
                table: "TicketTemplateElementaryAttributes",
                column: "TicketTemplateTableRowId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateElementaryUnits_ParentElementaryUnitId",
                table: "TicketTemplateElementaryUnits",
                column: "ParentElementaryUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateTableCells_TicketTemplateElementaryUnits_PropertiesId",
                table: "TicketTemplateTableCells",
                column: "PropertiesId",
                principalTable: "TicketTemplateElementaryUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateTableRows_TicketTemplateElementaryUnits_PropertiesId",
                table: "TicketTemplateTableRows",
                column: "PropertiesId",
                principalTable: "TicketTemplateElementaryUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateTables_TicketTemplateElementaryUnits_ColumnsId",
                table: "TicketTemplateTables",
                column: "ColumnsId",
                principalTable: "TicketTemplateElementaryUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateTables_TicketTemplateElementaryUnits_PropertiesId",
                table: "TicketTemplateTables",
                column: "PropertiesId",
                principalTable: "TicketTemplateElementaryUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
