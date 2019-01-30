namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_LecturerWorkload : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LecturerWorkloads",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LecturerId = c.Guid(nullable: false),
                        AcademicYearId = c.Guid(nullable: false),
                        Workload = c.Double(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId, cascadeDelete: true)
                .Index(t => t.LecturerId)
                .Index(t => t.AcademicYearId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LecturerWorkloads", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.LecturerWorkloads", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.LecturerWorkloads", new[] { "AcademicYearId" });
            DropIndex("dbo.LecturerWorkloads", new[] { "LecturerId" });
            DropTable("dbo.LecturerWorkloads");
        }
    }
}
