namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSemesterRecord1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SemesterRecords", "LessonLecturer", c => c.String());
            AddColumn("dbo.SemesterRecords", "LessonGroup", c => c.String());
            DropColumn("dbo.SemesterRecords", "LessonTeacher");
            DropColumn("dbo.SemesterRecords", "LessonGroupName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SemesterRecords", "LessonGroupName", c => c.String());
            AddColumn("dbo.SemesterRecords", "LessonTeacher", c => c.String());
            DropColumn("dbo.SemesterRecords", "LessonGroup");
            DropColumn("dbo.SemesterRecords", "LessonLecturer");
        }
    }
}
