namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContingent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contingents",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AcademicYearId = c.Long(nullable: false),
                        StudentGroupId = c.Long(nullable: false),
                        CountStudetns = c.Int(nullable: false),
                        CountSubgroups = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .ForeignKey("dbo.StudentGroups", t => t.StudentGroupId, cascadeDelete: true)
                .Index(t => t.AcademicYearId)
                .Index(t => t.StudentGroupId);
            
            DropColumn("dbo.StudentGroups", "Capacity");
            DropColumn("dbo.StudentGroups", "SubgroupsCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentGroups", "SubgroupsCount", c => c.Int(nullable: false));
            AddColumn("dbo.StudentGroups", "Capacity", c => c.Int(nullable: false));
            DropForeignKey("dbo.Contingents", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.Contingents", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.Contingents", new[] { "StudentGroupId" });
            DropIndex("dbo.Contingents", new[] { "AcademicYearId" });
            DropTable("dbo.Contingents");
        }
    }
}
