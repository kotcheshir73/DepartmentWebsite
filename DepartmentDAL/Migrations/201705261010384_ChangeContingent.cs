namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeContingent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contingents", "StudentGroupId", "dbo.StudentGroups");
            DropIndex("dbo.Contingents", new[] { "StudentGroupId" });
            AddColumn("dbo.Contingents", "EducationDirectionId", c => c.Long(nullable: false));
            AddColumn("dbo.Contingents", "Course", c => c.Int(nullable: false));
            CreateIndex("dbo.Contingents", "EducationDirectionId");
            AddForeignKey("dbo.Contingents", "EducationDirectionId", "dbo.EducationDirections", "Id", cascadeDelete: true);
            DropColumn("dbo.Contingents", "StudentGroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contingents", "StudentGroupId", c => c.Long(nullable: false));
            DropForeignKey("dbo.Contingents", "EducationDirectionId", "dbo.EducationDirections");
            DropIndex("dbo.Contingents", new[] { "EducationDirectionId" });
            DropColumn("dbo.Contingents", "Course");
            DropColumn("dbo.Contingents", "EducationDirectionId");
            CreateIndex("dbo.Contingents", "StudentGroupId");
            AddForeignKey("dbo.Contingents", "StudentGroupId", "dbo.StudentGroups", "Id", cascadeDelete: true);
        }
    }
}
