namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProcessAccountingModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicYearProcesses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DepartmentProcessId = c.Guid(nullable: false),
                        AcademicYearId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                        UserConfirmedId = c.Guid(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DepartmentProcesses", t => t.DepartmentProcessId, cascadeDelete: true)
                .Index(t => t.DepartmentProcessId);
            
            CreateTable(
                "dbo.DepartmentProcesses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Executor = c.Int(nullable: false),
                        DateStart = c.DateTime(),
                        DateFinish = c.DateTime(),
                        SemesterDateStart = c.Int(),
                        SemesterDateStartIndent = c.Int(),
                        Periodicity = c.Int(),
                        SemesterDateFinish = c.Int(),
                        SemesterDateFinishIndent = c.Int(),
                        Confirmability = c.Boolean(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProcessDirectionRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DepartmentProcessId = c.Guid(nullable: false),
                        EducationDirectionId = c.Guid(nullable: false),
                        AcademicCourse = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DepartmentProcesses", t => t.DepartmentProcessId, cascadeDelete: true)
                .Index(t => t.DepartmentProcessId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProcessDirectionRecords", "DepartmentProcessId", "dbo.DepartmentProcesses");
            DropForeignKey("dbo.AcademicYearProcesses", "DepartmentProcessId", "dbo.DepartmentProcesses");
            DropIndex("dbo.ProcessDirectionRecords", new[] { "DepartmentProcessId" });
            DropIndex("dbo.AcademicYearProcesses", new[] { "DepartmentProcessId" });
            DropTable("dbo.ProcessDirectionRecords");
            DropTable("dbo.DepartmentProcesses");
            DropTable("dbo.AcademicYearProcesses");
        }
    }
}
