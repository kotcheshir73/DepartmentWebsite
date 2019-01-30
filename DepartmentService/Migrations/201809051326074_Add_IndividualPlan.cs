namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_IndividualPlan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IndividualPlanRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IndividualPlanKindOfWorkId = c.Guid(nullable: false),
                        LecturerId = c.Guid(nullable: false),
                        PlanAutumn = c.Double(nullable: false),
                        FactAutumn = c.Double(nullable: false),
                        PlanSpring = c.Double(nullable: false),
                        FactSpring = c.Double(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IndividualPlanKindOfWorks", t => t.IndividualPlanKindOfWorkId, cascadeDelete: true)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId, cascadeDelete: true)
                .Index(t => t.IndividualPlanKindOfWorkId)
                .Index(t => t.LecturerId);
            
            CreateTable(
                "dbo.IndividualPlanKindOfWorks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IndividualPlanTitleId = c.Guid(nullable: false),
                        Name = c.String(),
                        TimeNormDescription = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IndividualPlanTitles", t => t.IndividualPlanTitleId, cascadeDelete: true)
                .Index(t => t.IndividualPlanTitleId);
            
            CreateTable(
                "dbo.IndividualPlanTitles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IndividualPlanRecords", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.IndividualPlanKindOfWorks", "IndividualPlanTitleId", "dbo.IndividualPlanTitles");
            DropForeignKey("dbo.IndividualPlanRecords", "IndividualPlanKindOfWorkId", "dbo.IndividualPlanKindOfWorks");
            DropIndex("dbo.IndividualPlanKindOfWorks", new[] { "IndividualPlanTitleId" });
            DropIndex("dbo.IndividualPlanRecords", new[] { "LecturerId" });
            DropIndex("dbo.IndividualPlanRecords", new[] { "IndividualPlanKindOfWorkId" });
            DropTable("dbo.IndividualPlanTitles");
            DropTable("dbo.IndividualPlanKindOfWorks");
            DropTable("dbo.IndividualPlanRecords");
        }
    }
}
