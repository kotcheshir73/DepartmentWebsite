namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateKindOfLoad : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.KindOfLoads", "KindOfLoadName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.KindOfLoads", "KindOfLoadName", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
