using Microsoft.EntityFrameworkCore;
using ScheduleSyncProject.Logic.Logic.Repository;
using ScheduleSyncProject.Logic.Models.CoreModels;
using ScheduleSyncProject.Logic.Models.SettingModels;

namespace ScheduleSyncProject.Test;

internal class CoreRepositoryTests
{
	private CoreRepository _coreRepository;

	private ScheduleDbContext _dbContext;

	[OneTimeSetUp]
	public async Task OneTimeSetUp()
	{
		var settings = new DatabaseSettings
		{
			ConnectionString = "Data Source=DESKTOP-UO6OGFM\\SQLEXPRESS;persist security info=True; Integrated Security=SSPI;TrustServerCertificate=True;"
		};

		_dbContext = new(settings);

		await _dbContext.Database.ExecuteSqlAsync($@"USE [master]
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'ScheduleSyncProjectTest')
  BEGIN
	CREATE DATABASE [ScheduleSyncProjectTest]
	 CONTAINMENT = NONE
	 ON  PRIMARY 
	( NAME = N'ScheduleSyncProjectTest', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ScheduleSyncProjectTest.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
	 LOG ON 
	( NAME = N'ScheduleSyncProjectTest_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ScheduleSyncProjectTest.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
	 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF

	IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
	BEGIN
	EXEC [ScheduleSyncProjectTest].[dbo].[sp_fulltext_database] @action = 'enable'
	END

	ALTER DATABASE [ScheduleSyncProjectTest] SET ANSI_NULL_DEFAULT OFF 
	ALTER DATABASE [ScheduleSyncProjectTest] SET ANSI_NULLS OFF 
	ALTER DATABASE [ScheduleSyncProjectTest] SET ANSI_PADDING OFF 
	ALTER DATABASE [ScheduleSyncProjectTest] SET ANSI_WARNINGS OFF 
	ALTER DATABASE [ScheduleSyncProjectTest] SET ARITHABORT OFF 
	ALTER DATABASE [ScheduleSyncProjectTest] SET AUTO_CLOSE ON 
	ALTER DATABASE [ScheduleSyncProjectTest] SET AUTO_SHRINK OFF 
	ALTER DATABASE [ScheduleSyncProjectTest] SET AUTO_UPDATE_STATISTICS ON 
	ALTER DATABASE [ScheduleSyncProjectTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
	ALTER DATABASE [ScheduleSyncProjectTest] SET CURSOR_DEFAULT  GLOBAL 
	ALTER DATABASE [ScheduleSyncProjectTest] SET CONCAT_NULL_YIELDS_NULL OFF 
	ALTER DATABASE [ScheduleSyncProjectTest] SET NUMERIC_ROUNDABORT OFF 
	ALTER DATABASE [ScheduleSyncProjectTest] SET QUOTED_IDENTIFIER OFF 
	ALTER DATABASE [ScheduleSyncProjectTest] SET RECURSIVE_TRIGGERS OFF 
	ALTER DATABASE [ScheduleSyncProjectTest] SET  ENABLE_BROKER 
	ALTER DATABASE [ScheduleSyncProjectTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
	ALTER DATABASE [ScheduleSyncProjectTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
	ALTER DATABASE [ScheduleSyncProjectTest] SET TRUSTWORTHY OFF 
	ALTER DATABASE [ScheduleSyncProjectTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
	ALTER DATABASE [ScheduleSyncProjectTest] SET PARAMETERIZATION SIMPLE 
	ALTER DATABASE [ScheduleSyncProjectTest] SET READ_COMMITTED_SNAPSHOT ON 
	ALTER DATABASE [ScheduleSyncProjectTest] SET HONOR_BROKER_PRIORITY OFF 
	ALTER DATABASE [ScheduleSyncProjectTest] SET RECOVERY SIMPLE 
	ALTER DATABASE [ScheduleSyncProjectTest] SET  MULTI_USER 
	ALTER DATABASE [ScheduleSyncProjectTest] SET PAGE_VERIFY CHECKSUM  
	ALTER DATABASE [ScheduleSyncProjectTest] SET DB_CHAINING OFF 
	ALTER DATABASE [ScheduleSyncProjectTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
	ALTER DATABASE [ScheduleSyncProjectTest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
	ALTER DATABASE [ScheduleSyncProjectTest] SET DELAYED_DURABILITY = DISABLED 
	ALTER DATABASE [ScheduleSyncProjectTest] SET ACCELERATED_DATABASE_RECOVERY = OFF  
	ALTER DATABASE [ScheduleSyncProjectTest] SET QUERY_STORE = ON
	ALTER DATABASE [ScheduleSyncProjectTest] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
	ALTER DATABASE [ScheduleSyncProjectTest] SET  READ_WRITE 
END");

		_dbContext.Database.SetConnectionString(@"Data Source=DESKTOP-UO6OGFM\SQLEXPRESS;Initial Catalog=ScheduleSyncProjectTest;persist security info=True; Integrated Security=SSPI;TrustServerCertificate=True;");

		await _dbContext.Database.ExecuteSqlAsync($@"
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SeasonDates]') AND type in (N'U'))
CREATE TABLE [dbo].[SeasonDates](
	[Id] [uniqueidentifier] NOT NULL,
	[DateCreate] [datetime2](7) NOT NULL,
	[DateDelete] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[AcademicYearId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[DateBeginFirstHalfSemester] [datetime2](7) NOT NULL,
	[DateEndFirstHalfSemester] [datetime2](7) NOT NULL,
	[DateBeginSecondHalfSemester] [datetime2](7) NOT NULL,
	[DateEndSecondHalfSemester] [datetime2](7) NOT NULL,
	[DateBeginOffset] [datetime2](7) NOT NULL,
	[DateEndOffset] [datetime2](7) NOT NULL,
	[DateBeginExamination] [datetime2](7) NOT NULL,
	[DateEndExamination] [datetime2](7) NOT NULL,
	[DateBeginPractice] [datetime2](7) NULL,
	[DateEndPractice] [datetime2](7) NULL,
 CONSTRAINT [PK_SeasonDates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]");

		await _dbContext.Database.ExecuteSqlAsync($@"
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Classrooms]') AND type in (N'U'))
CREATE TABLE [dbo].[Classrooms](
	[Id] [uniqueidentifier] NOT NULL,
	[DateCreate] [datetime2](7) NOT NULL,
	[DateDelete] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[Number] [nvarchar](max) NULL,
	[ClassroomType] [int] NOT NULL,
	[Capacity] [int] NOT NULL,
	[NotUseInSchedule] [bit] NOT NULL,
	CONSTRAINT [PK_Classrooms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]");

		await _dbContext.Database.ExecuteSqlAsync($@"
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lecturers]') AND type in (N'U'))
CREATE TABLE [dbo].[Lecturers](
	[Id] [uniqueidentifier] NOT NULL,
	[DateCreate] [datetime2](7) NOT NULL,
	[DateDelete] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[LecturerStudyPostId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NOT NULL,
	[Abbreviation] [nvarchar](10) NULL,
	[DateBirth] [datetime2](7) NOT NULL,
	[Address] [nvarchar](250) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[MobileNumber] [nvarchar](50) NOT NULL,
	[HomeNumber] [nvarchar](50) NULL,
	[Rank] [int] NOT NULL,
	[Rank2] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Photo] [varbinary](max) NULL,
	[OnlyForPrivate] [bit] NOT NULL,
	[LecturerDepartmentPostId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Lecturers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]");

		await _dbContext.Database.ExecuteSqlAsync($@"
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StudentGroups]') AND type in (N'U'))
CREATE TABLE [dbo].[StudentGroups](
	[Id] [uniqueidentifier] NOT NULL,
	[DateCreate] [datetime2](7) NOT NULL,
	[DateDelete] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[EducationDirectionId] [uniqueidentifier] NOT NULL,
	[CuratorId] [uniqueidentifier] NULL,
	[GroupName] [nvarchar](20) NOT NULL,
	[Course] [int] NOT NULL,
 CONSTRAINT [PK_StudentGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]");

		await _dbContext.Database.ExecuteSqlAsync($@"
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SemesterRecords]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[SemesterRecords](
		[Id] [uniqueidentifier] NOT NULL,
		[ClassroomId] [uniqueidentifier] NULL,
		[StudentGroupId] [uniqueidentifier] NULL,
		[LecturerId] [uniqueidentifier] NULL,
		[DisciplineId] [uniqueidentifier] NULL,
		[NotParseRecord] [nvarchar](max) NULL,
		[LessonDiscipline] [nvarchar](max) NOT NULL,
		[LessonLecturer] [nvarchar](max) NOT NULL,
		[LessonClassroom] [nvarchar](max) NOT NULL,
		[LessonType] [int] NOT NULL,
		[LessonStudentGroup] [nvarchar](max) NOT NULL,
		[ScheduleDate] [datetime2](7) NOT NULL,
	 CONSTRAINT [PK_SemesterRecords] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	ALTER TABLE [dbo].[SemesterRecords] ADD  DEFAULT (N'') FOR [LessonStudentGroup]
	ALTER TABLE [dbo].[SemesterRecords] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [ScheduleDate]

	ALTER TABLE [dbo].[SemesterRecords]  WITH CHECK ADD  CONSTRAINT [FK_SemesterRecords_Classrooms_ClassroomId] FOREIGN KEY([ClassroomId])
	REFERENCES [dbo].[Classrooms] ([Id])
	ALTER TABLE [dbo].[SemesterRecords] CHECK CONSTRAINT [FK_SemesterRecords_Classrooms_ClassroomId]

	ALTER TABLE [dbo].[SemesterRecords]  WITH CHECK ADD  CONSTRAINT [FK_SemesterRecords_Lecturers_LecturerId] FOREIGN KEY([LecturerId])
	REFERENCES [dbo].[Lecturers] ([Id])
	ALTER TABLE [dbo].[SemesterRecords] CHECK CONSTRAINT [FK_SemesterRecords_Lecturers_LecturerId]

	ALTER TABLE [dbo].[SemesterRecords]  WITH CHECK ADD  CONSTRAINT [FK_SemesterRecords_StudentGroups_StudentGroupId] FOREIGN KEY([StudentGroupId])
	REFERENCES [dbo].[StudentGroups] ([Id])
	ALTER TABLE [dbo].[SemesterRecords] CHECK CONSTRAINT [FK_SemesterRecords_StudentGroups_StudentGroupId]
END");

		_coreRepository = new(_dbContext);
	}

	[OneTimeTearDown]
	public async Task OneTimeTearDown()
	{
		await _dbContext.Database.ExecuteSqlAsync($@"
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SemesterRecords]') AND type in (N'U'))
DROP TABLE [dbo].[SemesterRecords]");

		await _dbContext.Database.ExecuteSqlAsync($@"
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StudentGroups]') AND type in (N'U'))
DROP TABLE [dbo].[StudentGroups]");

		await _dbContext.Database.ExecuteSqlAsync($@"
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lecturers]') AND type in (N'U'))
DROP TABLE [dbo].[Lecturers]");

		await _dbContext.Database.ExecuteSqlAsync($@"
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Classrooms]') AND type in (N'U'))
DROP TABLE [dbo].[Classrooms]");

		await _dbContext.Database.ExecuteSqlAsync($@"
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SeasonDates]') AND type in (N'U'))
DROP TABLE [dbo].[SeasonDates]");

		await _dbContext.Database.ExecuteSqlAsync($@"USE [master]
IF EXISTS(SELECT * FROM sys.databases WHERE name = 'ScheduleSyncProjectTest')
DROP DATABASE [ScheduleSyncProjectTest]");

		await _coreRepository.DisposeAsync();
	}

	[TearDown]
	public async Task TearDown()
	{
		await _dbContext.Database.ExecuteSqlAsync($@"DELETE FROM [dbo].[SemesterRecords]");

		await _dbContext.Database.ExecuteSqlAsync($@"DELETE FROM [dbo].[StudentGroups]");

		await _dbContext.Database.ExecuteSqlAsync($@"DELETE FROM [dbo].[Lecturers]");

		await _dbContext.Database.ExecuteSqlAsync($@"DELETE FROM [dbo].[Classrooms]");

		await _dbContext.Database.ExecuteSqlAsync($@"DELETE FROM [dbo].[SeasonDates]");
	}

	[Test]
	public async Task Test_GetSeasonDatesAsync()
	{
		var guid1 = Guid.NewGuid();
		var guid2 = Guid.NewGuid();
		await _dbContext.Database.ExecuteSqlAsync($@"
INSERT INTO [dbo].[SeasonDates]
	([Id], [DateCreate], [DateDelete], [IsDeleted], [AcademicYearId], [Title]
	, [DateBeginFirstHalfSemester], [DateEndFirstHalfSemester]
	, [DateBeginSecondHalfSemester], [DateEndSecondHalfSemester]
	, [DateBeginOffset], [DateEndOffset]
	, [DateBeginExamination], [DateEndExamination]
	, [DateBeginPractice], [DateEndPractice])
VALUES
	({guid1}, {DateTime.UtcNow}, NULL, 0, {Guid.NewGuid()}, 'Title_1'
	, {DateTime.UtcNow.Date.AddDays(-7)}, {DateTime.UtcNow.Date.AddDays(7)}
	, {DateTime.UtcNow.Date.AddDays(-7)}, {DateTime.UtcNow.Date.AddDays(7)}
	, {DateTime.UtcNow.Date.AddDays(-7)}, {DateTime.UtcNow.Date.AddDays(7)}
	, {DateTime.UtcNow.Date.AddDays(-7)}, {DateTime.UtcNow.Date.AddDays(7)}
	, {DateTime.UtcNow.Date.AddDays(-7)}, {DateTime.UtcNow.Date.AddDays(7)}),
	({guid2}, {DateTime.UtcNow}, NULL, 0, {Guid.NewGuid()}, 'Title_2'
	, {DateTime.UtcNow.Date.AddDays(7)}, {DateTime.UtcNow.Date.AddDays(14)}
	, {DateTime.UtcNow.Date.AddDays(7)}, {DateTime.UtcNow.Date.AddDays(14)}
	, {DateTime.UtcNow.Date.AddDays(7)}, {DateTime.UtcNow.Date.AddDays(14)}
	, {DateTime.UtcNow.Date.AddDays(7)}, {DateTime.UtcNow.Date.AddDays(14)}
	, {DateTime.UtcNow.Date.AddDays(7)}, {DateTime.UtcNow.Date.AddDays(14)})
");

		var data = await _coreRepository.GetSeasonDatesAsync(CancellationToken.None);
		Assert.That(data, Is.Not.Null);
		Assert.That(data.Count(), Is.EqualTo(2));
		var firstRecord = data.FirstOrDefault(x => x.Id == guid1);
		Assert.That(firstRecord, Is.Not.Null);
		Assert.That(firstRecord.DateBeginFirstHalfSemester, Is.EqualTo(DateTime.UtcNow.Date.AddDays(-7)));
		var secondRecord = data.FirstOrDefault(x => x.Id == guid2);
		Assert.That(secondRecord, Is.Not.Null);
		Assert.That(secondRecord.DateBeginFirstHalfSemester, Is.EqualTo(DateTime.UtcNow.Date.AddDays(7)));
	}

	[Test]
	public async Task Test_GetClassroomsAsync()
	{
		var guid1 = Guid.NewGuid();
		var guid2 = Guid.NewGuid();
		await _dbContext.Database.ExecuteSqlAsync($@"
INSERT INTO [dbo].[Classrooms]
	([Id], [DateCreate], [DateDelete], [IsDeleted], [Number], [ClassroomType], [Capacity], [NotUseInSchedule])
VALUES
	({guid1}, {DateTime.UtcNow}, NULL, 0, '100-1', 1, 20, 0),
	({guid2}, {DateTime.UtcNow}, NULL, 0, '101-1', 2, 40, 0)
");

		var data = await _coreRepository.GetClassroomsAsync(CancellationToken.None);
		Assert.That(data, Is.Not.Null);
		Assert.That(data.Count(), Is.EqualTo(2));
		var firstRecord = data.FirstOrDefault(x => x.Id == guid1);
		Assert.That(firstRecord, Is.Not.Null);
		Assert.That(firstRecord.Number, Is.EqualTo("100-1"));
		var secondRecord = data.FirstOrDefault(x => x.Id == guid2);
		Assert.That(secondRecord, Is.Not.Null);
		Assert.That(secondRecord.Number, Is.EqualTo("101-1"));
	}

	[Test]
	public async Task Test_GetLecturersAsync()
	{
		var guid1 = Guid.NewGuid();
		var guid2 = Guid.NewGuid();
		await _dbContext.Database.ExecuteSqlAsync($@"
INSERT INTO [dbo].[Lecturers]
	([Id], [DateCreate], [DateDelete], [IsDeleted], [LecturerStudyPostId]
	, [FirstName], [LastName], [Patronymic], [Abbreviation], [DateBirth]
	, [Address], [Email], [MobileNumber], [HomeNumber], [Rank], [Rank2]
	, [Description], [Photo], [OnlyForPrivate], [LecturerDepartmentPostId])
VALUES
	({guid1}, {DateTime.UtcNow}, NULL, 0, {Guid.NewGuid()}
	, 'FirstName_1', 'LastName_1', 'Patronymic', NULL, {DateTime.UtcNow}
	, 'Address', 'Email', 'MobileNumber', 'HomeNumber', 1, 1, NULL, NULL, 0, {Guid.NewGuid()}),
	({guid2}, {DateTime.UtcNow}, NULL, 0, {Guid.NewGuid()}
	, 'FirstName_2', 'LastName_2', 'Patronymic', NULL, {DateTime.UtcNow}
	, 'Address', 'Email', 'MobileNumber', 'HomeNumber', 1, 1, NULL, NULL, 0, {Guid.NewGuid()})
");

		var data = await _coreRepository.GetLecturersAsync(CancellationToken.None);
		Assert.That(data, Is.Not.Null);
		Assert.That(data.Count(), Is.EqualTo(2));
		var firstRecord = data.FirstOrDefault(x => x.Id == guid1);
		Assert.That(firstRecord, Is.Not.Null);
		Assert.That(firstRecord.LastName, Is.EqualTo("LastName_1"));
		var secondRecord = data.FirstOrDefault(x => x.Id == guid2);
		Assert.That(secondRecord, Is.Not.Null);
		Assert.That(secondRecord.LastName, Is.EqualTo("LastName_2"));
	}

	[Test]
	public async Task Test_GetStudentGroupsAsync()
	{
		var guid1 = Guid.NewGuid();
		var guid2 = Guid.NewGuid();
		await _dbContext.Database.ExecuteSqlAsync($@"
INSERT INTO [dbo].[StudentGroups]
	([Id], [DateCreate], [DateDelete], [IsDeleted], [EducationDirectionId], [CuratorId], [GroupName], [Course])
VALUES
	({guid1}, {DateTime.UtcNow}, NULL, 0, {Guid.NewGuid()}, {Guid.NewGuid()}, 'GroupName_1', 1),
	({guid2}, {DateTime.UtcNow}, NULL, 0, {Guid.NewGuid()}, {Guid.NewGuid()}, 'GroupName_2', 3)
");

		var data = await _coreRepository.GetStudentGroupsAsync(CancellationToken.None);
		Assert.That(data, Is.Not.Null);
		Assert.That(data.Count(), Is.EqualTo(2));
		var firstRecord = data.FirstOrDefault(x => x.Id == guid1);
		Assert.That(firstRecord, Is.Not.Null);
		Assert.That(firstRecord.GroupName, Is.EqualTo("GroupName_1"));
		var secondRecord = data.FirstOrDefault(x => x.Id == guid2);
		Assert.That(secondRecord, Is.Not.Null);
		Assert.That(secondRecord.GroupName, Is.EqualTo("GroupName_2"));
	}

	[Test]
	public async Task Test_GetSemesterRecordsAsync()
	{
		var guid1 = Guid.NewGuid();
		var guid2 = Guid.NewGuid();
		await _dbContext.Database.ExecuteSqlAsync($@"
INSERT INTO [dbo].[SemesterRecords]
	([Id], [ClassroomId], [StudentGroupId], [LecturerId], [DisciplineId]
	, [NotParseRecord], [LessonDiscipline], [LessonLecturer], [LessonClassroom], [LessonType], [LessonStudentGroup], [ScheduleDate])
VALUES
	({guid1}, NULL, NULL, NULL, NULL, NULL
	, 'LessonDiscipline_1', 'LessonLecturer', 'LessonClassroom', 1, 'LessonStudentGroup', {DateTime.UtcNow.Date.AddDays(-1)}),
	({guid2}, NULL, NULL, NULL, NULL, NULL
	, 'LessonDiscipline_2', 'LessonLecturer', 'LessonClassroom', 1, 'LessonStudentGroup', {DateTime.UtcNow.Date.AddDays(1)}),
	({Guid.NewGuid()}, NULL, NULL, NULL, NULL, NULL
	, 'LessonDiscipline_3', 'LessonLecturer', 'LessonClassroom', 1, 'LessonStudentGroup', {DateTime.UtcNow.Date.AddDays(-2)}),
	({Guid.NewGuid()}, NULL, NULL, NULL, NULL, NULL
	, 'LessonDiscipline_4', 'LessonLecturer', 'LessonClassroom', 1, 'LessonStudentGroup', {DateTime.UtcNow.Date.AddDays(2)}),
	({Guid.NewGuid()}, NULL, NULL, NULL, NULL, NULL
	, 'LessonDiscipline_5', 'LessonLecturer', 'LessonClassroom', 1, 'LessonStudentGroup', {DateTime.UtcNow.Date.AddDays(-3)}),
	({Guid.NewGuid()}, NULL, NULL, NULL, NULL, NULL
	, 'LessonDiscipline_6', 'LessonLecturer', 'LessonClassroom', 1, 'LessonStudentGroup', {DateTime.UtcNow.Date.AddDays(3)})
");

		var data = await _coreRepository.GetSemesterRecordsAsync(DateTime.UtcNow.Date.AddDays(-2), DateTime.UtcNow.Date.AddDays(2),
			CancellationToken.None);
		Assert.That(data, Is.Not.Null);
		Assert.That(data.Count(), Is.EqualTo(4));
		var firstRecord = data.FirstOrDefault(x => x.Id == guid1);
		Assert.That(firstRecord, Is.Not.Null);
		Assert.Multiple(() =>
		{
			Assert.That(firstRecord.ScheduleDate, Is.EqualTo(DateTime.UtcNow.Date.AddDays(-1)));
			Assert.That(firstRecord.LessonDiscipline, Is.EqualTo("LessonDiscipline_1"));
		});
		var secondRecord = data.FirstOrDefault(x => x.Id == guid2);
		Assert.That(secondRecord, Is.Not.Null);
		Assert.Multiple(() =>
		{
			Assert.That(secondRecord.ScheduleDate, Is.EqualTo(DateTime.UtcNow.Date.AddDays(1)));
			Assert.That(secondRecord.LessonDiscipline, Is.EqualTo("LessonDiscipline_2"));
		});
	}

	[Test]
	public async Task Test_SaveSemesterRecordAsync()
	{
		var guidClassroom = Guid.NewGuid();
		await _dbContext.Database.ExecuteSqlAsync($@"
INSERT INTO[dbo].[Classrooms]
	([Id], [DateCreate], [DateDelete], [IsDeleted], [Number], [ClassroomType], [Capacity], [NotUseInSchedule])
VALUES
	({guidClassroom}, {DateTime.UtcNow}, NULL, 0, '100-1', 1, 20, 0)
");
		var guidLecturer = Guid.NewGuid();
		await _dbContext.Database.ExecuteSqlAsync($@"
INSERT INTO[dbo].[Lecturers]
	([Id], [DateCreate], [DateDelete], [IsDeleted], [LecturerStudyPostId], [FirstName], [LastName], [Patronymic], [Abbreviation], [DateBirth], [Address], [Email], [MobileNumber], [HomeNumber], [Rank], [Rank2], [Description], [Photo], [OnlyForPrivate], [LecturerDepartmentPostId])
VALUES
	({guidLecturer}, {DateTime.UtcNow}, NULL, 0, {Guid.NewGuid()}, 'FirstName', 'LastName', 'Patronymic', NULL, {DateTime.UtcNow}, 'Address', 'Email', 'MobileNumber', 'HomeNumber', 1, 1, NULL, NULL, 0, {Guid.NewGuid()})
");
		var guidStudentGroup = Guid.NewGuid();
		await _dbContext.Database.ExecuteSqlAsync($@"
INSERT INTO[dbo].[StudentGroups]
	([Id], [DateCreate], [DateDelete], [IsDeleted], [EducationDirectionId], [CuratorId], [GroupName], [Course])
VALUES
	({guidStudentGroup}, {DateTime.UtcNow}, NULL, 0, {Guid.NewGuid()}, {Guid.NewGuid()}, 'GroupName', 1)
");

		var data = await _coreRepository.GetSemesterRecordsAsync(DateTime.UtcNow.Date.AddDays(-10), DateTime.UtcNow.Date.AddDays(10),
			CancellationToken.None);
		Assert.That(data, Is.Not.Null);
		Assert.That(data.Count(), Is.EqualTo(0));

		var records = new SemesterRecord[]
			{
				new(){ ClassroomId = guidClassroom, LecturerId = guidLecturer, StudentGroupId = guidStudentGroup,
					LessonDiscipline = "Discipline", LessonClassroom = "100-1", LessonLecturer = "Lecturer",
					LessonStudentGroup = "GroupName", LessonType = LessonTypes.лек, ScheduleDate = DateTime.UtcNow.Date.AddDays(-1) },
				new(){ LessonDiscipline = "Discipline", LessonClassroom = "no", LessonLecturer = "no",
					LessonStudentGroup = "no", LessonType = LessonTypes.нд, ScheduleDate = DateTime.UtcNow.Date.AddDays(1) }
			};

		Assert.DoesNotThrowAsync(async () =>
		{
			foreach (var record in records)
			{
				await _coreRepository.SaveSemesterRecordAsync(record, CancellationToken.None);
			}
		});

		data = await _coreRepository.GetSemesterRecordsAsync(DateTime.UtcNow.Date.AddDays(-10), DateTime.UtcNow.Date.AddDays(10),
			CancellationToken.None);
		Assert.That(data, Is.Not.Null);
		Assert.That(data.Count(), Is.EqualTo(2));
		data = data.OrderBy(x => x.ScheduleDate);
		Assert.Multiple(() =>
		{
			Assert.That(data.First().ScheduleDate, Is.EqualTo(DateTime.UtcNow.Date.AddDays(-1)));
			Assert.That(data.First().ClassroomId, Is.EqualTo(guidClassroom));
			Assert.That(data.First().LecturerId, Is.EqualTo(guidLecturer));
			Assert.That(data.First().StudentGroupId, Is.EqualTo(guidStudentGroup));
		});
		Assert.Multiple(() =>
		{
			Assert.That(data.Last().ScheduleDate, Is.EqualTo(DateTime.UtcNow.Date.AddDays(1)));
			Assert.That(data.Last().ClassroomId, Is.Null);
			Assert.That(data.Last().LecturerId, Is.Null);
			Assert.That(data.Last().StudentGroupId, Is.Null);
		});
	}

	[Test]
	public async Task Test_RemoveSemesterRecordsAsync()
	{
		var guid1 = Guid.NewGuid();
		var guid2 = Guid.NewGuid();
		await _dbContext.Database.ExecuteSqlAsync($@"
INSERT INTO [dbo].[SemesterRecords]
	([Id], [ClassroomId], [StudentGroupId], [LecturerId], [DisciplineId]
	, [NotParseRecord], [LessonDiscipline], [LessonLecturer], [LessonClassroom], [LessonType], [LessonStudentGroup], [ScheduleDate])
VALUES
	({guid1}, NULL, NULL, NULL, NULL, NULL
	, 'LessonDiscipline_1', 'LessonLecturer', 'LessonClassroom', 1, 'LessonStudentGroup', {DateTime.UtcNow.Date.AddDays(-1)}),
	({guid2}, NULL, NULL, NULL, NULL, NULL
	, 'LessonDiscipline_2', 'LessonLecturer', 'LessonClassroom', 1, 'LessonStudentGroup', {DateTime.UtcNow.Date.AddDays(1)}),
	({Guid.NewGuid()}, NULL, NULL, NULL, NULL, NULL
	, 'LessonDiscipline_3', 'LessonLecturer', 'LessonClassroom', 1, 'LessonStudentGroup', {DateTime.UtcNow.Date.AddDays(-2)}),
	({Guid.NewGuid()}, NULL, NULL, NULL, NULL, NULL
	, 'LessonDiscipline_4', 'LessonLecturer', 'LessonClassroom', 1, 'LessonStudentGroup', {DateTime.UtcNow.Date.AddDays(2)}),
	({Guid.NewGuid()}, NULL, NULL, NULL, NULL, NULL
	, 'LessonDiscipline_5', 'LessonLecturer', 'LessonClassroom', 1, 'LessonStudentGroup', {DateTime.UtcNow.Date.AddDays(-3)}),
	({Guid.NewGuid()}, NULL, NULL, NULL, NULL, NULL
	, 'LessonDiscipline_6', 'LessonLecturer', 'LessonClassroom', 1, 'LessonStudentGroup', {DateTime.UtcNow.Date.AddDays(3)})
");

		var data = await _coreRepository.GetSemesterRecordsAsync(DateTime.UtcNow.Date.AddDays(-5), DateTime.UtcNow.Date.AddDays(5),
			CancellationToken.None);
		Assert.That(data, Is.Not.Null);
		Assert.Multiple(() =>
		{
			Assert.That(data.Count(), Is.EqualTo(6));
			Assert.That(data.FirstOrDefault(x => x.Id == guid1), Is.Not.Null);
			Assert.That(data.FirstOrDefault(x => x.Id == guid2), Is.Not.Null);
		});

		Assert.DoesNotThrowAsync(async () =>
		{
			await _coreRepository.RemoveSemesterRecordAsync(guid1, CancellationToken.None);
			await _coreRepository.RemoveSemesterRecordAsync(guid2, CancellationToken.None);
		});

		data = await _coreRepository.GetSemesterRecordsAsync(DateTime.UtcNow.Date.AddDays(-5), DateTime.UtcNow.Date.AddDays(5),
			CancellationToken.None);
		Assert.That(data, Is.Not.Null);
		Assert.That(data.Count(), Is.EqualTo(4));
	}
}