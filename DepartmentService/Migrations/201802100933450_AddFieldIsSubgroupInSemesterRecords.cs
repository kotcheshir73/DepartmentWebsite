namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldIsSubgroupInSemesterRecords : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SemesterRecords", "IsSubgroup", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SemesterRecords", "IsSubgroup");
        }
    }
}
