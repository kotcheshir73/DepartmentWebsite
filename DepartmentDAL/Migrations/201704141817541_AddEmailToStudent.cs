namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailToStudent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Email", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Email");
        }
    }
}
