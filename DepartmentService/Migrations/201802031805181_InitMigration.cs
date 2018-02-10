namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicPlanRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AcademicPlanId = c.Guid(nullable: false),
                        DisciplineId = c.Guid(nullable: false),
                        KindOfLoadId = c.Guid(nullable: false),
                        Semester = c.Int(nullable: false),
                        Hours = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicPlans", t => t.AcademicPlanId, cascadeDelete: true)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId, cascadeDelete: true)
                .ForeignKey("dbo.KindOfLoads", t => t.KindOfLoadId, cascadeDelete: true)
                .Index(t => t.AcademicPlanId)
                .Index(t => t.DisciplineId)
                .Index(t => t.KindOfLoadId);
            
            CreateTable(
                "dbo.AcademicPlans",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EducationDirectionId = c.Guid(nullable: false),
                        AcademicYearId = c.Guid(nullable: false),
                        AcademicLevel = c.Int(nullable: false),
                        AcademicCourses = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .ForeignKey("dbo.EducationDirections", t => t.EducationDirectionId, cascadeDelete: true)
                .Index(t => t.EducationDirectionId)
                .Index(t => t.AcademicYearId);
            
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
                "dbo.LoadDistributions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AcademicYearId = c.Guid(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .Index(t => t.AcademicYearId);
            
            CreateTable(
                "dbo.LoadDistributionRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoadDistributionId = c.Guid(nullable: false),
                        AcademicPlanRecordId = c.Guid(nullable: false),
                        ContingentId = c.Guid(nullable: false),
                        TimeNormId = c.Guid(nullable: false),
                        Load = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicPlanRecords", t => t.AcademicPlanRecordId, cascadeDelete: false)
                .ForeignKey("dbo.Contingents", t => t.ContingentId, cascadeDelete: false)
                .ForeignKey("dbo.LoadDistributions", t => t.LoadDistributionId, cascadeDelete: true)
                .ForeignKey("dbo.TimeNorms", t => t.TimeNormId, cascadeDelete: false)
                .Index(t => t.LoadDistributionId)
                .Index(t => t.AcademicPlanRecordId)
                .Index(t => t.ContingentId)
                .Index(t => t.TimeNormId);
            
            CreateTable(
                "dbo.Contingents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AcademicYearId = c.Guid(nullable: false),
                        EducationDirectionId = c.Guid(nullable: false),
                        Course = c.Int(nullable: false),
                        CountStudetns = c.Int(nullable: false),
                        CountSubgroups = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .ForeignKey("dbo.EducationDirections", t => t.EducationDirectionId, cascadeDelete: false)
                .Index(t => t.AcademicYearId)
                .Index(t => t.EducationDirectionId);
            
            CreateTable(
                "dbo.EducationDirections",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Cipher = c.String(nullable: false, maxLength: 10),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.LoadDistributionMissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoadDistributionRecordId = c.Guid(nullable: false),
                        LecturerId = c.Guid(nullable: false),
                        Hours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId, cascadeDelete: true)
                .ForeignKey("dbo.LoadDistributionRecords", t => t.LoadDistributionRecordId, cascadeDelete: true)
                .Index(t => t.LoadDistributionRecordId)
                .Index(t => t.LecturerId);
            
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
                "dbo.DisciplineLessonStudentRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineLessonId = c.Guid(nullable: false),
                        StudentId = c.Guid(nullable: false),
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
                "dbo.DisciplineLessons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineId = c.Guid(nullable: false),
                        LessonType = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId, cascadeDelete: true)
                .Index(t => t.DisciplineId);
            
            CreateTable(
                "dbo.Disciplines",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineBlockId = c.Guid(nullable: false),
                        DisciplineName = c.String(nullable: false, maxLength: 200),
                        DisciplineShortName = c.String(maxLength: 20),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineBlocks", t => t.DisciplineBlockId, cascadeDelete: true)
                .Index(t => t.DisciplineBlockId);
            
            CreateTable(
                "dbo.DisciplineBlocks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.DisciplineLessonTasks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineLessonId = c.Guid(nullable: false),
                        VariantNumber = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        MaxBall = c.Decimal(precision: 18, scale: 2),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineLessons", t => t.DisciplineLessonId, cascadeDelete: true)
                .Index(t => t.DisciplineLessonId);
            
            CreateTable(
                "dbo.DisciplineLessonTaskImageContexts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Image = c.Binary(nullable: false),
                        DisciplineLessonTaskId = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineLessonTasks", t => t.DisciplineLessonTaskId, cascadeDelete: true)
                .Index(t => t.DisciplineLessonTaskId);
            
            CreateTable(
                "dbo.DisciplineLessonTaskStudentRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisciplineLessonTaskId = c.Guid(nullable: false),
                        StudentId = c.Guid(nullable: false),
                        Result = c.Int(nullable: false),
                        Comment = c.String(),
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
                "dbo.DisciplineLessonTaskTextContexts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(nullable: false),
                        DisciplineLessonTaskId = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineLessonTasks", t => t.DisciplineLessonTaskId, cascadeDelete: true)
                .Index(t => t.DisciplineLessonTaskId);
            
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
                "dbo.TimeNorms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        KindOfLoadId = c.Guid(nullable: false),
                        AcademicYearId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        Formula = c.String(),
                        Hours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreate = c.DateTime(nullable: false),
                        DateDelete = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .ForeignKey("dbo.KindOfLoads", t => t.KindOfLoadId, cascadeDelete: false)
                .Index(t => t.KindOfLoadId)
                .Index(t => t.AcademicYearId);
            
            CreateTable(
                "dbo.KindOfLoads",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        KindOfLoadName = c.String(nullable: false, maxLength: 50),
                        KindOfLoadType = c.Int(nullable: false),
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
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StudentId = c.Guid(),
                        LecturerId = c.Guid(),
                        RoleType = c.Int(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
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
            DropForeignKey("dbo.Users", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "LecturerId", "dbo.Lecturers");
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
            DropForeignKey("dbo.Accesses", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.SeasonDates", "AcademicYearId", "dbo.AcademicYears");
            DropForeignKey("dbo.LoadDistributionRecords", "TimeNormId", "dbo.TimeNorms");
            DropForeignKey("dbo.TimeNorms", "KindOfLoadId", "dbo.KindOfLoads");
            DropForeignKey("dbo.AcademicPlanRecords", "KindOfLoadId", "dbo.KindOfLoads");
            DropForeignKey("dbo.TimeNorms", "AcademicYearId", "dbo.AcademicYears");
            DropForeignKey("dbo.LoadDistributionRecords", "LoadDistributionId", "dbo.LoadDistributions");
            DropForeignKey("dbo.LoadDistributionRecords", "ContingentId", "dbo.Contingents");
            DropForeignKey("dbo.StudentHistories", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.DisciplineLessonStudentRecords", "StudentId", "dbo.Students");
            DropForeignKey("dbo.DisciplineLessonTaskTextContexts", "DisciplineLessonTaskId", "dbo.DisciplineLessonTasks");
            DropForeignKey("dbo.DisciplineLessonTaskStudentRecords", "StudentId", "dbo.Students");
            DropForeignKey("dbo.DisciplineLessonTaskStudentRecords", "DisciplineLessonTaskId", "dbo.DisciplineLessonTasks");
            DropForeignKey("dbo.DisciplineLessonTaskImageContexts", "DisciplineLessonTaskId", "dbo.DisciplineLessonTasks");
            DropForeignKey("dbo.DisciplineLessonTasks", "DisciplineLessonId", "dbo.DisciplineLessons");
            DropForeignKey("dbo.DisciplineLessonStudentRecords", "DisciplineLessonId", "dbo.DisciplineLessons");
            DropForeignKey("dbo.DisciplineStudentRecords", "StudentId", "dbo.Students");
            DropForeignKey("dbo.DisciplineStudentRecords", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.DisciplineLessons", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.Disciplines", "DisciplineBlockId", "dbo.DisciplineBlocks");
            DropForeignKey("dbo.AcademicPlanRecords", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.StudentGroups", "EducationDirectionId", "dbo.EducationDirections");
            DropForeignKey("dbo.StudentGroups", "CuratorId", "dbo.Lecturers");
            DropForeignKey("dbo.LoadDistributionMissions", "LoadDistributionRecordId", "dbo.LoadDistributionRecords");
            DropForeignKey("dbo.LoadDistributionMissions", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.Lecturers", "LecturerPostId", "dbo.LecturerPosts");
            DropForeignKey("dbo.Contingents", "EducationDirectionId", "dbo.EducationDirections");
            DropForeignKey("dbo.AcademicPlans", "EducationDirectionId", "dbo.EducationDirections");
            DropForeignKey("dbo.Contingents", "AcademicYearId", "dbo.AcademicYears");
            DropForeignKey("dbo.LoadDistributionRecords", "AcademicPlanRecordId", "dbo.AcademicPlanRecords");
            DropForeignKey("dbo.LoadDistributions", "AcademicYearId", "dbo.AcademicYears");
            DropForeignKey("dbo.AcademicPlans", "AcademicYearId", "dbo.AcademicYears");
            DropForeignKey("dbo.AcademicPlanRecords", "AcademicPlanId", "dbo.AcademicPlans");
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
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
            DropIndex("dbo.Users", new[] { "LecturerId" });
            DropIndex("dbo.Users", new[] { "StudentId" });
            DropIndex("dbo.Messages", new[] { "UserId" });
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
            DropIndex("dbo.Accesses", new[] { "RoleId" });
            DropIndex("dbo.SeasonDates", new[] { "AcademicYearId" });
            DropIndex("dbo.TimeNorms", new[] { "AcademicYearId" });
            DropIndex("dbo.TimeNorms", new[] { "KindOfLoadId" });
            DropIndex("dbo.StudentHistories", new[] { "StudentId" });
            DropIndex("dbo.DisciplineLessonTaskTextContexts", new[] { "DisciplineLessonTaskId" });
            DropIndex("dbo.DisciplineLessonTaskStudentRecords", new[] { "StudentId" });
            DropIndex("dbo.DisciplineLessonTaskStudentRecords", new[] { "DisciplineLessonTaskId" });
            DropIndex("dbo.DisciplineLessonTaskImageContexts", new[] { "DisciplineLessonTaskId" });
            DropIndex("dbo.DisciplineLessonTasks", new[] { "DisciplineLessonId" });
            DropIndex("dbo.DisciplineStudentRecords", new[] { "StudentId" });
            DropIndex("dbo.DisciplineStudentRecords", new[] { "DisciplineId" });
            DropIndex("dbo.Disciplines", new[] { "DisciplineBlockId" });
            DropIndex("dbo.DisciplineLessons", new[] { "DisciplineId" });
            DropIndex("dbo.DisciplineLessonStudentRecords", new[] { "StudentId" });
            DropIndex("dbo.DisciplineLessonStudentRecords", new[] { "DisciplineLessonId" });
            DropIndex("dbo.Students", new[] { "StudentGroupId" });
            DropIndex("dbo.LoadDistributionMissions", new[] { "LecturerId" });
            DropIndex("dbo.LoadDistributionMissions", new[] { "LoadDistributionRecordId" });
            DropIndex("dbo.Lecturers", new[] { "LecturerPostId" });
            DropIndex("dbo.StudentGroups", new[] { "CuratorId" });
            DropIndex("dbo.StudentGroups", new[] { "EducationDirectionId" });
            DropIndex("dbo.Contingents", new[] { "EducationDirectionId" });
            DropIndex("dbo.Contingents", new[] { "AcademicYearId" });
            DropIndex("dbo.LoadDistributionRecords", new[] { "TimeNormId" });
            DropIndex("dbo.LoadDistributionRecords", new[] { "ContingentId" });
            DropIndex("dbo.LoadDistributionRecords", new[] { "AcademicPlanRecordId" });
            DropIndex("dbo.LoadDistributionRecords", new[] { "LoadDistributionId" });
            DropIndex("dbo.LoadDistributions", new[] { "AcademicYearId" });
            DropIndex("dbo.AcademicPlans", new[] { "AcademicYearId" });
            DropIndex("dbo.AcademicPlans", new[] { "EducationDirectionId" });
            DropIndex("dbo.AcademicPlanRecords", new[] { "KindOfLoadId" });
            DropIndex("dbo.AcademicPlanRecords", new[] { "DisciplineId" });
            DropIndex("dbo.AcademicPlanRecords", new[] { "AcademicPlanId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.StreamingLessons");
            DropTable("dbo.SemesterRecords");
            DropTable("dbo.ScheduleLessonTimes");
            DropTable("dbo.OffsetRecords");
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
            DropTable("dbo.ExaminationRecords");
            DropTable("dbo.CurrentSettings");
            DropTable("dbo.ConsultationRecords");
            DropTable("dbo.Classrooms");
            DropTable("dbo.Roles");
            DropTable("dbo.Accesses");
            DropTable("dbo.SeasonDates");
            DropTable("dbo.KindOfLoads");
            DropTable("dbo.TimeNorms");
            DropTable("dbo.StudentHistories");
            DropTable("dbo.DisciplineLessonTaskTextContexts");
            DropTable("dbo.DisciplineLessonTaskStudentRecords");
            DropTable("dbo.DisciplineLessonTaskImageContexts");
            DropTable("dbo.DisciplineLessonTasks");
            DropTable("dbo.DisciplineStudentRecords");
            DropTable("dbo.DisciplineBlocks");
            DropTable("dbo.Disciplines");
            DropTable("dbo.DisciplineLessons");
            DropTable("dbo.DisciplineLessonStudentRecords");
            DropTable("dbo.Students");
            DropTable("dbo.LoadDistributionMissions");
            DropTable("dbo.LecturerPosts");
            DropTable("dbo.Lecturers");
            DropTable("dbo.StudentGroups");
            DropTable("dbo.EducationDirections");
            DropTable("dbo.Contingents");
            DropTable("dbo.LoadDistributionRecords");
            DropTable("dbo.LoadDistributions");
            DropTable("dbo.AcademicYears");
            DropTable("dbo.AcademicPlans");
            DropTable("dbo.AcademicPlanRecords");
        }
    }
}
