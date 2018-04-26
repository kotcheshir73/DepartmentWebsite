namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedDLSR : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DisciplineLessonStudentRecords", "Comment", c => c.String());
            AddColumn("dbo.DisciplineLessonTaskStudentRecords", "Score", c => c.Int(nullable: false));
            DropColumn("dbo.DisciplineLessonTasks", "DisciplineLessonName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DisciplineLessonTasks", "DisciplineLessonName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.DisciplineLessonTaskStudentRecords", "Score");
            DropColumn("dbo.DisciplineLessonStudentRecords", "Comment");
        }
    }
}
