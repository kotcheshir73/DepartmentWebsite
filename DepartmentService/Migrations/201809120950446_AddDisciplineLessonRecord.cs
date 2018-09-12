namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDisciplineLessonRecord : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DisciplineLessonStudentRecords", "DisciplineLessonId", "dbo.DisciplineLessons");
            DropIndex("dbo.DisciplineLessonStudentRecords", new[] { "DisciplineLessonId" });
            CreateTable(
                "dbo.DisciplineLessonRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineLessonId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Subgroup = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineLessons", t => t.DisciplineLessonId, cascadeDelete: true)
                .Index(t => t.DisciplineLessonId);
            
            AddColumn("dbo.DisciplineLessonStudentRecords", "DisciplineLessonRecordId", c => c.Guid(nullable: false));
            AddColumn("dbo.DisciplineLessonStudentRecords", "Ball", c => c.Int());
            CreateIndex("dbo.DisciplineLessonStudentRecords", "DisciplineLessonRecordId");
            AddForeignKey("dbo.DisciplineLessonStudentRecords", "DisciplineLessonRecordId", "dbo.DisciplineLessonRecords", "Id", cascadeDelete: true);
            DropColumn("dbo.DisciplineLessonStudentRecords", "DisciplineLessonId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DisciplineLessonStudentRecords", "DisciplineLessonId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.DisciplineLessonStudentRecords", "DisciplineLessonRecordId", "dbo.DisciplineLessonRecords");
            DropForeignKey("dbo.DisciplineLessonRecords", "DisciplineLessonId", "dbo.DisciplineLessons");
            DropIndex("dbo.DisciplineLessonRecords", new[] { "DisciplineLessonId" });
            DropIndex("dbo.DisciplineLessonStudentRecords", new[] { "DisciplineLessonRecordId" });
            DropColumn("dbo.DisciplineLessonStudentRecords", "Ball");
            DropColumn("dbo.DisciplineLessonStudentRecords", "DisciplineLessonRecordId");
            DropTable("dbo.DisciplineLessonRecords");
            CreateIndex("dbo.DisciplineLessonStudentRecords", "DisciplineLessonId");
            AddForeignKey("dbo.DisciplineLessonStudentRecords", "DisciplineLessonId", "dbo.DisciplineLessons", "Id", cascadeDelete: true);
        }
    }
}
