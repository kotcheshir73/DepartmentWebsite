namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConsultationClassroomIntoExaminationRecord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExaminationRecords", "LessonConsultationClassroom", c => c.String());
            AddColumn("dbo.ExaminationRecords", "ConsultationClassroomId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ExaminationRecords", "ConsultationClassroomId");
            AddForeignKey("dbo.ExaminationRecords", "ConsultationClassroomId", "dbo.Classrooms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExaminationRecords", "ConsultationClassroomId", "dbo.Classrooms");
            DropIndex("dbo.ExaminationRecords", new[] { "ConsultationClassroomId" });
            DropColumn("dbo.ExaminationRecords", "ConsultationClassroomId");
            DropColumn("dbo.ExaminationRecords", "LessonConsultationClassroom");
        }
    }
}
