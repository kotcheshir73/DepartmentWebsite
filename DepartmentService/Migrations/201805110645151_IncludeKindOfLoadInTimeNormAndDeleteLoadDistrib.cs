namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncludeKindOfLoadInTimeNormAndDeleteLoadDistrib : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LoadDistributions", "AcademicYearId", "dbo.AcademicYears");
            DropForeignKey("dbo.LoadDistributionRecords", "AcademicPlanRecordId", "dbo.AcademicPlanRecords");
            DropForeignKey("dbo.LoadDistributionRecords", "LoadDistributionId", "dbo.LoadDistributions");
            DropForeignKey("dbo.LoadDistributionMissions", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.LoadDistributionMissions", "LoadDistributionRecordId", "dbo.LoadDistributionRecords");
            DropForeignKey("dbo.AcademicPlanRecordElements", "KindOfLoadId", "dbo.KindOfLoads");
            DropForeignKey("dbo.TimeNorms", "KindOfLoadId", "dbo.KindOfLoads");
            DropForeignKey("dbo.LoadDistributionRecords", "TimeNormId", "dbo.TimeNorms");
            DropIndex("dbo.AcademicPlanRecordElements", new[] { "KindOfLoadId" });
            DropIndex("dbo.LoadDistributions", new[] { "AcademicYearId" });
            DropIndex("dbo.LoadDistributionRecords", new[] { "LoadDistributionId" });
            DropIndex("dbo.LoadDistributionRecords", new[] { "AcademicPlanRecordId" });
            DropIndex("dbo.LoadDistributionRecords", new[] { "TimeNormId" });
            DropIndex("dbo.LoadDistributionMissions", new[] { "LoadDistributionRecordId" });
            DropIndex("dbo.LoadDistributionMissions", new[] { "LecturerId" });
            DropIndex("dbo.TimeNorms", new[] { "KindOfLoadId" });
            AddColumn("dbo.AcademicPlanRecordElements", "TimeNormId", c => c.Guid(nullable: false));
            AddColumn("dbo.TimeNorms", "TimeNormName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.TimeNorms", "TimeNormShortName", c => c.String(nullable: false, maxLength: 5));
            AddColumn("dbo.TimeNorms", "TimeNormOrder", c => c.Int(nullable: false));
            AddColumn("dbo.TimeNorms", "KindOfLoadName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.TimeNorms", "KindOfLoadAttributeName", c => c.String(maxLength: 10));
            AddColumn("dbo.TimeNorms", "KindOfLoadBlueAsteriskName", c => c.String(maxLength: 100));
            AddColumn("dbo.TimeNorms", "KindOfLoadBlueAsteriskAttributeName", c => c.String(maxLength: 100));
            AddColumn("dbo.TimeNorms", "KindOfLoadBlueAsteriskPracticName", c => c.String(maxLength: 100));
            CreateIndex("dbo.AcademicPlanRecordElements", "TimeNormId");
            AddForeignKey("dbo.AcademicPlanRecordElements", "TimeNormId", "dbo.TimeNorms", "Id", cascadeDelete: false);
            DropColumn("dbo.AcademicPlanRecordElements", "KindOfLoadId");
            DropColumn("dbo.TimeNorms", "KindOfLoadId");
            DropColumn("dbo.TimeNorms", "Title");
            DropTable("dbo.LoadDistributions");
            DropTable("dbo.LoadDistributionRecords");
            DropTable("dbo.LoadDistributionMissions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LoadDistributionMissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoadDistributionRecordId = c.Guid(nullable: false),
                        LecturerId = c.Guid(nullable: false),
                        Hours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoadDistributionRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoadDistributionId = c.Guid(nullable: false),
                        AcademicPlanRecordId = c.Guid(nullable: false),
                        TimeNormId = c.Guid(nullable: false),
                        Load = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoadDistributions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AcademicYearId = c.Guid(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TimeNorms", "Title", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.TimeNorms", "KindOfLoadId", c => c.Guid(nullable: false));
            AddColumn("dbo.AcademicPlanRecordElements", "KindOfLoadId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.AcademicPlanRecordElements", "TimeNormId", "dbo.TimeNorms");
            DropIndex("dbo.AcademicPlanRecordElements", new[] { "TimeNormId" });
            DropColumn("dbo.TimeNorms", "KindOfLoadBlueAsteriskPracticName");
            DropColumn("dbo.TimeNorms", "KindOfLoadBlueAsteriskAttributeName");
            DropColumn("dbo.TimeNorms", "KindOfLoadBlueAsteriskName");
            DropColumn("dbo.TimeNorms", "KindOfLoadAttributeName");
            DropColumn("dbo.TimeNorms", "KindOfLoadName");
            DropColumn("dbo.TimeNorms", "TimeNormOrder");
            DropColumn("dbo.TimeNorms", "TimeNormShortName");
            DropColumn("dbo.TimeNorms", "TimeNormName");
            DropColumn("dbo.AcademicPlanRecordElements", "TimeNormId");
            CreateIndex("dbo.TimeNorms", "KindOfLoadId");
            CreateIndex("dbo.LoadDistributionMissions", "LecturerId");
            CreateIndex("dbo.LoadDistributionMissions", "LoadDistributionRecordId");
            CreateIndex("dbo.LoadDistributionRecords", "TimeNormId");
            CreateIndex("dbo.LoadDistributionRecords", "AcademicPlanRecordId");
            CreateIndex("dbo.LoadDistributionRecords", "LoadDistributionId");
            CreateIndex("dbo.LoadDistributions", "AcademicYearId");
            CreateIndex("dbo.AcademicPlanRecordElements", "KindOfLoadId");
            AddForeignKey("dbo.LoadDistributionRecords", "TimeNormId", "dbo.TimeNorms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TimeNorms", "KindOfLoadId", "dbo.KindOfLoads", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AcademicPlanRecordElements", "KindOfLoadId", "dbo.KindOfLoads", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LoadDistributionMissions", "LoadDistributionRecordId", "dbo.LoadDistributionRecords", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LoadDistributionMissions", "LecturerId", "dbo.Lecturers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LoadDistributionRecords", "LoadDistributionId", "dbo.LoadDistributions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LoadDistributionRecords", "AcademicPlanRecordId", "dbo.AcademicPlanRecords", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LoadDistributions", "AcademicYearId", "dbo.AcademicYears", "Id", cascadeDelete: true);
        }
    }
}
