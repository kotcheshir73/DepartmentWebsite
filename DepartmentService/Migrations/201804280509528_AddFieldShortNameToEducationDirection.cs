namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldShortNameToEducationDirection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EducationDirections", "ShortName", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EducationDirections", "ShortName");
        }
    }
}
