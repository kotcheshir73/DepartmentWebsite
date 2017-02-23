namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableExaminationRecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExaminationRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateConsultation = c.DateTime(nullable: false),
                        DateExamination = c.DateTime(nullable: false),
                        SeasonDatesId = c.Long(nullable: false),
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
            DropForeignKey("dbo.ExaminationRecords", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.ExaminationRecords", "SeasonDatesId", "dbo.SeasonDates");
            DropForeignKey("dbo.ExaminationRecords", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.ExaminationRecords", "ClassroomId", "dbo.Classrooms");
            DropIndex("dbo.ExaminationRecords", new[] { "LecturerId" });
            DropIndex("dbo.ExaminationRecords", new[] { "StudentGroupId" });
            DropIndex("dbo.ExaminationRecords", new[] { "ClassroomId" });
            DropIndex("dbo.ExaminationRecords", new[] { "SeasonDatesId" });
            DropTable("dbo.ExaminationRecords");
        }
    }
}
