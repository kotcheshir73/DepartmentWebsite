namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateForBlueAsteriskSecondPart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Disciplines", "DisciplineParentId", c => c.Guid());
            AddColumn("dbo.Disciplines", "IsParent", c => c.Boolean(nullable: false));
            AddColumn("dbo.DisciplineBlocks", "DisciplineBlockBlueAsteriskName", c => c.String(maxLength: 20));
            AddColumn("dbo.DisciplineBlocks", "DisciplineBlockUseForGrouping", c => c.Boolean(nullable: false));
            AddColumn("dbo.DisciplineBlocks", "DisciplineBlockOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DisciplineBlocks", "DisciplineBlockOrder");
            DropColumn("dbo.DisciplineBlocks", "DisciplineBlockUseForGrouping");
            DropColumn("dbo.DisciplineBlocks", "DisciplineBlockBlueAsteriskName");
            DropColumn("dbo.Disciplines", "IsParent");
            DropColumn("dbo.Disciplines", "DisciplineParentId");
        }
    }
}
