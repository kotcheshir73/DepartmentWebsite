namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStudentTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentGroups", "StewardId", "dbo.Students");
            DropIndex("dbo.Users", new[] { "StudentId" });
            DropIndex("dbo.StudentGroups", new[] { "StewardId" });
            DropPrimaryKey("dbo.Students");
            AddColumn("dbo.Users", "Student_NumberOfBook", c => c.String(maxLength: 10));
            AddColumn("dbo.Students", "NumberOfBook", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.StudentGroups", "Steward_NumberOfBook", c => c.String(maxLength: 10));
            AddPrimaryKey("dbo.Students", "NumberOfBook");
            CreateIndex("dbo.Users", "Student_NumberOfBook");
            CreateIndex("dbo.StudentGroups", "Steward_NumberOfBook");
            AddForeignKey("dbo.Users", "Student_NumberOfBook", "dbo.Students", "NumberOfBook");
            AddForeignKey("dbo.StudentGroups", "Steward_NumberOfBook", "dbo.Students", "NumberOfBook");
            DropColumn("dbo.Students", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Id", c => c.Long(nullable: false, identity: true));
            DropForeignKey("dbo.StudentGroups", "Steward_NumberOfBook", "dbo.Students");
            DropForeignKey("dbo.Users", "Student_NumberOfBook", "dbo.Students");
            DropIndex("dbo.StudentGroups", new[] { "Steward_NumberOfBook" });
            DropIndex("dbo.Users", new[] { "Student_NumberOfBook" });
            DropPrimaryKey("dbo.Students");
            DropColumn("dbo.StudentGroups", "Steward_NumberOfBook");
            DropColumn("dbo.Students", "NumberOfBook");
            DropColumn("dbo.Users", "Student_NumberOfBook");
            AddPrimaryKey("dbo.Students", "Id");
            CreateIndex("dbo.StudentGroups", "StewardId");
            CreateIndex("dbo.Users", "StudentId");
            AddForeignKey("dbo.StudentGroups", "StewardId", "dbo.Students", "Id");
            AddForeignKey("dbo.Users", "StudentId", "dbo.Students", "Id");
        }
    }
}
