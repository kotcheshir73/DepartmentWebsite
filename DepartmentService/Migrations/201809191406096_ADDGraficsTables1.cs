namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDGraficsTables1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GraficClassrooms", "ClassroomDescription", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GraficClassrooms", "ClassroomDescription", c => c.String(nullable: false));
        }
    }
}
