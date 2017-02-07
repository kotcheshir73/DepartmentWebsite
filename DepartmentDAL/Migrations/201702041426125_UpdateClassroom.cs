namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClassroom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classrooms", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Classrooms", "DateDelete", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classrooms", "DateDelete");
            DropColumn("dbo.Classrooms", "IsDeleted");
        }
    }
}
