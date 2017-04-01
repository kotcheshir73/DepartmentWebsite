namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAcademyYear : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicYears",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 10),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AcademicPlans", "AcademicYearId", c => c.Long(nullable: false));
            CreateIndex("dbo.AcademicPlans", "AcademicYearId");
            AddForeignKey("dbo.AcademicPlans", "AcademicYearId", "dbo.AcademicYears", "Id", cascadeDelete: true);
            DropColumn("dbo.AcademicPlans", "AcademicYear");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AcademicPlans", "AcademicYear", c => c.String(nullable: false, maxLength: 10));
            DropForeignKey("dbo.AcademicPlans", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.AcademicPlans", new[] { "AcademicYearId" });
            DropColumn("dbo.AcademicPlans", "AcademicYearId");
            DropTable("dbo.AcademicYears");
        }
    }
}
