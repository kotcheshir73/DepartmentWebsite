namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStudentNotRequiredPatronumic : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Patronymic", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Patronymic", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
