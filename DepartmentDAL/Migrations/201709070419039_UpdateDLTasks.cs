namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDLTasks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DisciplineLessonStudentRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        DisciplineLessonId = c.Long(nullable: false),
                        StudentId = c.String(maxLength: 10),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineLessons", t => t.DisciplineLessonId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.DisciplineLessonId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.DisciplineLessons",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LessonType = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        DisciplineId = c.Long(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId, cascadeDelete: true)
                .Index(t => t.DisciplineId);
            
            CreateTable(
                "dbo.DisciplineStudentRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Variant = c.Int(nullable: false),
                        SubGroup = c.Int(nullable: false),
                        DisciplineId = c.Long(nullable: false),
                        StudentId = c.String(maxLength: 10),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.DisciplineId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.DisciplineLessonTasks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        VariantNumber = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        MaxBall = c.Decimal(precision: 18, scale: 2),
                        DisciplineLessonId = c.Long(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineLessons", t => t.DisciplineLessonId, cascadeDelete: true)
                .Index(t => t.DisciplineLessonId);
            
            CreateTable(
                "dbo.DisciplineLessonTaskImageContexts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Image = c.Binary(),
                        Order = c.Int(nullable: false),
                        DisciplineLessonTaskId = c.Long(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineLessonTasks", t => t.DisciplineLessonTaskId, cascadeDelete: true)
                .Index(t => t.DisciplineLessonTaskId);
            
            CreateTable(
                "dbo.DisciplineLessonTaskStudentRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Result = c.Int(nullable: false),
                        Comment = c.String(),
                        DisciplineLessonTaskId = c.Long(nullable: false),
                        StudentId = c.String(maxLength: 10),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineLessonTasks", t => t.DisciplineLessonTaskId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.DisciplineLessonTaskId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.DisciplineLessonTaskTextContexts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Text = c.String(),
                        Order = c.Int(nullable: false),
                        DisciplineLessonTaskId = c.Long(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineLessonTasks", t => t.DisciplineLessonTaskId, cascadeDelete: true)
                .Index(t => t.DisciplineLessonTaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DisciplineLessonStudentRecords", "StudentId", "dbo.Students");
            DropForeignKey("dbo.DisciplineLessonTaskTextContexts", "DisciplineLessonTaskId", "dbo.DisciplineLessonTasks");
            DropForeignKey("dbo.DisciplineLessonTaskStudentRecords", "StudentId", "dbo.Students");
            DropForeignKey("dbo.DisciplineLessonTaskStudentRecords", "DisciplineLessonTaskId", "dbo.DisciplineLessonTasks");
            DropForeignKey("dbo.DisciplineLessonTaskImageContexts", "DisciplineLessonTaskId", "dbo.DisciplineLessonTasks");
            DropForeignKey("dbo.DisciplineLessonTasks", "DisciplineLessonId", "dbo.DisciplineLessons");
            DropForeignKey("dbo.DisciplineLessonStudentRecords", "DisciplineLessonId", "dbo.DisciplineLessons");
            DropForeignKey("dbo.DisciplineStudentRecords", "StudentId", "dbo.Students");
            DropForeignKey("dbo.DisciplineStudentRecords", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.DisciplineLessons", "DisciplineId", "dbo.Disciplines");
            DropIndex("dbo.DisciplineLessonTaskTextContexts", new[] { "DisciplineLessonTaskId" });
            DropIndex("dbo.DisciplineLessonTaskStudentRecords", new[] { "StudentId" });
            DropIndex("dbo.DisciplineLessonTaskStudentRecords", new[] { "DisciplineLessonTaskId" });
            DropIndex("dbo.DisciplineLessonTaskImageContexts", new[] { "DisciplineLessonTaskId" });
            DropIndex("dbo.DisciplineLessonTasks", new[] { "DisciplineLessonId" });
            DropIndex("dbo.DisciplineStudentRecords", new[] { "StudentId" });
            DropIndex("dbo.DisciplineStudentRecords", new[] { "DisciplineId" });
            DropIndex("dbo.DisciplineLessons", new[] { "DisciplineId" });
            DropIndex("dbo.DisciplineLessonStudentRecords", new[] { "StudentId" });
            DropIndex("dbo.DisciplineLessonStudentRecords", new[] { "DisciplineLessonId" });
            DropTable("dbo.DisciplineLessonTaskTextContexts");
            DropTable("dbo.DisciplineLessonTaskStudentRecords");
            DropTable("dbo.DisciplineLessonTaskImageContexts");
            DropTable("dbo.DisciplineLessonTasks");
            DropTable("dbo.DisciplineStudentRecords");
            DropTable("dbo.DisciplineLessons");
            DropTable("dbo.DisciplineLessonStudentRecords");
        }
    }
}
