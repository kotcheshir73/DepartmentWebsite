namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisiplineLessonMigration : DbMigration
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
            
            AddColumn("dbo.DisciplineLessons", "CountOfPairs", c => c.Int(nullable: false));
            AddColumn("dbo.DisciplineLessons", "Date", c => c.DateTime());
            AddColumn("dbo.DisciplineLessonTaskStudentRecords", "Date", c => c.DateTime());
            AddColumn("dbo.DisciplineLessonTasks", "Task", c => c.String(nullable: false));
            AddColumn("dbo.DisciplineLessonTasks", "IsNecessarily", c => c.Boolean(nullable: false));
            DropColumn("dbo.DisciplineLessonTasks", "VariantNumber");
            DropColumn("dbo.DisciplineLessonTasks", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DisciplineLessonTasks", "Image", c => c.Binary());
            AddColumn("dbo.DisciplineLessonTasks", "VariantNumber", c => c.Int(nullable: false));
            DropForeignKey("dbo.DisciplineLessonTaskVariants", "DisciplineLessonTaskId", "dbo.DisciplineLessonTasks");
            DropIndex("dbo.DisciplineLessonTaskVariants", new[] { "DisciplineLessonTaskId" });
            DropColumn("dbo.DisciplineLessonTasks", "IsNecessarily");
            DropColumn("dbo.DisciplineLessonTasks", "Task");
            DropColumn("dbo.DisciplineLessonTaskStudentRecords", "Date");
            DropColumn("dbo.DisciplineLessons", "Date");
            DropColumn("dbo.DisciplineLessons", "CountOfPairs");
            DropTable("dbo.DisciplineLessonTaskVariants");
        }
    }
}
