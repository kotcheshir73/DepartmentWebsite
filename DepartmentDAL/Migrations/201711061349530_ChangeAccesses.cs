namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAccesses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accesses", "AccessType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accesses", "AccessType");
        }
    }
}
