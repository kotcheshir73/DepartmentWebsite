namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteDBR : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DisciplineBlockRecords", "AcademicYearId", "dbo.AcademicYears");
            DropForeignKey("dbo.DisciplineBlockRecords", "DisciplineBlockId", "dbo.DisciplineBlocks");
            DropForeignKey("dbo.DisciplineBlockRecords", "EducationDirectionId", "dbo.EducationDirections");
            DropForeignKey("dbo.DisciplineBlockRecords", "TimeNormId", "dbo.TimeNorms");
            DropIndex("dbo.DisciplineBlockRecords", new[] { "DisciplineBlockId" });
            DropIndex("dbo.DisciplineBlockRecords", new[] { "EducationDirectionId" });
            DropIndex("dbo.DisciplineBlockRecords", new[] { "AcademicYearId" });
            DropIndex("dbo.DisciplineBlockRecords", new[] { "TimeNormId" });
            DropTable("dbo.DisciplineBlockRecords");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.DisciplineBlockRecords", "TimeNormId");
            CreateIndex("dbo.DisciplineBlockRecords", "AcademicYearId");
            CreateIndex("dbo.DisciplineBlockRecords", "EducationDirectionId");
            CreateIndex("dbo.DisciplineBlockRecords", "DisciplineBlockId");
            AddForeignKey("dbo.DisciplineBlockRecords", "TimeNormId", "dbo.TimeNorms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DisciplineBlockRecords", "EducationDirectionId", "dbo.EducationDirections", "Id");
            AddForeignKey("dbo.DisciplineBlockRecords", "DisciplineBlockId", "dbo.DisciplineBlocks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DisciplineBlockRecords", "AcademicYearId", "dbo.AcademicYears", "Id", cascadeDelete: true);
        }
    }
}
