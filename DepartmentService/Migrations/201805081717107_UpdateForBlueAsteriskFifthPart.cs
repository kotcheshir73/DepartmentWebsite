namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateForBlueAsteriskFifthPart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KindOfLoads", "KindOfLoadBlueAsteriskPracticName", c => c.String(maxLength: 100));
            AlterColumn("dbo.KindOfLoads", "KindOfLoadName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.KindOfLoads", "AttributeName", c => c.String(maxLength: 10));
            AlterColumn("dbo.KindOfLoads", "KindOfLoadBlueAsteriskName", c => c.String(maxLength: 100));
            AlterColumn("dbo.KindOfLoads", "KindOfLoadBlueAsteriskAttributeName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.KindOfLoads", "KindOfLoadBlueAsteriskAttributeName", c => c.String(maxLength: 50));
            AlterColumn("dbo.KindOfLoads", "KindOfLoadBlueAsteriskName", c => c.String(maxLength: 50));
            AlterColumn("dbo.KindOfLoads", "AttributeName", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.KindOfLoads", "KindOfLoadName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.KindOfLoads", "KindOfLoadBlueAsteriskPracticName");
        }
    }
}
