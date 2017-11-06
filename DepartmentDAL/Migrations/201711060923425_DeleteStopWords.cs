namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteStopWords : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ScheduleStopWords");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ScheduleStopWords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StopWord = c.String(),
                        StopWordReplace = c.String(),
                        StopWordType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
