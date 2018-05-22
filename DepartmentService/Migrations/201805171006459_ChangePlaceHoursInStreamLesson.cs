namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePlaceHoursInStreamLesson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StreamLessons", "StreamLessonHours", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.StreamLessonRecords", "Hours");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StreamLessonRecords", "Hours", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.StreamLessons", "StreamLessonHours");
        }
    }
}
