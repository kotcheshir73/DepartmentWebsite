namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEDandCinAPandAPR : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AcademicPlanRecords", "ContingentId", "dbo.Contingents");
            DropForeignKey("dbo.AcademicPlans", "EducationDirectionId", "dbo.EducationDirections");
            DropIndex("dbo.AcademicPlanRecords", new[] { "ContingentId" });
            DropIndex("dbo.AcademicPlans", new[] { "EducationDirectionId" });
            AlterColumn("dbo.AcademicPlanRecords", "ContingentId", c => c.Guid());
            AlterColumn("dbo.AcademicPlans", "EducationDirectionId", c => c.Guid());
            CreateIndex("dbo.AcademicPlanRecords", "ContingentId");
            CreateIndex("dbo.AcademicPlans", "EducationDirectionId");
            AddForeignKey("dbo.AcademicPlanRecords", "ContingentId", "dbo.Contingents", "Id");
            AddForeignKey("dbo.AcademicPlans", "EducationDirectionId", "dbo.EducationDirections", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AcademicPlans", "EducationDirectionId", "dbo.EducationDirections");
            DropForeignKey("dbo.AcademicPlanRecords", "ContingentId", "dbo.Contingents");
            DropIndex("dbo.AcademicPlans", new[] { "EducationDirectionId" });
            DropIndex("dbo.AcademicPlanRecords", new[] { "ContingentId" });
            AlterColumn("dbo.AcademicPlans", "EducationDirectionId", c => c.Guid(nullable: false));
            AlterColumn("dbo.AcademicPlanRecords", "ContingentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.AcademicPlans", "EducationDirectionId");
            CreateIndex("dbo.AcademicPlanRecords", "ContingentId");
            AddForeignKey("dbo.AcademicPlans", "EducationDirectionId", "dbo.EducationDirections", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AcademicPlanRecords", "ContingentId", "dbo.Contingents", "Id", cascadeDelete: true);
        }
    }
}
