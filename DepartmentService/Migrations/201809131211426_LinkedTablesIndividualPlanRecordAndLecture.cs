namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkedTablesIndividualPlanRecordAndLecture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IndividualPlanRecords", "AcademicYearId", c => c.Guid(nullable: false));
            CreateIndex("dbo.IndividualPlanRecords", "AcademicYearId");
            AddForeignKey("dbo.IndividualPlanRecords", "AcademicYearId", "dbo.AcademicYears", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IndividualPlanRecords", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.IndividualPlanRecords", new[] { "AcademicYearId" });
            DropColumn("dbo.IndividualPlanRecords", "AcademicYearId");
        }
    }
}
