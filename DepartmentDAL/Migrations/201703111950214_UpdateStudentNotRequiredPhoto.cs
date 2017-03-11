namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStudentNotRequiredPhoto : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Photo", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Photo", c => c.Binary(nullable: false));
        }
    }
}
