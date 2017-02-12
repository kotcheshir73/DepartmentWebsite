namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStreamingLesson : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StreamingLessons",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IncomingGroups = c.String(),
                        StreamName = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StreamingLessons");
        }
    }
}
