namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDisciplineAddShortName2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Disciplines", "DisciplineShortName", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Disciplines", "DisciplineShortName", c => c.String(maxLength: 10));
        }
    }
}
