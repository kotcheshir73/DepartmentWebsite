namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldFormulaInTimeNorm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeNorms", "Formula", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeNorms", "Formula");
        }
    }
}
