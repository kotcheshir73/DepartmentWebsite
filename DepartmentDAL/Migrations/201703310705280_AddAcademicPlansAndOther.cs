namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAcademicPlansAndOther : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicPlanRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AcademicPlanId = c.Long(nullable: false),
                        DisciplineId = c.Long(nullable: false),
                        KindOfLoadId = c.Long(nullable: false),
                        Semester = c.Int(nullable: false),
                        Hours = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicPlans", t => t.AcademicPlanId, cascadeDelete: true)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId, cascadeDelete: true)
                .ForeignKey("dbo.KindOfLoads", t => t.KindOfLoadId, cascadeDelete: true)
                .Index(t => t.AcademicPlanId)
                .Index(t => t.DisciplineId)
                .Index(t => t.KindOfLoadId);
            
            CreateTable(
                "dbo.AcademicPlans",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AcademicYear = c.String(nullable: false, maxLength: 10),
                        AcademicLevel = c.Int(nullable: false),
                        AcademicCourses = c.Int(nullable: false),
                        EducationDirectionId = c.Long(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationDirections", t => t.EducationDirectionId, cascadeDelete: true)
                .Index(t => t.EducationDirectionId);
            
            CreateTable(
                "dbo.Disciplines",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DisciplineName = c.String(nullable: false, maxLength: 100),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KindOfLoads",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        KindOfLoadName = c.String(nullable: false, maxLength: 20),
                        KindOfLoadType = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AcademicPlanRecords", "KindOfLoadId", "dbo.KindOfLoads");
            DropForeignKey("dbo.AcademicPlanRecords", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.AcademicPlans", "EducationDirectionId", "dbo.EducationDirections");
            DropForeignKey("dbo.AcademicPlanRecords", "AcademicPlanId", "dbo.AcademicPlans");
            DropIndex("dbo.AcademicPlans", new[] { "EducationDirectionId" });
            DropIndex("dbo.AcademicPlanRecords", new[] { "KindOfLoadId" });
            DropIndex("dbo.AcademicPlanRecords", new[] { "DisciplineId" });
            DropIndex("dbo.AcademicPlanRecords", new[] { "AcademicPlanId" });
            DropTable("dbo.KindOfLoads");
            DropTable("dbo.Disciplines");
            DropTable("dbo.AcademicPlans");
            DropTable("dbo.AcademicPlanRecords");
        }
    }
}
