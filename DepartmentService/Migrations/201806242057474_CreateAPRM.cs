namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAPRM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicPlanRecordMissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LecturerId = c.Guid(nullable: false),
                        AcademicPlanRecordElementId = c.Guid(nullable: false),
                        Hours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicPlanRecordElements", t => t.AcademicPlanRecordElementId, cascadeDelete: true)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId, cascadeDelete: true)
                .Index(t => t.LecturerId)
                .Index(t => t.AcademicPlanRecordElementId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AcademicPlanRecordMissions", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.AcademicPlanRecordMissions", "AcademicPlanRecordElementId", "dbo.AcademicPlanRecordElements");
            DropIndex("dbo.AcademicPlanRecordMissions", new[] { "AcademicPlanRecordElementId" });
            DropIndex("dbo.AcademicPlanRecordMissions", new[] { "LecturerId" });
            DropTable("dbo.AcademicPlanRecordMissions");
        }
    }
}
