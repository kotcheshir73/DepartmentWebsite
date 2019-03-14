namespace DepartmentContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateExaminations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExaminationTemplateBlocks", "QuestionTagInTemplate", c => c.String());
            AddColumn("dbo.ExaminationTemplates", "ExaminationTemplateName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExaminationTemplates", "ExaminationTemplateName");
            DropColumn("dbo.ExaminationTemplateBlocks", "QuestionTagInTemplate");
        }
    }
}
