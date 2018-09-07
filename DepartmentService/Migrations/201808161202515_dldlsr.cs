namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dldlsr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DisciplineLessons", "Date", c => c.DateTime());
            AddColumn("dbo.DisciplineLessonTaskStudentRecords", "Date", c => c.DateTime());
            AlterColumn("dbo.DisciplineLessonTasks", "MaxBall", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DisciplineLessonTasks", "MaxBall", c => c.Int(nullable: false));
            DropColumn("dbo.DisciplineLessonTaskStudentRecords", "Date");
            DropColumn("dbo.DisciplineLessons", "Date");
        }
    }
}
