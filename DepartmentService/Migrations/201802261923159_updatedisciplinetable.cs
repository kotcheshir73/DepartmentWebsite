namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedisciplinetable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DisciplineLessonTaskImageContexts", "DisciplineLessonTaskId", "dbo.DisciplineLessonTasks");
            DropForeignKey("dbo.DisciplineLessonTaskTextContexts", "DisciplineLessonTaskId", "dbo.DisciplineLessonTasks");
            DropIndex("dbo.DisciplineLessonTaskImageContexts", new[] { "DisciplineLessonTaskId" });
            DropIndex("dbo.DisciplineLessonTaskTextContexts", new[] { "DisciplineLessonTaskId" });
            AddColumn("dbo.DisciplineLessons", "Order", c => c.Int(nullable: false));
            AddColumn("dbo.DisciplineLessons", "DisciplineLessonFile", c => c.Binary());
            AddColumn("dbo.DisciplineLessonTasks", "DisciplineLessonName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.DisciplineLessonTasks", "Description", c => c.String());
            AddColumn("dbo.DisciplineLessonTasks", "Image", c => c.Binary());
            DropTable("dbo.DisciplineLessonTaskImageContexts");
            DropTable("dbo.DisciplineLessonTaskTextContexts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DisciplineLessonTaskTextContexts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(nullable: false),
                        DisciplineLessonTaskId = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DisciplineLessonTaskImageContexts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Image = c.Binary(nullable: false),
                        DisciplineLessonTaskId = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.DisciplineLessonTasks", "Image");
            DropColumn("dbo.DisciplineLessonTasks", "Description");
            DropColumn("dbo.DisciplineLessonTasks", "DisciplineLessonName");
            DropColumn("dbo.DisciplineLessons", "DisciplineLessonFile");
            DropColumn("dbo.DisciplineLessons", "Order");
            CreateIndex("dbo.DisciplineLessonTaskTextContexts", "DisciplineLessonTaskId");
            CreateIndex("dbo.DisciplineLessonTaskImageContexts", "DisciplineLessonTaskId");
            AddForeignKey("dbo.DisciplineLessonTaskTextContexts", "DisciplineLessonTaskId", "dbo.DisciplineLessonTasks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DisciplineLessonTaskImageContexts", "DisciplineLessonTaskId", "dbo.DisciplineLessonTasks", "Id", cascadeDelete: true);
        }
    }
}
