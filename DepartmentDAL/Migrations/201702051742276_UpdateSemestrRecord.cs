namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSemestrRecord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SemesterRecords", "LessonType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SemesterRecords", "LessonType");
        }
    }
}
