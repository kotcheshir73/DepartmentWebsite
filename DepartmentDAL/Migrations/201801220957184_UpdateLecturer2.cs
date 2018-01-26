namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLecturer2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lecturers", "Rank2", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lecturers", "Rank2");
        }
    }
}
