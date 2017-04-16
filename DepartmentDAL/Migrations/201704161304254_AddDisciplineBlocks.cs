namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDisciplineBlocks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DisciplineBlocks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Disciplines", "DisciplineBlockId", c => c.Long(nullable: false));
            CreateIndex("dbo.Disciplines", "DisciplineBlockId");
            AddForeignKey("dbo.Disciplines", "DisciplineBlockId", "dbo.DisciplineBlocks", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Disciplines", "DisciplineBlockId", "dbo.DisciplineBlocks");
            DropIndex("dbo.Disciplines", new[] { "DisciplineBlockId" });
            DropColumn("dbo.Disciplines", "DisciplineBlockId");
            DropTable("dbo.DisciplineBlocks");
        }
    }
}
