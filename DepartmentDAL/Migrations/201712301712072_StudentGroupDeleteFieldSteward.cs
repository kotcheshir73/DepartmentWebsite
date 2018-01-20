namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentGroupDeleteFieldSteward : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentGroups", "FK_dbo.StudentGroups_dbo.Students_Steward_NumberOfBook");
            DropIndex("dbo.StudentGroups", new[] { "StewardId" });
            AddColumn("dbo.StudentGroups", "StewardName", c => c.String());
            DropColumn("dbo.StudentGroups", "StewardId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentGroups", "StewardId", c => c.String(maxLength: 10));
            DropColumn("dbo.StudentGroups", "StewardName");
            CreateIndex("dbo.StudentGroups", "StewardId");
            AddForeignKey("dbo.StudentGroups", "StewardId", "dbo.Students", "NumberOfBook");
        }
    }
}
