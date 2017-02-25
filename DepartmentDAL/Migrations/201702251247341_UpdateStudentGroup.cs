namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStudentGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentGroups", "Capacity", c => c.Int(nullable: false));
            AddColumn("dbo.StudentGroups", "SubgroupsCount", c => c.Int(nullable: false));
            AddColumn("dbo.StudentGroups", "StewardId", c => c.Long());
            AddColumn("dbo.StudentGroups", "CuratorId", c => c.Long());
            CreateIndex("dbo.StudentGroups", "StewardId");
            CreateIndex("dbo.StudentGroups", "CuratorId");
            AddForeignKey("dbo.StudentGroups", "CuratorId", "dbo.Lecturers", "Id");
            AddForeignKey("dbo.StudentGroups", "StewardId", "dbo.Students", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentGroups", "StewardId", "dbo.Students");
            DropForeignKey("dbo.StudentGroups", "CuratorId", "dbo.Lecturers");
            DropIndex("dbo.StudentGroups", new[] { "CuratorId" });
            DropIndex("dbo.StudentGroups", new[] { "StewardId" });
            DropColumn("dbo.StudentGroups", "CuratorId");
            DropColumn("dbo.StudentGroups", "StewardId");
            DropColumn("dbo.StudentGroups", "SubgroupsCount");
            DropColumn("dbo.StudentGroups", "Capacity");
        }
    }
}
