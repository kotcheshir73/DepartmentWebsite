namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSoftwareTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Softwares",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SoftwareName = c.String(),
                        SoftwareDescription = c.String(),
                        SoftwareKey = c.String(),
                        SoftwareK = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SoftwareRecords", "SoftwareId", c => c.Guid(nullable: false));
            AddColumn("dbo.SoftwareRecords", "SetupDescription", c => c.String());
            CreateIndex("dbo.SoftwareRecords", "SoftwareId");
            AddForeignKey("dbo.SoftwareRecords", "SoftwareId", "dbo.Softwares", "Id", cascadeDelete: true);
            DropColumn("dbo.SoftwareRecords", "SoftwareName");
            DropColumn("dbo.SoftwareRecords", "SoftwareDescription");
            DropColumn("dbo.SoftwareRecords", "SoftwareKey");
            DropColumn("dbo.SoftwareRecords", "SoftwareK");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SoftwareRecords", "SoftwareK", c => c.String());
            AddColumn("dbo.SoftwareRecords", "SoftwareKey", c => c.String());
            AddColumn("dbo.SoftwareRecords", "SoftwareDescription", c => c.String());
            AddColumn("dbo.SoftwareRecords", "SoftwareName", c => c.String());
            DropForeignKey("dbo.SoftwareRecords", "SoftwareId", "dbo.Softwares");
            DropIndex("dbo.SoftwareRecords", new[] { "SoftwareId" });
            DropColumn("dbo.SoftwareRecords", "SetupDescription");
            DropColumn("dbo.SoftwareRecords", "SoftwareId");
            DropTable("dbo.Softwares");
        }
    }
}
