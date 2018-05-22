namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldContingentNameToContingent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contingents", "ContingentName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contingents", "ContingentName");
        }
    }
}
