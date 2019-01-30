namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_statement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatementRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StatementId = c.Guid(nullable: false),
                        StudentId = c.Guid(nullable: false),
                        Score = c.String(nullable: false),
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
                        Date = c.DateTime(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StatementRecords", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Statements", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.StatementRecords", "StatementId", "dbo.Statements");
            DropForeignKey("dbo.Statements", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.Statements", "AcademicPlanRecordId", "dbo.AcademicPlanRecords");
            DropIndex("dbo.Statements", new[] { "StudentGroupId" });
            DropIndex("dbo.Statements", new[] { "AcademicPlanRecordId" });
            DropIndex("dbo.Statements", new[] { "LecturerId" });
            DropIndex("dbo.StatementRecords", new[] { "StudentId" });
            DropIndex("dbo.StatementRecords", new[] { "StatementId" });
            DropTable("dbo.Statements");
            DropTable("dbo.StatementRecords");
        }
    }
}
