namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableDLCS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DisciplineLessonConductedStudents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineLessonConductedId = c.Guid(nullable: false),
                        StudentId = c.Guid(nullable: false),
                        Comment = c.String(),
                        Status = c.Int(nullable: false),
                        Ball = c.Int(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineLessonConducteds", t => t.DisciplineLessonConductedId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.DisciplineLessonConductedId)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DisciplineLessonConductedStudents", "StudentId", "dbo.Students");
            DropForeignKey("dbo.DisciplineLessonConductedStudents", "DisciplineLessonConductedId", "dbo.DisciplineLessonConducteds");
            DropIndex("dbo.DisciplineLessonConductedStudents", new[] { "StudentId" });
            DropIndex("dbo.DisciplineLessonConductedStudents", new[] { "DisciplineLessonConductedId" });
            DropTable("dbo.DisciplineLessonConductedStudents");
        }
    }
}
