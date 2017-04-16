namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTimeNormAndKindOfLoad : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeNorms", "ParentTimeNormId", "dbo.TimeNorms");
            DropIndex("dbo.TimeNorms", new[] { "ParentTimeNormId" });
            DropColumn("dbo.TimeNorms", "ParentTimeNormId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimeNorms", "ParentTimeNormId", c => c.Long());
            CreateIndex("dbo.TimeNorms", "ParentTimeNormId");
            AddForeignKey("dbo.TimeNorms", "ParentTimeNormId", "dbo.TimeNorms", "Id");
        }
    }
}
