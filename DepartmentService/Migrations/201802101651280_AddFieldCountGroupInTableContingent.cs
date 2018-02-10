namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldCountGroupInTableContingent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contingents", "CountGroups", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contingents", "CountGroups");
        }
    }
}
