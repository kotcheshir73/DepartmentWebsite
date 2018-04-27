namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeContingentLink : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LoadDistributionRecords", "ContingentId", "dbo.Contingents");
            DropIndex("dbo.LoadDistributionRecords", new[] { "ContingentId" });
            AddColumn("dbo.AcademicPlanRecords", "ContingentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.AcademicPlanRecords", "ContingentId");
            AddForeignKey("dbo.AcademicPlanRecords", "ContingentId", "dbo.Contingents", "Id", cascadeDelete: false);
            DropColumn("dbo.LoadDistributionRecords", "ContingentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoadDistributionRecords", "ContingentId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.AcademicPlanRecords", "ContingentId", "dbo.Contingents");
            DropIndex("dbo.AcademicPlanRecords", new[] { "ContingentId" });
            DropColumn("dbo.AcademicPlanRecords", "ContingentId");
            CreateIndex("dbo.LoadDistributionRecords", "ContingentId");
            AddForeignKey("dbo.LoadDistributionRecords", "ContingentId", "dbo.Contingents", "Id", cascadeDelete: true);
        }
    }
}
