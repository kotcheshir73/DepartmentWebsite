namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "DateLastVisit", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "DateLastVisit", c => c.DateTime(nullable: false));
        }
    }
}
