namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSemestrRecord1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SemesterRecords", "StudentGroupId", "dbo.StudentGroups");
            DropIndex("dbo.SemesterRecords", new[] { "StudentGroupId" });
            AddColumn("dbo.SemesterRecords", "LessonGroupName", c => c.String());
            AlterColumn("dbo.SemesterRecords", "StudentGroupId", c => c.Long());
            CreateIndex("dbo.SemesterRecords", "StudentGroupId");
            AddForeignKey("dbo.SemesterRecords", "StudentGroupId", "dbo.StudentGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SemesterRecords", "StudentGroupId", "dbo.StudentGroups");
            DropIndex("dbo.SemesterRecords", new[] { "StudentGroupId" });
            AlterColumn("dbo.SemesterRecords", "StudentGroupId", c => c.Long(nullable: false));
            DropColumn("dbo.SemesterRecords", "LessonGroupName");
            CreateIndex("dbo.SemesterRecords", "StudentGroupId");
            AddForeignKey("dbo.SemesterRecords", "StudentGroupId", "dbo.StudentGroups", "Id", cascadeDelete: true);
        }
    }
}
