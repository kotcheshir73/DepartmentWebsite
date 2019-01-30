namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldIsStreamingInOffset : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OffsetRecords", "IsStreaming", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OffsetRecords", "IsStreaming");
        }
    }
}
