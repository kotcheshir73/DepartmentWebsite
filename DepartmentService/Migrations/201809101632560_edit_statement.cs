namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_statement : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Statements", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Statements", "Date", c => c.DateTime(nullable: false));
        }
    }
}
