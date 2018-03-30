namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaterialTechnicalValueGroupAndMaterialTechnicalValueRecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MaterialTechnicalValueGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GroupName = c.String(),
                        Order = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaterialTechnicalValueRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MaterialTechnicalValueId = c.Guid(nullable: false),
                        MaterialTechnicalValueGroupId = c.Guid(nullable: false),
                        FieldName = c.String(),
                        FieldValue = c.String(),
                        Order = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MaterialTechnicalValues", t => t.MaterialTechnicalValueId, cascadeDelete: true)
                .ForeignKey("dbo.MaterialTechnicalValueGroups", t => t.MaterialTechnicalValueGroupId, cascadeDelete: true)
                .Index(t => t.MaterialTechnicalValueId)
                .Index(t => t.MaterialTechnicalValueGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MaterialTechnicalValueRecords", "MaterialTechnicalValueGroupId", "dbo.MaterialTechnicalValueGroups");
            DropForeignKey("dbo.MaterialTechnicalValueRecords", "MaterialTechnicalValueId", "dbo.MaterialTechnicalValues");
            DropIndex("dbo.MaterialTechnicalValueRecords", new[] { "MaterialTechnicalValueGroupId" });
            DropIndex("dbo.MaterialTechnicalValueRecords", new[] { "MaterialTechnicalValueId" });
            DropTable("dbo.MaterialTechnicalValueRecords");
            DropTable("dbo.MaterialTechnicalValueGroups");
        }
    }
}
