namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateForBlueAsteriskThirdPart : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Disciplines", "DisciplineBlueAsteriskName", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Disciplines", "DisciplineBlueAsteriskName", c => c.String(maxLength: 20));
        }
    }
}
