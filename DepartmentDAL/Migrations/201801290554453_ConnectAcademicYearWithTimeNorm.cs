namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConnectAcademicYearWithTimeNorm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeNorms", "AcademicYearId", c => c.Guid(nullable: false));
            CreateIndex("dbo.TimeNorms", "AcademicYearId");
            AddForeignKey("dbo.TimeNorms", "AcademicYearId", "dbo.AcademicYears", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeNorms", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.TimeNorms", new[] { "AcademicYearId" });
            DropColumn("dbo.TimeNorms", "AcademicYearId");
        }
    }
}
