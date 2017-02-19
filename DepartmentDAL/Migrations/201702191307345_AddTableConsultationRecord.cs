namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableConsultationRecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConsultationRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SeasonDatesId = c.Long(nullable: false),
                        DateConsultation = c.DateTime(nullable: false),
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
            DropForeignKey("dbo.ConsultationRecords", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.ConsultationRecords", "SeasonDatesId", "dbo.SeasonDates");
            DropForeignKey("dbo.ConsultationRecords", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.ConsultationRecords", "ClassroomId", "dbo.Classrooms");
            DropIndex("dbo.ConsultationRecords", new[] { "LecturerId" });
            DropIndex("dbo.ConsultationRecords", new[] { "StudentGroupId" });
            DropIndex("dbo.ConsultationRecords", new[] { "ClassroomId" });
            DropIndex("dbo.ConsultationRecords", new[] { "SeasonDatesId" });
            DropTable("dbo.ConsultationRecords");
        }
    }
}
