namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisiplineLessonAddSemester : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DisciplineLessons", "Semester", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DisciplineLessons", "Semester");
        }
    }
}
