namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDGraficsTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GraficClassrooms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GraficId = c.Guid(nullable: false),
                        TimeNormId = c.Guid(nullable: false),
                        ClassroomDescription = c.String(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grafics", t => t.GraficId, cascadeDelete: true)
                .ForeignKey("dbo.TimeNorms", t => t.TimeNormId, cascadeDelete: true)
                .Index(t => t.GraficId)
                .Index(t => t.TimeNormId);
            
            CreateTable(
                "dbo.Grafics",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AcademicPlanRecordId = c.Guid(nullable: false),
                        StudentGroupId = c.Guid(nullable: false),
                        Comment = c.String(),
                        CommentWishesOfTeacher = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicPlanRecords", t => t.AcademicPlanRecordId, cascadeDelete: true)
                .ForeignKey("dbo.StudentGroups", t => t.StudentGroupId, cascadeDelete: true)
                .Index(t => t.AcademicPlanRecordId)
                .Index(t => t.StudentGroupId);
            
            CreateTable(
                "dbo.GraficRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GraficId = c.Guid(nullable: false),
                        TimeNormId = c.Guid(nullable: false),
                        WeekNumber = c.Int(nullable: false),
                        Hours = c.Double(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grafics", t => t.GraficId, cascadeDelete: true)
                .ForeignKey("dbo.TimeNorms", t => t.TimeNormId, cascadeDelete: true)
                .Index(t => t.GraficId)
                .Index(t => t.TimeNormId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GraficClassrooms", "TimeNormId", "dbo.TimeNorms");
            DropForeignKey("dbo.Grafics", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.GraficRecords", "TimeNormId", "dbo.TimeNorms");
            DropForeignKey("dbo.GraficRecords", "GraficId", "dbo.Grafics");
            DropForeignKey("dbo.GraficClassrooms", "GraficId", "dbo.Grafics");
            DropForeignKey("dbo.Grafics", "AcademicPlanRecordId", "dbo.AcademicPlanRecords");
            DropIndex("dbo.GraficRecords", new[] { "TimeNormId" });
            DropIndex("dbo.GraficRecords", new[] { "GraficId" });
            DropIndex("dbo.Grafics", new[] { "StudentGroupId" });
            DropIndex("dbo.Grafics", new[] { "AcademicPlanRecordId" });
            DropIndex("dbo.GraficClassrooms", new[] { "TimeNormId" });
            DropIndex("dbo.GraficClassrooms", new[] { "GraficId" });
            DropTable("dbo.GraficRecords");
            DropTable("dbo.Grafics");
            DropTable("dbo.GraficClassrooms");
        }
    }
}
