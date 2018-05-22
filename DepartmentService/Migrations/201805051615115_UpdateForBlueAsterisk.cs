namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateForBlueAsterisk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KindOfLoads", "KindOfLoadBlueAsteriskName", c => c.String(maxLength: 50));
            AddColumn("dbo.Disciplines", "DisciplineBlueAsteriskName", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Disciplines", "DisciplineBlueAsteriskName");
            DropColumn("dbo.KindOfLoads", "KindOfLoadBlueAsteriskName");
        }
    }
}
