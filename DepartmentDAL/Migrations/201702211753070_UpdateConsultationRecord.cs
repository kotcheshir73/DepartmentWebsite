namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateConsultationRecord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConsultationRecords", "IsStreaming", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConsultationRecords", "IsStreaming");
        }
    }
}
