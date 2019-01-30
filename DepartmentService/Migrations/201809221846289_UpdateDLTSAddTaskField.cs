namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDLTSAddTaskField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DisciplineLessonTaskStudentAccepts", "Task", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DisciplineLessonTaskStudentAccepts", "Task");
        }
    }
}
