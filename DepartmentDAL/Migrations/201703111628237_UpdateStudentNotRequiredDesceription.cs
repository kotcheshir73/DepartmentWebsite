namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStudentNotRequiredDesceription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Description", c => c.String(nullable: false));
        }
    }
}
