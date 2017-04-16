namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFiledLoadInLoadDistributionRecord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoadDistributionRecords", "Load", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoadDistributionRecords", "Load");
        }
    }
}
