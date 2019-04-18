namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisiplineLessonLinkEducationDirection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DisciplineLessons", "EducationDirectionId", c => c.Guid(nullable: false));
            CreateIndex("dbo.DisciplineLessons", "EducationDirectionId");
            AddForeignKey("dbo.DisciplineLessons", "EducationDirectionId", "dbo.EducationDirections", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DisciplineLessons", "EducationDirectionId", "dbo.EducationDirections");
            DropIndex("dbo.DisciplineLessons", new[] { "EducationDirectionId" });
            DropColumn("dbo.DisciplineLessons", "EducationDirectionId");
        }
    }
}
