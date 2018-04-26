namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTableRemoveColumn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "RoleType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "RoleType", c => c.Int(nullable: false));
        }
    }
}
