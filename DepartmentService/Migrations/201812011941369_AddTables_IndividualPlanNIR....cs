namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTables_IndividualPlanNIR : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IndividualPlanNIRContractualWorks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LecturerId = c.Guid(nullable: false),
                        JobContent = c.String(),
                        Post = c.String(),
                        PlannedTerm = c.String(),
                        ReadyMark = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId, cascadeDelete: true)
                .Index(t => t.LecturerId);
            
            CreateTable(
                "dbo.IndividualPlanNIRScientificArticles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LecturerId = c.Guid(nullable: false),
                        Name = c.String(),
                        TypeOfPublication = c.String(),
                        Volume = c.String(),
                        Publishing = c.String(),
                        Year = c.String(),
                        Status = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId, cascadeDelete: true)
                .Index(t => t.LecturerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IndividualPlanNIRScientificArticles", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.IndividualPlanNIRContractualWorks", "LecturerId", "dbo.Lecturers");
            DropIndex("dbo.IndividualPlanNIRScientificArticles", new[] { "LecturerId" });
            DropIndex("dbo.IndividualPlanNIRContractualWorks", new[] { "LecturerId" });
            DropTable("dbo.IndividualPlanNIRScientificArticles");
            DropTable("dbo.IndividualPlanNIRContractualWorks");
        }
    }
}
