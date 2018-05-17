namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateForBlueAsteriskFourthPart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KindOfLoads", "KindOfLoadBlueAsteriskAttributeName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KindOfLoads", "KindOfLoadBlueAsteriskAttributeName");
        }
    }
}
