namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConnectAcademicYearWithSeasonDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeasonDates", "AcademicYearId", c => c.Guid(nullable: false));
            CreateIndex("dbo.SeasonDates", "AcademicYearId");
            AddForeignKey("dbo.SeasonDates", "AcademicYearId", "dbo.AcademicYears", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeasonDates", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.SeasonDates", new[] { "AcademicYearId" });
            DropColumn("dbo.SeasonDates", "AcademicYearId");
        }
    }
}
