namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldIsStewardIntoStudent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "IsSteward", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "IsSteward");
        }
    }
}
