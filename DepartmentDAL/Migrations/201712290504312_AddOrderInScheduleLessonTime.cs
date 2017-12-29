namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderInScheduleLessonTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduleLessonTimes", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScheduleLessonTimes", "Order");
        }
    }
}
