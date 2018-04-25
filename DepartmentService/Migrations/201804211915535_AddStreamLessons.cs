namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStreamLessons : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StreamLessons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AcademicYearId = c.Guid(nullable: false),
                        StreamLessonName = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .Index(t => t.AcademicYearId);
            
            CreateTable(
                "dbo.StreamLessonRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StreamLessonId = c.Guid(nullable: false),
                        AcademicPlanRecordElementId = c.Guid(nullable: false),
                        Hours = c.Int(nullable: false),
                        IsMain = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicPlanRecordElements", t => t.AcademicPlanRecordElementId, cascadeDelete: false)
                .ForeignKey("dbo.StreamLessons", t => t.StreamLessonId, cascadeDelete: true)
                .Index(t => t.StreamLessonId)
                .Index(t => t.AcademicPlanRecordElementId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StreamLessonRecords", "StreamLessonId", "dbo.StreamLessons");
            DropForeignKey("dbo.StreamLessonRecords", "AcademicPlanRecordElementId", "dbo.AcademicPlanRecordElements");
            DropForeignKey("dbo.StreamLessons", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.StreamLessonRecords", new[] { "AcademicPlanRecordElementId" });
            DropIndex("dbo.StreamLessonRecords", new[] { "StreamLessonId" });
            DropIndex("dbo.StreamLessons", new[] { "AcademicYearId" });
            DropTable("dbo.StreamLessonRecords");
            DropTable("dbo.StreamLessons");
        }
    }
}
