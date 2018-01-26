namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLecturerPosts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LecturerPosts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PostTitle = c.String(),
                        Hours = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Lecturers", "LecturerPostId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Lecturers", "LecturerPostId");
            AddForeignKey("dbo.Lecturers", "LecturerPostId", "dbo.LecturerPosts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lecturers", "LecturerPostId", "dbo.LecturerPosts");
            DropIndex("dbo.Lecturers", new[] { "LecturerPostId" });
            DropColumn("dbo.Lecturers", "LecturerPostId");
            DropTable("dbo.LecturerPosts");
        }
    }
}
