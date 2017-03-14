namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStudent : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.StudentGroups", new[] { "Steward_NumberOfBook" });
            DropColumn("dbo.StudentGroups", "StewardId");
            RenameColumn(table: "dbo.StudentGroups", name: "Steward_NumberOfBook", newName: "StewardId");
            AlterColumn("dbo.StudentGroups", "StewardId", c => c.String(maxLength: 10));
            CreateIndex("dbo.StudentGroups", "StewardId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.StudentGroups", new[] { "StewardId" });
            AlterColumn("dbo.StudentGroups", "StewardId", c => c.Long());
            RenameColumn(table: "dbo.StudentGroups", name: "StewardId", newName: "Steward_NumberOfBook");
            AddColumn("dbo.StudentGroups", "StewardId", c => c.Long());
            CreateIndex("dbo.StudentGroups", "Steward_NumberOfBook");
        }
    }
}
