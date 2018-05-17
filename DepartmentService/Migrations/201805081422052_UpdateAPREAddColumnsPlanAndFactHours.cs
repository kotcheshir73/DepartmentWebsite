namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAPREAddColumnsPlanAndFactHours : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AcademicPlanRecordElements", "PlanHours", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.AcademicPlanRecordElements", "FactHours", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.AcademicPlanRecordElements", "Hours");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AcademicPlanRecordElements", "Hours", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.AcademicPlanRecordElements", "FactHours");
            DropColumn("dbo.AcademicPlanRecordElements", "PlanHours");
        }
    }
}
