namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLoadDistribution : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoadDistributions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AcademicYearId = c.Long(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .Index(t => t.AcademicYearId);
            
            CreateTable(
                "dbo.LoadDistributionRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LoadDistributionId = c.Long(nullable: false),
                        AcademicPlanRecordId = c.Long(nullable: false),
                        ContingentId = c.Long(nullable: false),
                        TimeNormId = c.Long(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicPlanRecords", t => t.AcademicPlanRecordId, cascadeDelete: true)
                .ForeignKey("dbo.Contingents", t => t.ContingentId, cascadeDelete: true)
                .ForeignKey("dbo.LoadDistributions", t => t.LoadDistributionId, cascadeDelete: true)
                .ForeignKey("dbo.TimeNorms", t => t.TimeNormId, cascadeDelete: true)
                .Index(t => t.LoadDistributionId)
                .Index(t => t.AcademicPlanRecordId)
                .Index(t => t.ContingentId)
                .Index(t => t.TimeNormId);
            
            CreateTable(
                "dbo.LoadDistributionMissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LoadDistributionRecordId = c.Long(nullable: false),
                        LecturerId = c.Long(nullable: false),
                        Hours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId, cascadeDelete: true)
                .ForeignKey("dbo.LoadDistributionRecords", t => t.LoadDistributionRecordId, cascadeDelete: true)
                .Index(t => t.LoadDistributionRecordId)
                .Index(t => t.LecturerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoadDistributionRecords", "TimeNormId", "dbo.TimeNorms");
            DropForeignKey("dbo.LoadDistributionRecords", "LoadDistributionId", "dbo.LoadDistributions");
            DropForeignKey("dbo.LoadDistributionMissions", "LoadDistributionRecordId", "dbo.LoadDistributionRecords");
            DropForeignKey("dbo.LoadDistributionMissions", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.LoadDistributionRecords", "ContingentId", "dbo.Contingents");
            DropForeignKey("dbo.LoadDistributionRecords", "AcademicPlanRecordId", "dbo.AcademicPlanRecords");
            DropForeignKey("dbo.LoadDistributions", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.LoadDistributionMissions", new[] { "LecturerId" });
            DropIndex("dbo.LoadDistributionMissions", new[] { "LoadDistributionRecordId" });
            DropIndex("dbo.LoadDistributionRecords", new[] { "TimeNormId" });
            DropIndex("dbo.LoadDistributionRecords", new[] { "ContingentId" });
            DropIndex("dbo.LoadDistributionRecords", new[] { "AcademicPlanRecordId" });
            DropIndex("dbo.LoadDistributionRecords", new[] { "LoadDistributionId" });
            DropIndex("dbo.LoadDistributions", new[] { "AcademicYearId" });
            DropTable("dbo.LoadDistributionMissions");
            DropTable("dbo.LoadDistributionRecords");
            DropTable("dbo.LoadDistributions");
        }
    }
}
