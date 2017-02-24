namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableScheduleLessonTimes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScheduleLessonTimes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        DateBeginLesson = c.DateTime(nullable: false),
                        DateEndLesson = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ScheduleLessonTimes");
        }
    }
}
