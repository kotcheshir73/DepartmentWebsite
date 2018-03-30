namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableSoftwareRecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SoftwareRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MaterialTechnicalValueId = c.Guid(nullable: false),
                        SoftwareName = c.String(),
                        SoftwareKey = c.String(),
                        SoftwareK = c.String(),
                        ClaimNumber = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MaterialTechnicalValues", t => t.MaterialTechnicalValueId, cascadeDelete: true)
                .Index(t => t.MaterialTechnicalValueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SoftwareRecords", "MaterialTechnicalValueId", "dbo.MaterialTechnicalValues");
            DropIndex("dbo.SoftwareRecords", new[] { "MaterialTechnicalValueId" });
            DropTable("dbo.SoftwareRecords");
        }
    }
}
