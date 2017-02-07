namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDirectionAndGroup : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudentGroups", "GroupName", c => c.String(maxLength: 20));
            DropColumn("dbo.StudentGroups", "GroupNumber");
            DropColumn("dbo.EducationDirections", "ShortAbbrev");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EducationDirections", "ShortAbbrev", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.StudentGroups", "GroupNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.StudentGroups", "GroupName", c => c.String());
        }
    }
}
