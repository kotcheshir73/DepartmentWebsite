namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeBall : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DisciplineLessonConductedStudents", "Ball", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DisciplineLessonConductedStudents", "Ball", c => c.Int());
        }
    }
}
