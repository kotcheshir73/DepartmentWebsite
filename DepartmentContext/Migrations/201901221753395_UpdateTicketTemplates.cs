namespace DepartmentContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTicketTemplates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TicketTemplates", "TicketTemplateBodyId", "dbo.TicketTemplateBodies");
            DropIndex("dbo.TicketTemplates", new[] { "TicketTemplateBodyId" });
            AddColumn("dbo.TicketTemplateBodies", "TicketTemplateId", c => c.Guid());
            CreateIndex("dbo.TicketTemplateBodies", "TicketTemplateId");
            AddForeignKey("dbo.TicketTemplateBodies", "TicketTemplateId", "dbo.TicketTemplates", "Id");
            DropColumn("dbo.TicketTemplates", "TicketTemplateBodyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TicketTemplates", "TicketTemplateBodyId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.TicketTemplateBodies", "TicketTemplateId", "dbo.TicketTemplates");
            DropIndex("dbo.TicketTemplateBodies", new[] { "TicketTemplateId" });
            DropColumn("dbo.TicketTemplateBodies", "TicketTemplateId");
            CreateIndex("dbo.TicketTemplates", "TicketTemplateBodyId");
            AddForeignKey("dbo.TicketTemplates", "TicketTemplateBodyId", "dbo.TicketTemplateBodies", "Id", cascadeDelete: true);
        }
    }
}
