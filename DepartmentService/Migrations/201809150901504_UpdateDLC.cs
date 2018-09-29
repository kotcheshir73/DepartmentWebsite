namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDLC : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DisciplineLessonRecords", "DisciplineLessonId", "dbo.DisciplineLessons");
            DropForeignKey("dbo.DisciplineLessonStudentRecords", "DisciplineLessonRecordId", "dbo.DisciplineLessonRecords");
            DropForeignKey("dbo.DisciplineLessonStudentRecords", "StudentId", "dbo.Students");
            DropIndex("dbo.DisciplineLessonStudentRecords", new[] { "DisciplineLessonRecordId" });
            DropIndex("dbo.DisciplineLessonStudentRecords", new[] { "StudentId" });
            DropIndex("dbo.DisciplineLessonRecords", new[] { "DisciplineLessonId" });
            CreateTable(
                "dbo.DisciplineLessonConducteds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineLessonId = c.Guid(nullable: false),
                        StudentGroupId = c.Guid(nullable: false),
                        Subgroup = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineLessons", t => t.DisciplineLessonId, cascadeDelete: true)
                .ForeignKey("dbo.StudentGroups", t => t.StudentGroupId, cascadeDelete: false)
                .Index(t => t.DisciplineLessonId)
                .Index(t => t.StudentGroupId);
            
            DropTable("dbo.DisciplineLessonStudentRecords");
            DropTable("dbo.DisciplineLessonRecords");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DisciplineLessonRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineLessonId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Subgroup = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DisciplineLessonStudentRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineLessonRecordId = c.Guid(nullable: false),
                        StudentId = c.Guid(nullable: false),
                        Comment = c.String(),
                        Status = c.Int(nullable: false),
                        Ball = c.Int(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.DisciplineLessonConducteds", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.DisciplineLessonConducteds", "DisciplineLessonId", "dbo.DisciplineLessons");
            DropIndex("dbo.DisciplineLessonConducteds", new[] { "StudentGroupId" });
            DropIndex("dbo.DisciplineLessonConducteds", new[] { "DisciplineLessonId" });
            DropTable("dbo.DisciplineLessonConducteds");
            CreateIndex("dbo.DisciplineLessonRecords", "DisciplineLessonId");
            CreateIndex("dbo.DisciplineLessonStudentRecords", "StudentId");
            CreateIndex("dbo.DisciplineLessonStudentRecords", "DisciplineLessonRecordId");
            AddForeignKey("dbo.DisciplineLessonStudentRecords", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DisciplineLessonStudentRecords", "DisciplineLessonRecordId", "dbo.DisciplineLessonRecords", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DisciplineLessonRecords", "DisciplineLessonId", "dbo.DisciplineLessons", "Id", cascadeDelete: true);
        }
    }
}
