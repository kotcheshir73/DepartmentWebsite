namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTable_StatementRecordExtended : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatementRecordExtendeds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StatementRecordId = c.Guid(nullable: false),
                        Name = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StatementRecords", t => t.StatementRecordId, cascadeDelete: true)
                .Index(t => t.StatementRecordId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StatementRecordExtendeds", "StatementRecordId", "dbo.StatementRecords");
            DropIndex("dbo.StatementRecordExtendeds", new[] { "StatementRecordId" });
            DropTable("dbo.StatementRecordExtendeds");
        }
    }
}
