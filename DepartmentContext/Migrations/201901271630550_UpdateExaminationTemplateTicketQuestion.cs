namespace DepartmentContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateExaminationTemplateTicketQuestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExaminationTemplateTicketQuestions", "ExaminationTemplateBlockId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExaminationTemplateTicketQuestions", "ExaminationTemplateBlockId");
        }
    }
}
