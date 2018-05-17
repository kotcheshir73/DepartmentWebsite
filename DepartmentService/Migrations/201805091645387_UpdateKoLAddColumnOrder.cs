namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateKoLAddColumnOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KindOfLoads", "KindOfLoadOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KindOfLoads", "KindOfLoadOrder");
        }
    }
}
