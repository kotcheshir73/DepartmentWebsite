namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStudentGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentGroups", "Course", c => c.Int(nullable: false));
            DropColumn("dbo.StudentGroups", "Kurs");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentGroups", "Kurs", c => c.Int(nullable: false));
            DropColumn("dbo.StudentGroups", "Course");
        }
    }
}
