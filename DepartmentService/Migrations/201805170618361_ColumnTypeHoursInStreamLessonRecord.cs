namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnTypeHoursInStreamLessonRecord : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StreamLessonRecords", "Hours", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StreamLessonRecords", "Hours", c => c.Int(nullable: false));
        }
    }
}
