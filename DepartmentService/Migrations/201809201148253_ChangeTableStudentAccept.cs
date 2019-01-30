namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTableStudentAccept : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DisciplineLessonTaskStudentRecords", newName: "DisciplineLessonTaskStudentAccepts");
            AddColumn("dbo.DisciplineLessonTaskStudentAccepts", "DateAccept", c => c.DateTime(nullable: false));
            AddColumn("dbo.DisciplineLessonTaskStudentAccepts", "Log", c => c.String());
            AlterColumn("dbo.DisciplineLessonTaskStudentAccepts", "Score", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.DisciplineLessonTaskStudentAccepts", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DisciplineLessonTaskStudentAccepts", "Date", c => c.DateTime());
            AlterColumn("dbo.DisciplineLessonTaskStudentAccepts", "Score", c => c.Int(nullable: false));
            DropColumn("dbo.DisciplineLessonTaskStudentAccepts", "Log");
            DropColumn("dbo.DisciplineLessonTaskStudentAccepts", "DateAccept");
            RenameTable(name: "dbo.DisciplineLessonTaskStudentAccepts", newName: "DisciplineLessonTaskStudentRecords");
        }
    }
}
