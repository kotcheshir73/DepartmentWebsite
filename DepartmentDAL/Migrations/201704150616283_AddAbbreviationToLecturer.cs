namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAbbreviationToLecturer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lecturers", "Abbreviation", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lecturers", "Abbreviation");
        }
    }
}
