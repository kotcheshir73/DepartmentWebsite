namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClassroomSetFieldNotUseInSchedule : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classrooms", "NotUseInSchedule", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classrooms", "NotUseInSchedule");
        }
    }
}
