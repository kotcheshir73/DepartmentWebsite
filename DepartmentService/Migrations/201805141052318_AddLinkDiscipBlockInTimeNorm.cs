namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLinkDiscipBlockInTimeNorm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeNorms", "DisciplineBlockId", c => c.Guid(nullable: true));
            CreateIndex("dbo.TimeNorms", "DisciplineBlockId");
            AddForeignKey("dbo.TimeNorms", "DisciplineBlockId", "dbo.DisciplineBlocks", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeNorms", "DisciplineBlockId", "dbo.DisciplineBlocks");
            DropIndex("dbo.TimeNorms", new[] { "DisciplineBlockId" });
            DropColumn("dbo.TimeNorms", "DisciplineBlockId");
        }
    }
}
