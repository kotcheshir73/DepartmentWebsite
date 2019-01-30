namespace DepartmentContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GlobalUpdateWebAndLoadDistribute : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GraficClassrooms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GraficId = c.Guid(nullable: false),
                        TimeNormId = c.Guid(nullable: false),
                        ClassroomDescription = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grafics", t => t.GraficId, cascadeDelete: true)
                .ForeignKey("dbo.TimeNorms", t => t.TimeNormId, cascadeDelete: true)
                .Index(t => t.GraficId)
                .Index(t => t.TimeNormId);
            
            CreateTable(
                "dbo.Grafics",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AcademicPlanRecordId = c.Guid(nullable: false),
                        StudentGroupId = c.Guid(nullable: false),
                        Comment = c.String(),
                        CommentWishesOfTeacher = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicPlanRecords", t => t.AcademicPlanRecordId, cascadeDelete: true)
                .ForeignKey("dbo.StudentGroups", t => t.StudentGroupId, cascadeDelete: true)
                .Index(t => t.AcademicPlanRecordId)
                .Index(t => t.StudentGroupId);
            
            CreateTable(
                "dbo.GraficRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GraficId = c.Guid(nullable: false),
                        TimeNormId = c.Guid(nullable: false),
                        WeekNumber = c.Int(nullable: false),
                        Hours = c.Double(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grafics", t => t.GraficId, cascadeDelete: true)
                .ForeignKey("dbo.TimeNorms", t => t.TimeNormId, cascadeDelete: true)
                .Index(t => t.GraficId)
                .Index(t => t.TimeNormId);
            
            CreateTable(
                "dbo.IndividualPlanRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IndividualPlanKindOfWorkId = c.Guid(nullable: false),
                        LecturerId = c.Guid(nullable: false),
                        AcademicYearId = c.Guid(nullable: false),
                        PlanAutumn = c.Double(nullable: false),
                        FactAutumn = c.Double(nullable: false),
                        PlanSpring = c.Double(nullable: false),
                        FactSpring = c.Double(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .ForeignKey("dbo.IndividualPlanKindOfWorks", t => t.IndividualPlanKindOfWorkId, cascadeDelete: true)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId, cascadeDelete: true)
                .Index(t => t.IndividualPlanKindOfWorkId)
                .Index(t => t.LecturerId)
                .Index(t => t.AcademicYearId);
            
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
            
            CreateTable(
                "dbo.LecturerWorkloads",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LecturerId = c.Guid(nullable: false),
                        AcademicYearId = c.Guid(nullable: false),
                        Workload = c.Double(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId, cascadeDelete: true)
                .Index(t => t.LecturerId)
                .Index(t => t.AcademicYearId);
            
            CreateTable(
                "dbo.Statements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LecturerId = c.Guid(nullable: false),
                        AcademicPlanRecordId = c.Guid(nullable: false),
                        StudentGroupId = c.Guid(nullable: false),
                        Course = c.Int(nullable: false),
                        TypeOfTest = c.Int(nullable: false),
                        Semester = c.Int(),
                        Date = c.DateTime(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicPlanRecords", t => t.AcademicPlanRecordId, cascadeDelete: true)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId, cascadeDelete: true)
                .ForeignKey("dbo.StudentGroups", t => t.StudentGroupId, cascadeDelete: true)
                .Index(t => t.LecturerId)
                .Index(t => t.AcademicPlanRecordId)
                .Index(t => t.StudentGroupId);
            
            CreateTable(
                "dbo.StatementRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StatementId = c.Guid(nullable: false),
                        StudentId = c.Guid(nullable: false),
                        Score = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Statements", t => t.StatementId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StatementId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.StatementRecordExtendeds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StatementRecordId = c.Guid(nullable: false),
                        Name = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StatementRecords", t => t.StatementRecordId, cascadeDelete: true)
                .Index(t => t.StatementRecordId);
            
            CreateTable(
                "dbo.IndividualPlanNIRContractualWorks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LecturerId = c.Guid(nullable: false),
                        JobContent = c.String(),
                        Post = c.String(),
                        PlannedTerm = c.String(),
                        ReadyMark = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId, cascadeDelete: true)
                .Index(t => t.LecturerId);
            
            CreateTable(
                "dbo.IndividualPlanNIRScientificArticles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LecturerId = c.Guid(nullable: false),
                        Name = c.String(),
                        TypeOfPublication = c.String(),
                        Volume = c.String(),
                        Publishing = c.String(),
                        Year = c.String(),
                        Status = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId, cascadeDelete: true)
                .Index(t => t.LecturerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IndividualPlanNIRScientificArticles", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.IndividualPlanNIRContractualWorks", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.GraficClassrooms", "TimeNormId", "dbo.TimeNorms");
            DropForeignKey("dbo.Grafics", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.Statements", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.StatementRecords", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StatementRecordExtendeds", "StatementRecordId", "dbo.StatementRecords");
            DropForeignKey("dbo.StatementRecords", "StatementId", "dbo.Statements");
            DropForeignKey("dbo.Statements", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.Statements", "AcademicPlanRecordId", "dbo.AcademicPlanRecords");
            DropForeignKey("dbo.LecturerWorkloads", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.LecturerWorkloads", "AcademicYearId", "dbo.AcademicYears");
            DropForeignKey("dbo.IndividualPlanRecords", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.IndividualPlanKindOfWorks", "IndividualPlanTitleId", "dbo.IndividualPlanTitles");
            DropForeignKey("dbo.IndividualPlanRecords", "IndividualPlanKindOfWorkId", "dbo.IndividualPlanKindOfWorks");
            DropForeignKey("dbo.IndividualPlanRecords", "AcademicYearId", "dbo.AcademicYears");
            DropForeignKey("dbo.GraficRecords", "TimeNormId", "dbo.TimeNorms");
            DropForeignKey("dbo.GraficRecords", "GraficId", "dbo.Grafics");
            DropForeignKey("dbo.GraficClassrooms", "GraficId", "dbo.Grafics");
            DropForeignKey("dbo.Grafics", "AcademicPlanRecordId", "dbo.AcademicPlanRecords");
            DropIndex("dbo.IndividualPlanNIRScientificArticles", new[] { "LecturerId" });
            DropIndex("dbo.IndividualPlanNIRContractualWorks", new[] { "LecturerId" });
            DropIndex("dbo.StatementRecordExtendeds", new[] { "StatementRecordId" });
            DropIndex("dbo.StatementRecords", new[] { "StudentId" });
            DropIndex("dbo.StatementRecords", new[] { "StatementId" });
            DropIndex("dbo.Statements", new[] { "StudentGroupId" });
            DropIndex("dbo.Statements", new[] { "AcademicPlanRecordId" });
            DropIndex("dbo.Statements", new[] { "LecturerId" });
            DropIndex("dbo.LecturerWorkloads", new[] { "AcademicYearId" });
            DropIndex("dbo.LecturerWorkloads", new[] { "LecturerId" });
            DropIndex("dbo.IndividualPlanKindOfWorks", new[] { "IndividualPlanTitleId" });
            DropIndex("dbo.IndividualPlanRecords", new[] { "AcademicYearId" });
            DropIndex("dbo.IndividualPlanRecords", new[] { "LecturerId" });
            DropIndex("dbo.IndividualPlanRecords", new[] { "IndividualPlanKindOfWorkId" });
            DropIndex("dbo.GraficRecords", new[] { "TimeNormId" });
            DropIndex("dbo.GraficRecords", new[] { "GraficId" });
            DropIndex("dbo.Grafics", new[] { "StudentGroupId" });
            DropIndex("dbo.Grafics", new[] { "AcademicPlanRecordId" });
            DropIndex("dbo.GraficClassrooms", new[] { "TimeNormId" });
            DropIndex("dbo.GraficClassrooms", new[] { "GraficId" });
            DropTable("dbo.IndividualPlanNIRScientificArticles");
            DropTable("dbo.IndividualPlanNIRContractualWorks");
            DropTable("dbo.StatementRecordExtendeds");
            DropTable("dbo.StatementRecords");
            DropTable("dbo.Statements");
            DropTable("dbo.LecturerWorkloads");
            DropTable("dbo.IndividualPlanTitles");
            DropTable("dbo.IndividualPlanKindOfWorks");
            DropTable("dbo.IndividualPlanRecords");
            DropTable("dbo.GraficRecords");
            DropTable("dbo.Grafics");
            DropTable("dbo.GraficClassrooms");
        }
    }
}
