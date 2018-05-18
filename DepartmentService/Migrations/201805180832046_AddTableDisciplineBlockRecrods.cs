namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableDisciplineBlockRecrods : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DisciplineBlockRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineBlockId = c.Guid(nullable: false),
                        EducationDirectionId = c.Guid(),
                        AcademicYearId = c.Guid(nullable: false),
                        TimeNormId = c.Guid(nullable: false),
                        DisciplineBlockRecordTitle = c.String(nullable: false),
                        DisciplineBlockRecordHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .ForeignKey("dbo.DisciplineBlocks", t => t.DisciplineBlockId, cascadeDelete: true)
                .ForeignKey("dbo.EducationDirections", t => t.EducationDirectionId)
                .ForeignKey("dbo.TimeNorms", t => t.TimeNormId, cascadeDelete: false)
                .Index(t => t.DisciplineBlockId)
                .Index(t => t.EducationDirectionId)
                .Index(t => t.AcademicYearId)
                .Index(t => t.TimeNormId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DisciplineBlockRecords", "TimeNormId", "dbo.TimeNorms");
            DropForeignKey("dbo.DisciplineBlockRecords", "EducationDirectionId", "dbo.EducationDirections");
            DropForeignKey("dbo.DisciplineBlockRecords", "DisciplineBlockId", "dbo.DisciplineBlocks");
            DropForeignKey("dbo.DisciplineBlockRecords", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.DisciplineBlockRecords", new[] { "TimeNormId" });
            DropIndex("dbo.DisciplineBlockRecords", new[] { "AcademicYearId" });
            DropIndex("dbo.DisciplineBlockRecords", new[] { "EducationDirectionId" });
            DropIndex("dbo.DisciplineBlockRecords", new[] { "DisciplineBlockId" });
            DropTable("dbo.DisciplineBlockRecords");
        }
    }
}
