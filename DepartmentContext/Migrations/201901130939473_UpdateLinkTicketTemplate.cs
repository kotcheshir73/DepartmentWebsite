namespace DepartmentContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLinkTicketTemplate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExaminationTemplates", "TicketTemplateId", "dbo.TicketTemplates");
            DropIndex("dbo.ExaminationTemplates", new[] { "TicketTemplateId" });
            AddColumn("dbo.TicketTemplates", "ExaminationTemplateId", c => c.Guid(nullable: false));
            CreateIndex("dbo.TicketTemplates", "ExaminationTemplateId");
            AddForeignKey("dbo.TicketTemplates", "ExaminationTemplateId", "dbo.ExaminationTemplates", "Id", cascadeDelete: true);
            DropColumn("dbo.ExaminationTemplates", "TicketTemplateId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExaminationTemplates", "TicketTemplateId", c => c.Guid());
            DropForeignKey("dbo.TicketTemplates", "ExaminationTemplateId", "dbo.ExaminationTemplates");
            DropIndex("dbo.TicketTemplates", new[] { "ExaminationTemplateId" });
            DropColumn("dbo.TicketTemplates", "ExaminationTemplateId");
            CreateIndex("dbo.ExaminationTemplates", "TicketTemplateId");
            AddForeignKey("dbo.ExaminationTemplates", "TicketTemplateId", "dbo.TicketTemplates", "Id");
        }
    }
}
