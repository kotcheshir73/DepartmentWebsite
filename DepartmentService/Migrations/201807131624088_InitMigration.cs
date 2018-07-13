namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicPlanRecordElements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AcademicPlanRecordId = c.Guid(nullable: false),
                        TimeNormId = c.Guid(nullable: false),
                        PlanHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FactHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TimeNorms", t => t.TimeNormId, cascadeDelete: true)
                .ForeignKey("dbo.AcademicPlanRecords", t => t.AcademicPlanRecordId, cascadeDelete: true)
                .Index(t => t.AcademicPlanRecordId)
                .Index(t => t.TimeNormId);
            
            CreateTable(
                "dbo.AcademicPlanRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AcademicPlanId = c.Guid(nullable: false),
                        DisciplineId = c.Guid(nullable: false),
                        ContingentId = c.Guid(),
                        Semester = c.Int(),
                        Zet = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicPlans", t => t.AcademicPlanId, cascadeDelete: true)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId, cascadeDelete: true)
                .ForeignKey("dbo.Contingents", t => t.ContingentId)
                .Index(t => t.AcademicPlanId)
                .Index(t => t.DisciplineId)
                .Index(t => t.ContingentId);
            
            CreateTable(
                "dbo.AcademicPlans",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AcademicYearId = c.Guid(nullable: false),
                        EducationDirectionId = c.Guid(),
                        AcademicLevel = c.Int(nullable: false),
                        AcademicCourses = c.Int(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .ForeignKey("dbo.EducationDirections", t => t.EducationDirectionId)
                .Index(t => t.AcademicYearId)
                .Index(t => t.EducationDirectionId);
            
            CreateTable(
                "dbo.AcademicYears",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 10),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SeasonDates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AcademicYearId = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        DateBeginFirstHalfSemester = c.DateTime(nullable: false),
                        DateEndFirstHalfSemester = c.DateTime(nullable: false),
                        DateBeginSecondHalfSemester = c.DateTime(nullable: false),
                        DateEndSecondHalfSemester = c.DateTime(nullable: false),
                        DateBeginOffset = c.DateTime(nullable: false),
                        DateEndOffset = c.DateTime(nullable: false),
                        DateBeginExamination = c.DateTime(nullable: false),
                        DateEndExamination = c.DateTime(nullable: false),
                        DateBeginPractice = c.DateTime(),
                        DateEndPractice = c.DateTime(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .Index(t => t.AcademicYearId);
            
            CreateTable(
                "dbo.StreamLessons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AcademicYearId = c.Guid(nullable: false),
                        StreamLessonName = c.String(),
                        StreamLessonHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .Index(t => t.AcademicYearId);
            
            CreateTable(
                "dbo.StreamLessonRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StreamLessonId = c.Guid(nullable: false),
                        AcademicPlanRecordElementId = c.Guid(nullable: false),
                        IsMain = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicPlanRecordElements", t => t.AcademicPlanRecordElementId, cascadeDelete: false)
                .ForeignKey("dbo.StreamLessons", t => t.StreamLessonId, cascadeDelete: true)
                .Index(t => t.StreamLessonId)
                .Index(t => t.AcademicPlanRecordElementId);
            
            CreateTable(
                "dbo.TimeNorms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AcademicYearId = c.Guid(nullable: false),
                        DisciplineBlockId = c.Guid(nullable: false),
                        TimeNormName = c.String(nullable: false, maxLength: 50),
                        TimeNormShortName = c.String(nullable: false, maxLength: 5),
                        TimeNormOrder = c.Int(nullable: false),
                        TimeNormAcademicLevel = c.Int(),
                        KindOfLoadName = c.String(nullable: false, maxLength: 100),
                        KindOfLoadAttributeName = c.String(maxLength: 10),
                        KindOfLoadBlueAsteriskName = c.String(maxLength: 100),
                        KindOfLoadBlueAsteriskAttributeName = c.String(maxLength: 100),
                        KindOfLoadBlueAsteriskPracticName = c.String(maxLength: 100),
                        KindOfLoadType = c.Int(nullable: false),
                        Hours = c.Decimal(precision: 18, scale: 2),
                        NumKoef = c.Decimal(precision: 18, scale: 2),
                        TimeNormKoef = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: false)
                .ForeignKey("dbo.DisciplineBlocks", t => t.DisciplineBlockId, cascadeDelete: true)
                .Index(t => t.AcademicYearId)
                .Index(t => t.DisciplineBlockId);
            
            CreateTable(
                "dbo.DisciplineBlocks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        DisciplineBlockBlueAsteriskName = c.String(maxLength: 20),
                        DisciplineBlockUseForGrouping = c.Boolean(nullable: false),
                        DisciplineBlockOrder = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Disciplines",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineBlockId = c.Guid(nullable: false),
                        DisciplineParentId = c.Guid(),
                        IsParent = c.Boolean(nullable: false),
                        DisciplineName = c.String(nullable: false, maxLength: 200),
                        DisciplineShortName = c.String(maxLength: 20),
                        DisciplineBlueAsteriskName = c.String(maxLength: 200),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineBlocks", t => t.DisciplineBlockId, cascadeDelete: false)
                .Index(t => t.DisciplineBlockId);
            
            CreateTable(
                "dbo.DisciplineLessons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineId = c.Guid(nullable: false),
                        LessonType = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        Order = c.Int(nullable: false),
                        DisciplineLessonFile = c.Binary(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId, cascadeDelete: true)
                .Index(t => t.DisciplineId);
            
            CreateTable(
                "dbo.DisciplineLessonStudentRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineLessonId = c.Guid(nullable: false),
                        StudentId = c.Guid(nullable: false),
                        Comment = c.String(),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineLessons", t => t.DisciplineLessonId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.DisciplineLessonId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NumberOfBook = c.String(nullable: false, maxLength: 10),
                        StudentGroupId = c.Guid(),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 30),
                        Patronymic = c.String(maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 150),
                        StudentState = c.Int(nullable: false),
                        Description = c.String(),
                        Photo = c.Binary(),
                        IsSteward = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentGroups", t => t.StudentGroupId)
                .Index(t => t.StudentGroupId);
            
            CreateTable(
                "dbo.DisciplineLessonTaskStudentRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineLessonTaskId = c.Guid(nullable: false),
                        StudentId = c.Guid(nullable: false),
                        Result = c.Int(nullable: false),
                        Comment = c.String(),
                        Score = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineLessonTasks", t => t.DisciplineLessonTaskId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.DisciplineLessonTaskId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.DisciplineLessonTasks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineLessonId = c.Guid(nullable: false),
                        VariantNumber = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        MaxBall = c.Decimal(precision: 18, scale: 2),
                        Description = c.String(),
                        Image = c.Binary(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineLessons", t => t.DisciplineLessonId, cascadeDelete: true)
                .Index(t => t.DisciplineLessonId);
            
            CreateTable(
                "dbo.DisciplineStudentRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineId = c.Guid(nullable: false),
                        StudentId = c.Guid(nullable: false),
                        Variant = c.Int(nullable: false),
                        SubGroup = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.DisciplineId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.StudentGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EducationDirectionId = c.Guid(nullable: false),
                        CuratorId = c.Guid(),
                        GroupName = c.String(nullable: false, maxLength: 20),
                        Course = c.Int(nullable: false),
                        StewardName = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lecturers", t => t.CuratorId)
                .ForeignKey("dbo.EducationDirections", t => t.EducationDirectionId, cascadeDelete: true)
                .Index(t => t.EducationDirectionId)
                .Index(t => t.CuratorId);
            
            CreateTable(
                "dbo.Lecturers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LecturerPostId = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 30),
                        Patronymic = c.String(nullable: false, maxLength: 30),
                        Abbreviation = c.String(maxLength: 10),
                        DateBirth = c.DateTime(nullable: false),
                        Address = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 150),
                        MobileNumber = c.String(nullable: false, maxLength: 50),
                        HomeNumber = c.String(maxLength: 50),
                        Post = c.Int(nullable: false),
                        Rank = c.Int(nullable: false),
                        Rank2 = c.Int(nullable: false),
                        Description = c.String(),
                        Photo = c.Binary(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LecturerPosts", t => t.LecturerPostId, cascadeDelete: true)
                .Index(t => t.LecturerPostId);
            
            CreateTable(
                "dbo.AcademicPlanRecordMissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LecturerId = c.Guid(nullable: false),
                        AcademicPlanRecordElementId = c.Guid(nullable: false),
                        Hours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicPlanRecordElements", t => t.AcademicPlanRecordElementId, cascadeDelete: true)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId, cascadeDelete: true)
                .Index(t => t.LecturerId)
                .Index(t => t.AcademicPlanRecordElementId);
            
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
            
            CreateTable(
                "dbo.EducationDirections",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Cipher = c.String(nullable: false, maxLength: 10),
                        ShortName = c.String(nullable: false, maxLength: 10),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contingents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AcademicYearId = c.Guid(nullable: false),
                        EducationDirectionId = c.Guid(nullable: false),
                        ContingentName = c.String(nullable: false),
                        Course = c.Int(nullable: false),
                        CountGroups = c.Int(nullable: false),
                        CountStudetns = c.Int(nullable: false),
                        CountSubgroups = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .ForeignKey("dbo.EducationDirections", t => t.EducationDirectionId, cascadeDelete: true)
                .Index(t => t.AcademicYearId)
                .Index(t => t.EducationDirectionId);
            
            CreateTable(
                "dbo.StudentHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StudentId = c.Guid(nullable: false),
                        TextMessage = c.String(nullable: false, maxLength: 150),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Accesses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                        Operation = c.Int(nullable: false),
                        AccessType = c.Int(nullable: false),
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
                        Id = c.Guid(nullable: false),
                        RoleName = c.String(nullable: false, maxLength: 20),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StudentId = c.Guid(),
                        LecturerId = c.Guid(),
                        Login = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false),
                        Avatar = c.Binary(),
                        DateLastVisit = c.DateTime(),
                        DateBanned = c.DateTime(),
                        IsBanned = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.LecturerId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
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
                "dbo.Classrooms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(),
                        ClassroomType = c.Int(nullable: false),
                        Capacity = c.Int(nullable: false),
                        NotUseInSchedule = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConsultationRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateConsultation = c.DateTime(nullable: false),
                        SeasonDatesId = c.Guid(nullable: false),
                        ClassroomId = c.Guid(),
                        StudentGroupId = c.Guid(),
                        LecturerId = c.Guid(),
                        DisciplineId = c.Guid(),
                        NotParseRecord = c.String(),
                        LessonDiscipline = c.String(),
                        LessonLecturer = c.String(),
                        LessonGroup = c.String(),
                        LessonClassroom = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classrooms", t => t.ClassroomId)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId)
                .ForeignKey("dbo.SeasonDates", t => t.SeasonDatesId, cascadeDelete: true)
                .ForeignKey("dbo.StudentGroups", t => t.StudentGroupId)
                .Index(t => t.SeasonDatesId)
                .Index(t => t.ClassroomId)
                .Index(t => t.StudentGroupId)
                .Index(t => t.LecturerId)
                .Index(t => t.DisciplineId);
            
            CreateTable(
                "dbo.CurrentSettings",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
            CreateTable(
                "dbo.ExaminationRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ConsultationClassroomId = c.Guid(),
                        DateConsultation = c.DateTime(nullable: false),
                        DateExamination = c.DateTime(nullable: false),
                        LessonConsultationClassroom = c.String(),
                        SeasonDatesId = c.Guid(nullable: false),
                        ClassroomId = c.Guid(),
                        StudentGroupId = c.Guid(),
                        LecturerId = c.Guid(),
                        DisciplineId = c.Guid(),
                        NotParseRecord = c.String(),
                        LessonDiscipline = c.String(),
                        LessonLecturer = c.String(),
                        LessonGroup = c.String(),
                        LessonClassroom = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classrooms", t => t.ClassroomId)
                .ForeignKey("dbo.Classrooms", t => t.ConsultationClassroomId)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId)
                .ForeignKey("dbo.SeasonDates", t => t.SeasonDatesId, cascadeDelete: true)
                .ForeignKey("dbo.StudentGroups", t => t.StudentGroupId)
                .Index(t => t.ConsultationClassroomId)
                .Index(t => t.SeasonDatesId)
                .Index(t => t.ClassroomId)
                .Index(t => t.StudentGroupId)
                .Index(t => t.LecturerId)
                .Index(t => t.DisciplineId);
            
            CreateTable(
                "dbo.MaterialTechnicalValueGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GroupName = c.String(),
                        Order = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaterialTechnicalValueRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MaterialTechnicalValueId = c.Guid(nullable: false),
                        MaterialTechnicalValueGroupId = c.Guid(nullable: false),
                        FieldName = c.String(),
                        FieldValue = c.String(),
                        Order = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MaterialTechnicalValues", t => t.MaterialTechnicalValueId, cascadeDelete: true)
                .ForeignKey("dbo.MaterialTechnicalValueGroups", t => t.MaterialTechnicalValueGroupId, cascadeDelete: true)
                .Index(t => t.MaterialTechnicalValueId)
                .Index(t => t.MaterialTechnicalValueGroupId);
            
            CreateTable(
                "dbo.MaterialTechnicalValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ClassroomId = c.Guid(nullable: false),
                        InventoryNumber = c.String(nullable: false, maxLength: 50),
                        FullName = c.String(nullable: false, maxLength: 200),
                        Description = c.String(),
                        Location = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeleteReason = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classrooms", t => t.ClassroomId, cascadeDelete: true)
                .Index(t => t.ClassroomId);
            
            CreateTable(
                "dbo.SoftwareRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MaterialTechnicalValueId = c.Guid(nullable: false),
                        SoftwareName = c.String(),
                        SoftwareDescription = c.String(),
                        SoftwareKey = c.String(),
                        SoftwareK = c.String(),
                        ClaimNumber = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MaterialTechnicalValues", t => t.MaterialTechnicalValueId, cascadeDelete: true)
                .Index(t => t.MaterialTechnicalValueId);
            
            CreateTable(
                "dbo.OffsetRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Week = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        Lesson = c.Int(nullable: false),
                        SeasonDatesId = c.Guid(nullable: false),
                        ClassroomId = c.Guid(),
                        StudentGroupId = c.Guid(),
                        LecturerId = c.Guid(),
                        DisciplineId = c.Guid(),
                        NotParseRecord = c.String(),
                        LessonDiscipline = c.String(),
                        LessonLecturer = c.String(),
                        LessonGroup = c.String(),
                        LessonClassroom = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classrooms", t => t.ClassroomId)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId)
                .ForeignKey("dbo.SeasonDates", t => t.SeasonDatesId, cascadeDelete: true)
                .ForeignKey("dbo.StudentGroups", t => t.StudentGroupId)
                .Index(t => t.SeasonDatesId)
                .Index(t => t.ClassroomId)
                .Index(t => t.StudentGroupId)
                .Index(t => t.LecturerId)
                .Index(t => t.DisciplineId);
            
            CreateTable(
                "dbo.ScheduleLessonTimes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        Order = c.Int(nullable: false),
                        DateBeginLesson = c.DateTime(nullable: false),
                        DateEndLesson = c.DateTime(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SemesterRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsFirstHalfSemester = c.Boolean(nullable: false),
                        Week = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        Lesson = c.Int(nullable: false),
                        LessonType = c.Int(nullable: false),
                        IsStreaming = c.Boolean(nullable: false),
                        IsSubgroup = c.Boolean(nullable: false),
                        SeasonDatesId = c.Guid(nullable: false),
                        ClassroomId = c.Guid(),
                        StudentGroupId = c.Guid(),
                        LecturerId = c.Guid(),
                        DisciplineId = c.Guid(),
                        NotParseRecord = c.String(),
                        LessonDiscipline = c.String(),
                        LessonLecturer = c.String(),
                        LessonGroup = c.String(),
                        LessonClassroom = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classrooms", t => t.ClassroomId)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId)
                .ForeignKey("dbo.SeasonDates", t => t.SeasonDatesId, cascadeDelete: true)
                .ForeignKey("dbo.StudentGroups", t => t.StudentGroupId)
                .Index(t => t.SeasonDatesId)
                .Index(t => t.ClassroomId)
                .Index(t => t.StudentGroupId)
                .Index(t => t.LecturerId)
                .Index(t => t.DisciplineId);
            
            CreateTable(
                "dbo.StreamingLessons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IncomingGroups = c.String(nullable: false),
                        StreamName = c.String(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SemesterRecords", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.SemesterRecords", "SeasonDatesId", "dbo.SeasonDates");
            DropForeignKey("dbo.SemesterRecords", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.SemesterRecords", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.SemesterRecords", "ClassroomId", "dbo.Classrooms");
            DropForeignKey("dbo.OffsetRecords", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.OffsetRecords", "SeasonDatesId", "dbo.SeasonDates");
            DropForeignKey("dbo.OffsetRecords", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.OffsetRecords", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.OffsetRecords", "ClassroomId", "dbo.Classrooms");
            DropForeignKey("dbo.MaterialTechnicalValueRecords", "MaterialTechnicalValueGroupId", "dbo.MaterialTechnicalValueGroups");
            DropForeignKey("dbo.SoftwareRecords", "MaterialTechnicalValueId", "dbo.MaterialTechnicalValues");
            DropForeignKey("dbo.MaterialTechnicalValueRecords", "MaterialTechnicalValueId", "dbo.MaterialTechnicalValues");
            DropForeignKey("dbo.MaterialTechnicalValues", "ClassroomId", "dbo.Classrooms");
            DropForeignKey("dbo.ExaminationRecords", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.ExaminationRecords", "SeasonDatesId", "dbo.SeasonDates");
            DropForeignKey("dbo.ExaminationRecords", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.ExaminationRecords", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.ExaminationRecords", "ConsultationClassroomId", "dbo.Classrooms");
            DropForeignKey("dbo.ExaminationRecords", "ClassroomId", "dbo.Classrooms");
            DropForeignKey("dbo.ConsultationRecords", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.ConsultationRecords", "SeasonDatesId", "dbo.SeasonDates");
            DropForeignKey("dbo.ConsultationRecords", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.ConsultationRecords", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.ConsultationRecords", "ClassroomId", "dbo.Classrooms");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Accesses", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.AcademicPlanRecordElements", "AcademicPlanRecordId", "dbo.AcademicPlanRecords");
            DropForeignKey("dbo.TimeNorms", "DisciplineBlockId", "dbo.DisciplineBlocks");
            DropForeignKey("dbo.StudentHistories", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.StudentGroups", "EducationDirectionId", "dbo.EducationDirections");
            DropForeignKey("dbo.Contingents", "EducationDirectionId", "dbo.EducationDirections");
            DropForeignKey("dbo.Contingents", "AcademicYearId", "dbo.AcademicYears");
            DropForeignKey("dbo.AcademicPlanRecords", "ContingentId", "dbo.Contingents");
            DropForeignKey("dbo.AcademicPlans", "EducationDirectionId", "dbo.EducationDirections");
            DropForeignKey("dbo.StudentGroups", "CuratorId", "dbo.Lecturers");
            DropForeignKey("dbo.Lecturers", "LecturerPostId", "dbo.LecturerPosts");
            DropForeignKey("dbo.AcademicPlanRecordMissions", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.AcademicPlanRecordMissions", "AcademicPlanRecordElementId", "dbo.AcademicPlanRecordElements");
            DropForeignKey("dbo.DisciplineStudentRecords", "StudentId", "dbo.Students");
            DropForeignKey("dbo.DisciplineStudentRecords", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.DisciplineLessonTaskStudentRecords", "StudentId", "dbo.Students");
            DropForeignKey("dbo.DisciplineLessonTaskStudentRecords", "DisciplineLessonTaskId", "dbo.DisciplineLessonTasks");
            DropForeignKey("dbo.DisciplineLessonTasks", "DisciplineLessonId", "dbo.DisciplineLessons");
            DropForeignKey("dbo.DisciplineLessonStudentRecords", "StudentId", "dbo.Students");
            DropForeignKey("dbo.DisciplineLessonStudentRecords", "DisciplineLessonId", "dbo.DisciplineLessons");
            DropForeignKey("dbo.DisciplineLessons", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.Disciplines", "DisciplineBlockId", "dbo.DisciplineBlocks");
            DropForeignKey("dbo.AcademicPlanRecords", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.TimeNorms", "AcademicYearId", "dbo.AcademicYears");
            DropForeignKey("dbo.AcademicPlanRecordElements", "TimeNormId", "dbo.TimeNorms");
            DropForeignKey("dbo.StreamLessonRecords", "StreamLessonId", "dbo.StreamLessons");
            DropForeignKey("dbo.StreamLessonRecords", "AcademicPlanRecordElementId", "dbo.AcademicPlanRecordElements");
            DropForeignKey("dbo.StreamLessons", "AcademicYearId", "dbo.AcademicYears");
            DropForeignKey("dbo.SeasonDates", "AcademicYearId", "dbo.AcademicYears");
            DropForeignKey("dbo.AcademicPlans", "AcademicYearId", "dbo.AcademicYears");
            DropForeignKey("dbo.AcademicPlanRecords", "AcademicPlanId", "dbo.AcademicPlans");
            DropIndex("dbo.SemesterRecords", new[] { "DisciplineId" });
            DropIndex("dbo.SemesterRecords", new[] { "LecturerId" });
            DropIndex("dbo.SemesterRecords", new[] { "StudentGroupId" });
            DropIndex("dbo.SemesterRecords", new[] { "ClassroomId" });
            DropIndex("dbo.SemesterRecords", new[] { "SeasonDatesId" });
            DropIndex("dbo.OffsetRecords", new[] { "DisciplineId" });
            DropIndex("dbo.OffsetRecords", new[] { "LecturerId" });
            DropIndex("dbo.OffsetRecords", new[] { "StudentGroupId" });
            DropIndex("dbo.OffsetRecords", new[] { "ClassroomId" });
            DropIndex("dbo.OffsetRecords", new[] { "SeasonDatesId" });
            DropIndex("dbo.SoftwareRecords", new[] { "MaterialTechnicalValueId" });
            DropIndex("dbo.MaterialTechnicalValues", new[] { "ClassroomId" });
            DropIndex("dbo.MaterialTechnicalValueRecords", new[] { "MaterialTechnicalValueGroupId" });
            DropIndex("dbo.MaterialTechnicalValueRecords", new[] { "MaterialTechnicalValueId" });
            DropIndex("dbo.ExaminationRecords", new[] { "DisciplineId" });
            DropIndex("dbo.ExaminationRecords", new[] { "LecturerId" });
            DropIndex("dbo.ExaminationRecords", new[] { "StudentGroupId" });
            DropIndex("dbo.ExaminationRecords", new[] { "ClassroomId" });
            DropIndex("dbo.ExaminationRecords", new[] { "SeasonDatesId" });
            DropIndex("dbo.ExaminationRecords", new[] { "ConsultationClassroomId" });
            DropIndex("dbo.ConsultationRecords", new[] { "DisciplineId" });
            DropIndex("dbo.ConsultationRecords", new[] { "LecturerId" });
            DropIndex("dbo.ConsultationRecords", new[] { "StudentGroupId" });
            DropIndex("dbo.ConsultationRecords", new[] { "ClassroomId" });
            DropIndex("dbo.ConsultationRecords", new[] { "SeasonDatesId" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "LecturerId" });
            DropIndex("dbo.Users", new[] { "StudentId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.Accesses", new[] { "RoleId" });
            DropIndex("dbo.StudentHistories", new[] { "StudentId" });
            DropIndex("dbo.Contingents", new[] { "EducationDirectionId" });
            DropIndex("dbo.Contingents", new[] { "AcademicYearId" });
            DropIndex("dbo.AcademicPlanRecordMissions", new[] { "AcademicPlanRecordElementId" });
            DropIndex("dbo.AcademicPlanRecordMissions", new[] { "LecturerId" });
            DropIndex("dbo.Lecturers", new[] { "LecturerPostId" });
            DropIndex("dbo.StudentGroups", new[] { "CuratorId" });
            DropIndex("dbo.StudentGroups", new[] { "EducationDirectionId" });
            DropIndex("dbo.DisciplineStudentRecords", new[] { "StudentId" });
            DropIndex("dbo.DisciplineStudentRecords", new[] { "DisciplineId" });
            DropIndex("dbo.DisciplineLessonTasks", new[] { "DisciplineLessonId" });
            DropIndex("dbo.DisciplineLessonTaskStudentRecords", new[] { "StudentId" });
            DropIndex("dbo.DisciplineLessonTaskStudentRecords", new[] { "DisciplineLessonTaskId" });
            DropIndex("dbo.Students", new[] { "StudentGroupId" });
            DropIndex("dbo.DisciplineLessonStudentRecords", new[] { "StudentId" });
            DropIndex("dbo.DisciplineLessonStudentRecords", new[] { "DisciplineLessonId" });
            DropIndex("dbo.DisciplineLessons", new[] { "DisciplineId" });
            DropIndex("dbo.Disciplines", new[] { "DisciplineBlockId" });
            DropIndex("dbo.TimeNorms", new[] { "DisciplineBlockId" });
            DropIndex("dbo.TimeNorms", new[] { "AcademicYearId" });
            DropIndex("dbo.StreamLessonRecords", new[] { "AcademicPlanRecordElementId" });
            DropIndex("dbo.StreamLessonRecords", new[] { "StreamLessonId" });
            DropIndex("dbo.StreamLessons", new[] { "AcademicYearId" });
            DropIndex("dbo.SeasonDates", new[] { "AcademicYearId" });
            DropIndex("dbo.AcademicPlans", new[] { "EducationDirectionId" });
            DropIndex("dbo.AcademicPlans", new[] { "AcademicYearId" });
            DropIndex("dbo.AcademicPlanRecords", new[] { "ContingentId" });
            DropIndex("dbo.AcademicPlanRecords", new[] { "DisciplineId" });
            DropIndex("dbo.AcademicPlanRecords", new[] { "AcademicPlanId" });
            DropIndex("dbo.AcademicPlanRecordElements", new[] { "TimeNormId" });
            DropIndex("dbo.AcademicPlanRecordElements", new[] { "AcademicPlanRecordId" });
            DropTable("dbo.StreamingLessons");
            DropTable("dbo.SemesterRecords");
            DropTable("dbo.ScheduleLessonTimes");
            DropTable("dbo.OffsetRecords");
            DropTable("dbo.SoftwareRecords");
            DropTable("dbo.MaterialTechnicalValues");
            DropTable("dbo.MaterialTechnicalValueRecords");
            DropTable("dbo.MaterialTechnicalValueGroups");
            DropTable("dbo.ExaminationRecords");
            DropTable("dbo.CurrentSettings");
            DropTable("dbo.ConsultationRecords");
            DropTable("dbo.Classrooms");
            DropTable("dbo.Messages");
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.Accesses");
            DropTable("dbo.StudentHistories");
            DropTable("dbo.Contingents");
            DropTable("dbo.EducationDirections");
            DropTable("dbo.LecturerPosts");
            DropTable("dbo.AcademicPlanRecordMissions");
            DropTable("dbo.Lecturers");
            DropTable("dbo.StudentGroups");
            DropTable("dbo.DisciplineStudentRecords");
            DropTable("dbo.DisciplineLessonTasks");
            DropTable("dbo.DisciplineLessonTaskStudentRecords");
            DropTable("dbo.Students");
            DropTable("dbo.DisciplineLessonStudentRecords");
            DropTable("dbo.DisciplineLessons");
            DropTable("dbo.Disciplines");
            DropTable("dbo.DisciplineBlocks");
            DropTable("dbo.TimeNorms");
            DropTable("dbo.StreamLessonRecords");
            DropTable("dbo.StreamLessons");
            DropTable("dbo.SeasonDates");
            DropTable("dbo.AcademicYears");
            DropTable("dbo.AcademicPlans");
            DropTable("dbo.AcademicPlanRecords");
            DropTable("dbo.AcademicPlanRecordElements");
        }
    }
}
