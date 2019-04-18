namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisiplineLessonLinkAcademicYear : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DisciplineLessons", "AcademicYearId", c => c.Guid(nullable: false));
            CreateIndex("dbo.DisciplineLessons", "AcademicYearId");
            AddForeignKey("dbo.DisciplineLessons", "AcademicYearId", "dbo.AcademicYears", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DisciplineLessons", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.DisciplineLessons", new[] { "AcademicYearId" });
            DropColumn("dbo.DisciplineLessons", "AcademicYearId");
        }
    }
}
