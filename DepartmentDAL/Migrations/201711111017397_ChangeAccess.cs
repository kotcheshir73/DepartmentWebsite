namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAccess : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accesses", "Operation", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accesses", "Operation", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
