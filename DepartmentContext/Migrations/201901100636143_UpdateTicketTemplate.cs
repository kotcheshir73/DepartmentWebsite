namespace DepartmentContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTicketTemplate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExaminationTemplateBlockQuestions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ExaminationTemplateBlockId = c.Guid(nullable: false),
                        QuestionNumber = c.Int(nullable: false),
                        QuestionText = c.String(),
                        QuestionImage = c.Binary(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExaminationTemplateBlocks", t => t.ExaminationTemplateBlockId, cascadeDelete: true)
                .Index(t => t.ExaminationTemplateBlockId);
            
            CreateTable(
                "dbo.ExaminationTemplateBlocks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ExaminationTemplateId = c.Guid(nullable: false),
                        BlockName = c.String(),
                        CountQuestionInTicket = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExaminationTemplates", t => t.ExaminationTemplateId, cascadeDelete: true)
                .Index(t => t.ExaminationTemplateId);
            
            CreateTable(
                "dbo.ExaminationTemplates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineId = c.Guid(nullable: false),
                        EducationDirectionId = c.Guid(),
                        Semester = c.Int(),
                        TicketTemplateId = c.Guid(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId, cascadeDelete: true)
                .ForeignKey("dbo.EducationDirections", t => t.EducationDirectionId)
                .ForeignKey("dbo.TicketTemplates", t => t.TicketTemplateId)
                .Index(t => t.DisciplineId)
                .Index(t => t.EducationDirectionId)
                .Index(t => t.TicketTemplateId);
            
            CreateTable(
                "dbo.ExaminationTemplateTickets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ExaminationTemplateId = c.Guid(nullable: false),
                        TicketNumber = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExaminationTemplates", t => t.ExaminationTemplateId, cascadeDelete: true)
                .Index(t => t.ExaminationTemplateId);
            
            CreateTable(
                "dbo.ExaminationTemplateTicketQuestions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ExaminationTemplateTicketId = c.Guid(nullable: false),
                        ExaminationTemplateBlockQuestionId = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExaminationTemplateBlockQuestions", t => t.ExaminationTemplateBlockQuestionId, cascadeDelete: false)
                .ForeignKey("dbo.ExaminationTemplateTickets", t => t.ExaminationTemplateTicketId, cascadeDelete: true)
                .Index(t => t.ExaminationTemplateTicketId)
                .Index(t => t.ExaminationTemplateBlockQuestionId);
            
            CreateTable(
                "dbo.TicketTemplates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TicketTemplateBodyId = c.Guid(nullable: false),
                        XML = c.String(),
                        TemplateName = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketTemplateBodies", t => t.TicketTemplateBodyId, cascadeDelete: true)
                .Index(t => t.TicketTemplateBodyId);
            
            CreateTable(
                "dbo.TicketTemplateBodies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BodyFormatId = c.Guid(),
                        BodyName = c.String(),
                        SectName = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketTemplateElementaryUnits", t => t.BodyFormatId)
                .Index(t => t.BodyFormatId);
            
            CreateTable(
                "dbo.TicketTemplateElementaryUnits",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TicketTemplateParagraphDataId = c.Guid(),
                        ParentElementaryUnitId = c.Guid(),
                        Name = c.String(),
                        Value = c.String(),
                        Order = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketTemplateElementaryUnits", t => t.ParentElementaryUnitId)
                .ForeignKey("dbo.TicketTemplateParagraphDatas", t => t.TicketTemplateParagraphDataId)
                .Index(t => t.TicketTemplateParagraphDataId)
                .Index(t => t.ParentElementaryUnitId);
            
            CreateTable(
                "dbo.TicketTemplateElementaryAttributes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TicketTemplateParagraphDataId = c.Guid(),
                        TicketTemplateParagraphId = c.Guid(),
                        TicketTemplateTableRowId = c.Guid(),
                        TicketTemplateElementaryUnitId = c.Guid(),
                        Name = c.String(),
                        Value = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketTemplateElementaryUnits", t => t.TicketTemplateElementaryUnitId)
                .ForeignKey("dbo.TicketTemplateParagraphs", t => t.TicketTemplateParagraphId)
                .ForeignKey("dbo.TicketTemplateParagraphDatas", t => t.TicketTemplateParagraphDataId)
                .ForeignKey("dbo.TicketTemplateTableRows", t => t.TicketTemplateTableRowId)
                .Index(t => t.TicketTemplateParagraphDataId)
                .Index(t => t.TicketTemplateParagraphId)
                .Index(t => t.TicketTemplateTableRowId)
                .Index(t => t.TicketTemplateElementaryUnitId);
            
            CreateTable(
                "dbo.TicketTemplateParagraphs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TicketTemplateBodyId = c.Guid(),
                        TicketTemplateTableCellId = c.Guid(),
                        ParagraphFormatId = c.Guid(),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketTemplateElementaryUnits", t => t.ParagraphFormatId)
                .ForeignKey("dbo.TicketTemplateBodies", t => t.TicketTemplateBodyId)
                .ForeignKey("dbo.TicketTemplateTableCells", t => t.TicketTemplateTableCellId)
                .Index(t => t.TicketTemplateBodyId)
                .Index(t => t.TicketTemplateTableCellId)
                .Index(t => t.ParagraphFormatId);
            
            CreateTable(
                "dbo.TicketTemplateParagraphDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TicketTemplateParagraphId = c.Guid(),
                        FontId = c.Guid(),
                        Name = c.String(),
                        TextName = c.String(),
                        Text = c.String(),
                        Order = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketTemplateElementaryUnits", t => t.FontId)
                .ForeignKey("dbo.TicketTemplateParagraphs", t => t.TicketTemplateParagraphId)
                .Index(t => t.TicketTemplateParagraphId)
                .Index(t => t.FontId);
            
            CreateTable(
                "dbo.TicketTemplateTableCells",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TicketTemplateTableRowId = c.Guid(),
                        PropertiesId = c.Guid(),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketTemplateElementaryUnits", t => t.PropertiesId)
                .ForeignKey("dbo.TicketTemplateTableRows", t => t.TicketTemplateTableRowId)
                .Index(t => t.TicketTemplateTableRowId)
                .Index(t => t.PropertiesId);
            
            CreateTable(
                "dbo.TicketTemplateTableRows",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TicketTemplateTableId = c.Guid(),
                        PropertiesId = c.Guid(),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketTemplateElementaryUnits", t => t.PropertiesId)
                .ForeignKey("dbo.TicketTemplateTables", t => t.TicketTemplateTableId)
                .Index(t => t.TicketTemplateTableId)
                .Index(t => t.PropertiesId);
            
            CreateTable(
                "dbo.TicketTemplateTables",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TicketTemplateBodyId = c.Guid(),
                        PropertiesId = c.Guid(),
                        ColumnsId = c.Guid(),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketTemplateElementaryUnits", t => t.ColumnsId)
                .ForeignKey("dbo.TicketTemplateElementaryUnits", t => t.PropertiesId)
                .ForeignKey("dbo.TicketTemplateBodies", t => t.TicketTemplateBodyId)
                .Index(t => t.TicketTemplateBodyId)
                .Index(t => t.PropertiesId)
                .Index(t => t.ColumnsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExaminationTemplateBlockQuestions", "ExaminationTemplateBlockId", "dbo.ExaminationTemplateBlocks");
            DropForeignKey("dbo.ExaminationTemplates", "TicketTemplateId", "dbo.TicketTemplates");
            DropForeignKey("dbo.TicketTemplates", "TicketTemplateBodyId", "dbo.TicketTemplateBodies");
            DropForeignKey("dbo.TicketTemplateBodies", "BodyFormatId", "dbo.TicketTemplateElementaryUnits");
            DropForeignKey("dbo.TicketTemplateTableCells", "TicketTemplateTableRowId", "dbo.TicketTemplateTableRows");
            DropForeignKey("dbo.TicketTemplateTableRows", "TicketTemplateTableId", "dbo.TicketTemplateTables");
            DropForeignKey("dbo.TicketTemplateTables", "TicketTemplateBodyId", "dbo.TicketTemplateBodies");
            DropForeignKey("dbo.TicketTemplateTables", "PropertiesId", "dbo.TicketTemplateElementaryUnits");
            DropForeignKey("dbo.TicketTemplateTables", "ColumnsId", "dbo.TicketTemplateElementaryUnits");
            DropForeignKey("dbo.TicketTemplateElementaryAttributes", "TicketTemplateTableRowId", "dbo.TicketTemplateTableRows");
            DropForeignKey("dbo.TicketTemplateTableRows", "PropertiesId", "dbo.TicketTemplateElementaryUnits");
            DropForeignKey("dbo.TicketTemplateParagraphs", "TicketTemplateTableCellId", "dbo.TicketTemplateTableCells");
            DropForeignKey("dbo.TicketTemplateTableCells", "PropertiesId", "dbo.TicketTemplateElementaryUnits");
            DropForeignKey("dbo.TicketTemplateParagraphDatas", "TicketTemplateParagraphId", "dbo.TicketTemplateParagraphs");
            DropForeignKey("dbo.TicketTemplateElementaryUnits", "TicketTemplateParagraphDataId", "dbo.TicketTemplateParagraphDatas");
            DropForeignKey("dbo.TicketTemplateElementaryAttributes", "TicketTemplateParagraphDataId", "dbo.TicketTemplateParagraphDatas");
            DropForeignKey("dbo.TicketTemplateParagraphDatas", "FontId", "dbo.TicketTemplateElementaryUnits");
            DropForeignKey("dbo.TicketTemplateElementaryAttributes", "TicketTemplateParagraphId", "dbo.TicketTemplateParagraphs");
            DropForeignKey("dbo.TicketTemplateParagraphs", "TicketTemplateBodyId", "dbo.TicketTemplateBodies");
            DropForeignKey("dbo.TicketTemplateParagraphs", "ParagraphFormatId", "dbo.TicketTemplateElementaryUnits");
            DropForeignKey("dbo.TicketTemplateElementaryAttributes", "TicketTemplateElementaryUnitId", "dbo.TicketTemplateElementaryUnits");
            DropForeignKey("dbo.TicketTemplateElementaryUnits", "ParentElementaryUnitId", "dbo.TicketTemplateElementaryUnits");
            DropForeignKey("dbo.ExaminationTemplateTicketQuestions", "ExaminationTemplateTicketId", "dbo.ExaminationTemplateTickets");
            DropForeignKey("dbo.ExaminationTemplateTicketQuestions", "ExaminationTemplateBlockQuestionId", "dbo.ExaminationTemplateBlockQuestions");
            DropForeignKey("dbo.ExaminationTemplateTickets", "ExaminationTemplateId", "dbo.ExaminationTemplates");
            DropForeignKey("dbo.ExaminationTemplateBlocks", "ExaminationTemplateId", "dbo.ExaminationTemplates");
            DropForeignKey("dbo.ExaminationTemplates", "EducationDirectionId", "dbo.EducationDirections");
            DropForeignKey("dbo.ExaminationTemplates", "DisciplineId", "dbo.Disciplines");
            DropIndex("dbo.TicketTemplateTables", new[] { "ColumnsId" });
            DropIndex("dbo.TicketTemplateTables", new[] { "PropertiesId" });
            DropIndex("dbo.TicketTemplateTables", new[] { "TicketTemplateBodyId" });
            DropIndex("dbo.TicketTemplateTableRows", new[] { "PropertiesId" });
            DropIndex("dbo.TicketTemplateTableRows", new[] { "TicketTemplateTableId" });
            DropIndex("dbo.TicketTemplateTableCells", new[] { "PropertiesId" });
            DropIndex("dbo.TicketTemplateTableCells", new[] { "TicketTemplateTableRowId" });
            DropIndex("dbo.TicketTemplateParagraphDatas", new[] { "FontId" });
            DropIndex("dbo.TicketTemplateParagraphDatas", new[] { "TicketTemplateParagraphId" });
            DropIndex("dbo.TicketTemplateParagraphs", new[] { "ParagraphFormatId" });
            DropIndex("dbo.TicketTemplateParagraphs", new[] { "TicketTemplateTableCellId" });
            DropIndex("dbo.TicketTemplateParagraphs", new[] { "TicketTemplateBodyId" });
            DropIndex("dbo.TicketTemplateElementaryAttributes", new[] { "TicketTemplateElementaryUnitId" });
            DropIndex("dbo.TicketTemplateElementaryAttributes", new[] { "TicketTemplateTableRowId" });
            DropIndex("dbo.TicketTemplateElementaryAttributes", new[] { "TicketTemplateParagraphId" });
            DropIndex("dbo.TicketTemplateElementaryAttributes", new[] { "TicketTemplateParagraphDataId" });
            DropIndex("dbo.TicketTemplateElementaryUnits", new[] { "ParentElementaryUnitId" });
            DropIndex("dbo.TicketTemplateElementaryUnits", new[] { "TicketTemplateParagraphDataId" });
            DropIndex("dbo.TicketTemplateBodies", new[] { "BodyFormatId" });
            DropIndex("dbo.TicketTemplates", new[] { "TicketTemplateBodyId" });
            DropIndex("dbo.ExaminationTemplateTicketQuestions", new[] { "ExaminationTemplateBlockQuestionId" });
            DropIndex("dbo.ExaminationTemplateTicketQuestions", new[] { "ExaminationTemplateTicketId" });
            DropIndex("dbo.ExaminationTemplateTickets", new[] { "ExaminationTemplateId" });
            DropIndex("dbo.ExaminationTemplates", new[] { "TicketTemplateId" });
            DropIndex("dbo.ExaminationTemplates", new[] { "EducationDirectionId" });
            DropIndex("dbo.ExaminationTemplates", new[] { "DisciplineId" });
            DropIndex("dbo.ExaminationTemplateBlocks", new[] { "ExaminationTemplateId" });
            DropIndex("dbo.ExaminationTemplateBlockQuestions", new[] { "ExaminationTemplateBlockId" });
            DropTable("dbo.TicketTemplateTables");
            DropTable("dbo.TicketTemplateTableRows");
            DropTable("dbo.TicketTemplateTableCells");
            DropTable("dbo.TicketTemplateParagraphDatas");
            DropTable("dbo.TicketTemplateParagraphs");
            DropTable("dbo.TicketTemplateElementaryAttributes");
            DropTable("dbo.TicketTemplateElementaryUnits");
            DropTable("dbo.TicketTemplateBodies");
            DropTable("dbo.TicketTemplates");
            DropTable("dbo.ExaminationTemplateTicketQuestions");
            DropTable("dbo.ExaminationTemplateTickets");
            DropTable("dbo.ExaminationTemplates");
            DropTable("dbo.ExaminationTemplateBlocks");
            DropTable("dbo.ExaminationTemplateBlockQuestions");
        }
    }
}
