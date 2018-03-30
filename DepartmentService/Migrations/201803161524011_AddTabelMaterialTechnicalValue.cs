namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTabelMaterialTechnicalValue : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MaterialTechnicalValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ClassroomId = c.Guid(nullable: false),
                        InventoryNumber = c.String(nullable: false, maxLength: 50),
                        FullName = c.String(nullable: false, maxLength: 200),
                        Description = c.String(),
                        Location = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeleteReason = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classrooms", t => t.ClassroomId, cascadeDelete: true)
                .Index(t => t.ClassroomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MaterialTechnicalValues", "ClassroomId", "dbo.Classrooms");
            DropIndex("dbo.MaterialTechnicalValues", new[] { "ClassroomId" });
            DropTable("dbo.MaterialTechnicalValues");
        }
    }
}
