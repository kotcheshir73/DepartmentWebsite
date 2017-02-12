namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSemesterRecord3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SemesterRecords", "SeasonDatesId", c => c.Long(nullable: false));
            CreateIndex("dbo.SemesterRecords", "SeasonDatesId");
            AddForeignKey("dbo.SemesterRecords", "SeasonDatesId", "dbo.SeasonDates", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SemesterRecords", "SeasonDatesId", "dbo.SeasonDates");
            DropIndex("dbo.SemesterRecords", new[] { "SeasonDatesId" });
            DropColumn("dbo.SemesterRecords", "SeasonDatesId");
        }
    }
}
