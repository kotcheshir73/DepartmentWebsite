namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveKindOfLoad : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.KindOfLoads");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.KindOfLoads",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        KindOfLoadName = c.String(nullable: false, maxLength: 100),
                        KindOfLoadOrder = c.Int(nullable: false),
                        AttributeName = c.String(maxLength: 10),
                        KindOfLoadBlueAsteriskName = c.String(maxLength: 100),
                        KindOfLoadBlueAsteriskAttributeName = c.String(maxLength: 100),
                        KindOfLoadBlueAsteriskPracticName = c.String(maxLength: 100),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
