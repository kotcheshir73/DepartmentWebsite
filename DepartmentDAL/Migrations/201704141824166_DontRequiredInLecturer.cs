namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DontRequiredInLecturer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lecturers", "HomeNumber", c => c.String(maxLength: 50));
            AlterColumn("dbo.Lecturers", "Rank", c => c.String(maxLength: 50));
            AlterColumn("dbo.Lecturers", "Description", c => c.String());
            AlterColumn("dbo.Lecturers", "Photo", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lecturers", "Photo", c => c.Binary(nullable: false));
            AlterColumn("dbo.Lecturers", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Lecturers", "Rank", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Lecturers", "HomeNumber", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
