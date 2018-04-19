namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotRequerdInTimeNormMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TimeNorms", "Hours", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TimeNorms", "NumKoef", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeNorms", "NumKoef", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TimeNorms", "Hours", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
