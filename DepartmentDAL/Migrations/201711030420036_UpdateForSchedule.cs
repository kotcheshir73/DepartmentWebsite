namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateForSchedule : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConsultationRecords", "NotParseRecord", c => c.String());
            AddColumn("dbo.ExaminationRecords", "NotParseRecord", c => c.String());
            AddColumn("dbo.OffsetRecords", "NotParseRecord", c => c.String());
            AddColumn("dbo.ScheduleStopWords", "StopWordReplace", c => c.String());
            AddColumn("dbo.SemesterRecords", "NotParseRecord", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SemesterRecords", "NotParseRecord");
            DropColumn("dbo.ScheduleStopWords", "StopWordReplace");
            DropColumn("dbo.OffsetRecords", "NotParseRecord");
            DropColumn("dbo.ExaminationRecords", "NotParseRecord");
            DropColumn("dbo.ConsultationRecords", "NotParseRecord");
        }
    }
}
