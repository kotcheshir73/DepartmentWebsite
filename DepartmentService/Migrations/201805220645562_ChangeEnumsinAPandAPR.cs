namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEnumsinAPandAPR : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AcademicPlanRecords", "Semester", c => c.Int());
            AlterColumn("dbo.AcademicPlans", "AcademicCourses", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AcademicPlans", "AcademicCourses", c => c.Int(nullable: false));
            AlterColumn("dbo.AcademicPlanRecords", "Semester", c => c.Int(nullable: false));
        }
    }
}
