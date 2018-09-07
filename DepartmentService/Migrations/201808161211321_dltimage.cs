namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dltimage : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DisciplineLessonTasks", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DisciplineLessonTasks", "Image", c => c.Binary());
        }
    }
}
