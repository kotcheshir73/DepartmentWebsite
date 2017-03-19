namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStudentNullStudentGroup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "StudentGroupId", "dbo.StudentGroups");
            DropIndex("dbo.Students", new[] { "StudentGroupId" });
            AlterColumn("dbo.Students", "StudentGroupId", c => c.Long());
            CreateIndex("dbo.Students", "StudentGroupId");
            AddForeignKey("dbo.Students", "StudentGroupId", "dbo.StudentGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "StudentGroupId", "dbo.StudentGroups");
            DropIndex("dbo.Students", new[] { "StudentGroupId" });
            AlterColumn("dbo.Students", "StudentGroupId", c => c.Long(nullable: false));
            CreateIndex("dbo.Students", "StudentGroupId");
            AddForeignKey("dbo.Students", "StudentGroupId", "dbo.StudentGroups", "Id", cascadeDelete: true);
        }
    }
}
