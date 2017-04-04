namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimeNorms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeNorms",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        KindOfLoadId = c.Long(nullable: false),
                        ParentTimeNormId = c.Long(),
                        Hours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KindOfLoads", t => t.KindOfLoadId, cascadeDelete: true)
                .ForeignKey("dbo.TimeNorms", t => t.ParentTimeNormId)
                .Index(t => t.KindOfLoadId)
                .Index(t => t.ParentTimeNormId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeNorms", "ParentTimeNormId", "dbo.TimeNorms");
            DropForeignKey("dbo.TimeNorms", "KindOfLoadId", "dbo.KindOfLoads");
            DropIndex("dbo.TimeNorms", new[] { "ParentTimeNormId" });
            DropIndex("dbo.TimeNorms", new[] { "KindOfLoadId" });
            DropTable("dbo.TimeNorms");
        }
    }
}
