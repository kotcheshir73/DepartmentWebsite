namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accesses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RoleId = c.Long(nullable: false),
                        Operation = c.String(nullable: false, maxLength: 100),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 20),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RoleId = c.Long(nullable: false),
                        StudentId = c.Long(),
                        LecturerId = c.Long(),
                        Login = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 20),
                        Avatar = c.Binary(nullable: false),
                        DateLastVisit = c.DateTime(nullable: false),
                        DateBanned = c.DateTime(),
                        IsBanned = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.RoleId)
                .Index(t => t.StudentId)
                .Index(t => t.LecturerId);
            
            CreateTable(
                "dbo.Lecturers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 30),
                        Patronymic = c.String(nullable: false, maxLength: 30),
                        DateBirth = c.DateTime(nullable: false),
                        Address = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 150),
                        MobileNumber = c.String(nullable: false, maxLength: 50),
                        HomeNumber = c.String(nullable: false, maxLength: 50),
                        Post = c.String(nullable: false, maxLength: 50),
                        Rank = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false),
                        Photo = c.Binary(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        Caption = c.String(nullable: false, maxLength: 150),
                        Text = c.String(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StudentGroupId = c.Long(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 30),
                        Patronymic = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false),
                        Photo = c.Binary(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentGroups", t => t.StudentGroupId, cascadeDelete: true)
                .Index(t => t.StudentGroupId);
            
            CreateTable(
                "dbo.StudentGroups",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EducationDirectionId = c.Long(nullable: false),
                        GroupName = c.String(),
                        Kurs = c.Int(nullable: false),
                        GroupNumber = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationDirections", t => t.EducationDirectionId, cascadeDelete: true)
                .Index(t => t.EducationDirectionId);
            
            CreateTable(
                "dbo.EducationDirections",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Cipher = c.String(nullable: false, maxLength: 10),
                        ShortAbbrev = c.String(nullable: false, maxLength: 10),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Classrooms",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClassroomType = c.Int(nullable: false),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SemesterRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Week = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        Lesson = c.Int(nullable: false),
                        LessonDiscipline = c.String(),
                        LessonTeacher = c.String(),
                        StudentGroupId = c.Long(nullable: false),
                        ClassroomId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classrooms", t => t.ClassroomId)
                .ForeignKey("dbo.StudentGroups", t => t.StudentGroupId, cascadeDelete: true)
                .Index(t => t.StudentGroupId)
                .Index(t => t.ClassroomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SemesterRecords", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.SemesterRecords", "ClassroomId", "dbo.Classrooms");
            DropForeignKey("dbo.Users", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.StudentGroups", "EducationDirectionId", "dbo.EducationDirections");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.Accesses", "RoleId", "dbo.Roles");
            DropIndex("dbo.SemesterRecords", new[] { "ClassroomId" });
            DropIndex("dbo.SemesterRecords", new[] { "StudentGroupId" });
            DropIndex("dbo.StudentGroups", new[] { "EducationDirectionId" });
            DropIndex("dbo.Students", new[] { "StudentGroupId" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "LecturerId" });
            DropIndex("dbo.Users", new[] { "StudentId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Accesses", new[] { "RoleId" });
            DropTable("dbo.SemesterRecords");
            DropTable("dbo.Classrooms");
            DropTable("dbo.EducationDirections");
            DropTable("dbo.StudentGroups");
            DropTable("dbo.Students");
            DropTable("dbo.Messages");
            DropTable("dbo.Lecturers");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Accesses");
        }
    }
}
