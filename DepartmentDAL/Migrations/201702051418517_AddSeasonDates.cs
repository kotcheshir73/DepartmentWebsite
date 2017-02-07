namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSeasonDates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SeasonDates",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        DateBeginSemester = c.DateTime(nullable: false),
                        DateEndSemester = c.DateTime(nullable: false),
                        DateBeginOffset = c.DateTime(nullable: false),
                        DateEndOffset = c.DateTime(nullable: false),
                        DateBeginExamination = c.DateTime(nullable: false),
                        DateEndExamination = c.DateTime(nullable: false),
                        DateBeginPractice = c.DateTime(),
                        DateEndPractice = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SeasonDates");
        }
    }
}
