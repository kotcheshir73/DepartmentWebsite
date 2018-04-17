namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldSoftwareDescriptionInTableSoftwareRecord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SoftwareRecords", "SoftwareDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SoftwareRecords", "SoftwareDescription");
        }
    }
}
