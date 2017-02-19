namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableScheduleStopWords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScheduleStopWords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StopWord = c.String(),
                        StopWordType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ScheduleStopWords");
        }
    }
}
