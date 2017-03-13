namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentHistories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StudentId = c.String(maxLength: 10),
                        DateCreate = c.DateTime(nullable: false),
                        TextMessage = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentHistories", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentHistories", new[] { "StudentId" });
            DropTable("dbo.StudentHistories");
        }
    }
}
