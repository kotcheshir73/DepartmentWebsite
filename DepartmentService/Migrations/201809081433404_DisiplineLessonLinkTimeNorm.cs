namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisiplineLessonLinkTimeNorm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DisciplineLessons", "TimeNormId", c => c.Guid(nullable: false));
            AddColumn("dbo.TimeNorms", "UseInLearningProgress", c => c.Boolean(nullable: false));
            CreateIndex("dbo.DisciplineLessons", "TimeNormId");
            AddForeignKey("dbo.DisciplineLessons", "TimeNormId", "dbo.TimeNorms", "Id", cascadeDelete: true);
            DropColumn("dbo.DisciplineLessons", "LessonType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DisciplineLessons", "LessonType", c => c.Int(nullable: false));
            DropForeignKey("dbo.DisciplineLessons", "TimeNormId", "dbo.TimeNorms");
            DropIndex("dbo.DisciplineLessons", new[] { "TimeNormId" });
            DropColumn("dbo.TimeNorms", "UseInLearningProgress");
            DropColumn("dbo.DisciplineLessons", "TimeNormId");
        }
    }
}
