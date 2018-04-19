namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AcademicPlanRecords", "KindOfLoadId", "dbo.KindOfLoads");
            DropIndex("dbo.AcademicPlanRecords", new[] { "KindOfLoadId" });
            CreateTable(
                "dbo.AcademicPlanRecordElements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AcademicPlanRecordId = c.Guid(nullable: false),
                        KindOfLoadId = c.Guid(nullable: false),
                        Hours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicPlanRecords", t => t.AcademicPlanRecordId, cascadeDelete: true)
                .ForeignKey("dbo.KindOfLoads", t => t.KindOfLoadId, cascadeDelete: true)
                .Index(t => t.AcademicPlanRecordId)
                .Index(t => t.KindOfLoadId);
            
            AddColumn("dbo.AcademicPlanRecords", "Zet", c => c.Int(nullable: false));
            AddColumn("dbo.TimeNorms", "NumKoef", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.TimeNorms", "KindOfLoadType", c => c.Int(nullable: false));
            AddColumn("dbo.TimeNorms", "TimeNormKoef", c => c.Int(nullable: false));
            AddColumn("dbo.KindOfLoads", "AttributeName", c => c.String(nullable: false, maxLength: 10));
            DropColumn("dbo.AcademicPlanRecords", "KindOfLoadId");
            DropColumn("dbo.AcademicPlanRecords", "Hours");
            DropColumn("dbo.TimeNorms", "Formula");
            DropColumn("dbo.KindOfLoads", "KindOfLoadType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KindOfLoads", "KindOfLoadType", c => c.Int(nullable: false));
            AddColumn("dbo.TimeNorms", "Formula", c => c.String());
            AddColumn("dbo.AcademicPlanRecords", "Hours", c => c.Int(nullable: false));
            AddColumn("dbo.AcademicPlanRecords", "KindOfLoadId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.AcademicPlanRecordElements", "KindOfLoadId", "dbo.KindOfLoads");
            DropForeignKey("dbo.AcademicPlanRecordElements", "AcademicPlanRecordId", "dbo.AcademicPlanRecords");
            DropIndex("dbo.AcademicPlanRecordElements", new[] { "KindOfLoadId" });
            DropIndex("dbo.AcademicPlanRecordElements", new[] { "AcademicPlanRecordId" });
            DropColumn("dbo.KindOfLoads", "AttributeName");
            DropColumn("dbo.TimeNorms", "TimeNormKoef");
            DropColumn("dbo.TimeNorms", "KindOfLoadType");
            DropColumn("dbo.TimeNorms", "NumKoef");
            DropColumn("dbo.AcademicPlanRecords", "Zet");
            DropTable("dbo.AcademicPlanRecordElements");
            CreateIndex("dbo.AcademicPlanRecords", "KindOfLoadId");
            AddForeignKey("dbo.AcademicPlanRecords", "KindOfLoadId", "dbo.KindOfLoads", "Id", cascadeDelete: true);
        }
    }
}
