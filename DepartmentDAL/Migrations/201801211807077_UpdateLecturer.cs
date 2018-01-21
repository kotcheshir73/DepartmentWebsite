namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLecturer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lecturers", "Post", c => c.Int(nullable: false));
            AlterColumn("dbo.Lecturers", "Rank", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lecturers", "Rank", c => c.String(maxLength: 50));
            AlterColumn("dbo.Lecturers", "Post", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
