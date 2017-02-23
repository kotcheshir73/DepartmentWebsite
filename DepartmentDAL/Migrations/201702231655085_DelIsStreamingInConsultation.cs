namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelIsStreamingInConsultation : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ConsultationRecords", "IsStreaming");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConsultationRecords", "IsStreaming", c => c.Boolean(nullable: false));
        }
    }
}
