namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAcademicLevelInTimeNorm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeNorms", "TimeNormAcademicLevel", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeNorms", "TimeNormAcademicLevel");
        }
    }
}
