namespace DepartmentContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTicketTemplates2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TicketTemplates", "ExaminationTemplateId", "dbo.ExaminationTemplates");
            DropIndex("dbo.TicketTemplates", new[] { "ExaminationTemplateId" });
            AlterColumn("dbo.TicketTemplates", "ExaminationTemplateId", c => c.Guid());
            CreateIndex("dbo.TicketTemplates", "ExaminationTemplateId");
            AddForeignKey("dbo.TicketTemplates", "ExaminationTemplateId", "dbo.ExaminationTemplates", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketTemplates", "ExaminationTemplateId", "dbo.ExaminationTemplates");
            DropIndex("dbo.TicketTemplates", new[] { "ExaminationTemplateId" });
            AlterColumn("dbo.TicketTemplates", "ExaminationTemplateId", c => c.Guid(nullable: false));
            CreateIndex("dbo.TicketTemplates", "ExaminationTemplateId");
            AddForeignKey("dbo.TicketTemplates", "ExaminationTemplateId", "dbo.ExaminationTemplates", "Id", cascadeDelete: true);
        }
    }
}
