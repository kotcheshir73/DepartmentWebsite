namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DisciplineLessonTaskVariants",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineLessonTaskId = c.Guid(nullable: false),
                        VariantNumber = c.String(nullable: false),
                        VariantTask = c.String(nullable: false),
                        Order = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineLessonTasks", t => t.DisciplineLessonTaskId, cascadeDelete: true)
                .Index(t => t.DisciplineLessonTaskId);
            
            CreateTable(
                "dbo.Softwares",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SoftwareName = c.String(),
                        SoftwareDescription = c.String(),
                        SoftwareKey = c.String(),
                        SoftwareK = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.DisciplineLessons", "CountOfPairs", c => c.Int(nullable: false));
            AddColumn("dbo.DisciplineLessons", "Date", c => c.DateTime());
            AddColumn("dbo.DisciplineLessonTaskStudentRecords", "Date", c => c.DateTime());
            AddColumn("dbo.DisciplineLessonTasks", "Task", c => c.String(nullable: false));
            AddColumn("dbo.DisciplineLessonTasks", "IsNecessarily", c => c.Boolean(nullable: false));
            AddColumn("dbo.SoftwareRecords", "SoftwareId", c => c.Guid(nullable: false));
            AddColumn("dbo.SoftwareRecords", "SetupDescription", c => c.String());
            CreateIndex("dbo.SoftwareRecords", "SoftwareId");
            AddForeignKey("dbo.SoftwareRecords", "SoftwareId", "dbo.Softwares", "Id", cascadeDelete: true);
            DropColumn("dbo.DisciplineLessonTasks", "VariantNumber");
            DropColumn("dbo.DisciplineLessonTasks", "Image");
            DropColumn("dbo.SoftwareRecords", "SoftwareName");
            DropColumn("dbo.SoftwareRecords", "SoftwareDescription");
            DropColumn("dbo.SoftwareRecords", "SoftwareKey");
            DropColumn("dbo.SoftwareRecords", "SoftwareK");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SoftwareRecords", "SoftwareK", c => c.String());
            AddColumn("dbo.SoftwareRecords", "SoftwareKey", c => c.String());
            AddColumn("dbo.SoftwareRecords", "SoftwareDescription", c => c.String());
            AddColumn("dbo.SoftwareRecords", "SoftwareName", c => c.String());
            AddColumn("dbo.DisciplineLessonTasks", "Image", c => c.Binary());
            AddColumn("dbo.DisciplineLessonTasks", "VariantNumber", c => c.Int(nullable: false));
            DropForeignKey("dbo.SoftwareRecords", "SoftwareId", "dbo.Softwares");
            DropForeignKey("dbo.DisciplineLessonTaskVariants", "DisciplineLessonTaskId", "dbo.DisciplineLessonTasks");
            DropIndex("dbo.SoftwareRecords", new[] { "SoftwareId" });
            DropIndex("dbo.DisciplineLessonTaskVariants", new[] { "DisciplineLessonTaskId" });
            DropColumn("dbo.SoftwareRecords", "SetupDescription");
            DropColumn("dbo.SoftwareRecords", "SoftwareId");
            DropColumn("dbo.DisciplineLessonTasks", "IsNecessarily");
            DropColumn("dbo.DisciplineLessonTasks", "Task");
            DropColumn("dbo.DisciplineLessonTaskStudentRecords", "Date");
            DropColumn("dbo.DisciplineLessons", "Date");
            DropColumn("dbo.DisciplineLessons", "CountOfPairs");
            DropTable("dbo.Softwares");
            DropTable("dbo.DisciplineLessonTaskVariants");
        }
    }
}
