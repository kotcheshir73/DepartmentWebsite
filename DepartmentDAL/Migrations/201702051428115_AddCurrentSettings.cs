namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrentSettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrentSettings",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CurrentSettings");
        }
    }
}
