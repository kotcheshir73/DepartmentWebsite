namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentStateToStudent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "StudentState", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "StudentState");
        }
    }
}
