namespace DepartmentContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateExaminationTemplateBlock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExaminationTemplateBlocks", "IsCombine", c => c.Boolean(nullable: false));
            AddColumn("dbo.ExaminationTemplateBlocks", "CombineBlocks", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExaminationTemplateBlocks", "CombineBlocks");
            DropColumn("dbo.ExaminationTemplateBlocks", "IsCombine");
        }
    }
}
