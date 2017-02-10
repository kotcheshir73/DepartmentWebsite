namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSemesterRecord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SemesterRecords", "LessonClassroom", c => c.String());
            AddColumn("dbo.SemesterRecords", "LecturerId", c => c.Long());
            CreateIndex("dbo.SemesterRecords", "LecturerId");
            AddForeignKey("dbo.SemesterRecords", "LecturerId", "dbo.Lecturers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SemesterRecords", "LecturerId", "dbo.Lecturers");
            DropIndex("dbo.SemesterRecords", new[] { "LecturerId" });
            DropColumn("dbo.SemesterRecords", "LecturerId");
            DropColumn("dbo.SemesterRecords", "LessonClassroom");
        }
    }
}
