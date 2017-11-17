namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserAvatar : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Avatar", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Avatar", c => c.Binary(nullable: false));
        }
    }
}
