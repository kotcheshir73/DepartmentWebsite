namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableOffsetRecords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OffsetRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SeasonDatesId = c.Long(nullable: false),
                        Week = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        Lesson = c.Int(nullable: false),
                        IsStreaming = c.Boolean(nullable: false),
                        LessonDiscipline = c.String(),
                        LessonLecturer = c.String(),
                        LessonGroup = c.String(),
                        LessonClassroom = c.String(),
                        ClassroomId = c.String(maxLength: 128),
                        StudentGroupId = c.Long(),
                        LecturerId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classrooms", t => t.ClassroomId)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId)
                .ForeignKey("dbo.SeasonDates", t => t.SeasonDatesId, cascadeDelete: true)
                .ForeignKey("dbo.StudentGroups", t => t.StudentGroupId)
                .Index(t => t.SeasonDatesId)
                .Index(t => t.ClassroomId)
                .Index(t => t.StudentGroupId)
                .Index(t => t.LecturerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OffsetRecords", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.OffsetRecords", "SeasonDatesId", "dbo.SeasonDates");
            DropForeignKey("dbo.OffsetRecords", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.OffsetRecords", "ClassroomId", "dbo.Classrooms");
            DropIndex("dbo.OffsetRecords", new[] { "LecturerId" });
            DropIndex("dbo.OffsetRecords", new[] { "StudentGroupId" });
            DropIndex("dbo.OffsetRecords", new[] { "ClassroomId" });
            DropIndex("dbo.OffsetRecords", new[] { "SeasonDatesId" });
            DropTable("dbo.OffsetRecords");
        }
    }
}
