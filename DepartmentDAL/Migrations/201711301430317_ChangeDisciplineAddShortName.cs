namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDisciplineAddShortName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Disciplines", "DisciplineShortName", c => c.String(maxLength: 10));
            AlterColumn("dbo.Disciplines", "DisciplineName", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Disciplines", "DisciplineName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Disciplines", "DisciplineShortName");
        }
    }
}
