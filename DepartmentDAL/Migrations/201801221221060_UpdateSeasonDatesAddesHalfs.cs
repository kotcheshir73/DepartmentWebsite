namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSeasonDatesAddesHalfs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeasonDates", "DateBeginFirstHalfSemester", c => c.DateTime(nullable: false));
            AddColumn("dbo.SeasonDates", "DateEndFirstHalfSemester", c => c.DateTime(nullable: false));
            AddColumn("dbo.SeasonDates", "DateBeginSecondHalfSemester", c => c.DateTime(nullable: false));
            AddColumn("dbo.SeasonDates", "DateEndSecondHalfSemester", c => c.DateTime(nullable: false));
            DropColumn("dbo.SeasonDates", "DateBeginSemester");
            DropColumn("dbo.SeasonDates", "DateEndSemester");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SeasonDates", "DateEndSemester", c => c.DateTime(nullable: false));
            AddColumn("dbo.SeasonDates", "DateBeginSemester", c => c.DateTime(nullable: false));
            DropColumn("dbo.SeasonDates", "DateEndSecondHalfSemester");
            DropColumn("dbo.SeasonDates", "DateBeginSecondHalfSemester");
            DropColumn("dbo.SeasonDates", "DateEndFirstHalfSemester");
            DropColumn("dbo.SeasonDates", "DateBeginFirstHalfSemester");
        }
    }
}
