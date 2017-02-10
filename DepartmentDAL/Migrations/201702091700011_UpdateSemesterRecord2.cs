namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSemesterRecord2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SemesterRecords", "IsStreaming", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SemesterRecords", "IsStreaming");
        }
    }
}
