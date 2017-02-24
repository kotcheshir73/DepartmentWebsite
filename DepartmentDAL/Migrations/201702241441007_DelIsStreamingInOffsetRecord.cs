namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelIsStreamingInOffsetRecord : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OffsetRecords", "IsStreaming");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OffsetRecords", "IsStreaming", c => c.Boolean(nullable: false));
        }
    }
}
