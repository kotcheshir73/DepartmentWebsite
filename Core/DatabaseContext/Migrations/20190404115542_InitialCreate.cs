using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicYears",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    ClassroomType = table.Column<int>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    NotUseInSchedule = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrentSettings",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentSettings", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    DisciplineBlockBlueAsteriskName = table.Column<string>(maxLength: 20, nullable: true),
                    DisciplineBlockUseForGrouping = table.Column<bool>(nullable: false),
                    DisciplineBlockOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineBlocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationDirections",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Cipher = table.Column<string>(maxLength: 10, nullable: false),
                    ShortName = table.Column<string>(maxLength: 10, nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDirections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndividualPlanTitles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualPlanTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LecturerPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PostTitle = table.Column<string>(nullable: true),
                    Hours = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialTechnicalValueGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    GroupName = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTechnicalValueGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleLessonTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    DateBeginLesson = table.Column<DateTime>(nullable: false),
                    DateEndLesson = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleLessonTimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Softwares",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SoftwareName = table.Column<string>(nullable: true),
                    SoftwareDescription = table.Column<string>(nullable: true),
                    SoftwareKey = table.Column<string>(nullable: true),
                    SoftwareK = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Softwares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StreamingLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IncomingGroups = table.Column<string>(nullable: false),
                    StreamName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamingLessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeasonDates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    DateBeginFirstHalfSemester = table.Column<DateTime>(nullable: false),
                    DateEndFirstHalfSemester = table.Column<DateTime>(nullable: false),
                    DateBeginSecondHalfSemester = table.Column<DateTime>(nullable: false),
                    DateEndSecondHalfSemester = table.Column<DateTime>(nullable: false),
                    DateBeginOffset = table.Column<DateTime>(nullable: false),
                    DateEndOffset = table.Column<DateTime>(nullable: false),
                    DateBeginExamination = table.Column<DateTime>(nullable: false),
                    DateEndExamination = table.Column<DateTime>(nullable: false),
                    DateBeginPractice = table.Column<DateTime>(nullable: true),
                    DateEndPractice = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeasonDates_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StreamLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    StreamLessonName = table.Column<string>(nullable: true),
                    StreamLessonHours = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StreamLessons_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialTechnicalValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ClassroomId = table.Column<Guid>(nullable: false),
                    InventoryNumber = table.Column<string>(maxLength: 50, nullable: false),
                    FullName = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Cost = table.Column<decimal>(nullable: false),
                    DeleteReason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTechnicalValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialTechnicalValues_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentAccesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    Operation = table.Column<int>(nullable: false),
                    AccessType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentAccesses_DepartmentRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "DepartmentRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DisciplineBlockId = table.Column<Guid>(nullable: false),
                    DisciplineParentId = table.Column<Guid>(nullable: true),
                    IsParent = table.Column<bool>(nullable: false),
                    DisciplineName = table.Column<string>(maxLength: 200, nullable: false),
                    DisciplineShortName = table.Column<string>(maxLength: 20, nullable: true),
                    DisciplineBlueAsteriskName = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplines_DisciplineBlocks_DisciplineBlockId",
                        column: x => x.DisciplineBlockId,
                        principalTable: "DisciplineBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeNorms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    DisciplineBlockId = table.Column<Guid>(nullable: false),
                    TimeNormName = table.Column<string>(maxLength: 50, nullable: false),
                    TimeNormShortName = table.Column<string>(maxLength: 5, nullable: false),
                    TimeNormOrder = table.Column<int>(nullable: false),
                    TimeNormAcademicLevel = table.Column<int>(nullable: true),
                    KindOfLoadName = table.Column<string>(maxLength: 100, nullable: false),
                    KindOfLoadAttributeName = table.Column<string>(maxLength: 10, nullable: true),
                    KindOfLoadBlueAsteriskName = table.Column<string>(maxLength: 100, nullable: true),
                    KindOfLoadBlueAsteriskAttributeName = table.Column<string>(maxLength: 100, nullable: true),
                    KindOfLoadBlueAsteriskPracticName = table.Column<string>(maxLength: 100, nullable: true),
                    KindOfLoadType = table.Column<int>(nullable: false),
                    Hours = table.Column<decimal>(nullable: true),
                    NumKoef = table.Column<decimal>(nullable: true),
                    TimeNormKoef = table.Column<int>(nullable: false),
                    UseInLearningProgress = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeNorms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeNorms_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeNorms_DisciplineBlocks_DisciplineBlockId",
                        column: x => x.DisciplineBlockId,
                        principalTable: "DisciplineBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcademicPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    EducationDirectionId = table.Column<Guid>(nullable: true),
                    AcademicLevel = table.Column<int>(nullable: false),
                    AcademicCourses = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicPlans_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicPlans_EducationDirections_EducationDirectionId",
                        column: x => x.EducationDirectionId,
                        principalTable: "EducationDirections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contingents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    EducationDirectionId = table.Column<Guid>(nullable: false),
                    ContingentName = table.Column<string>(nullable: false),
                    Course = table.Column<int>(nullable: false),
                    CountGroups = table.Column<int>(nullable: false),
                    CountStudetns = table.Column<int>(nullable: false),
                    CountSubgroups = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contingents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contingents_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contingents_EducationDirections_EducationDirectionId",
                        column: x => x.EducationDirectionId,
                        principalTable: "EducationDirections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualPlanKindOfWorks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IndividualPlanTitleId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    TimeNormDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualPlanKindOfWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualPlanKindOfWorks_IndividualPlanTitles_IndividualPlanTitleId",
                        column: x => x.IndividualPlanTitleId,
                        principalTable: "IndividualPlanTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LecturerPostId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    Patronymic = table.Column<string>(maxLength: 30, nullable: false),
                    Abbreviation = table.Column<string>(maxLength: 10, nullable: true),
                    DateBirth = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 250, nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    MobileNumber = table.Column<string>(maxLength: 50, nullable: false),
                    HomeNumber = table.Column<string>(maxLength: 50, nullable: true),
                    Post = table.Column<int>(nullable: false),
                    Rank = table.Column<int>(nullable: false),
                    Rank2 = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lecturers_LecturerPosts_LecturerPostId",
                        column: x => x.LecturerPostId,
                        principalTable: "LecturerPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialTechnicalValueRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MaterialTechnicalValueId = table.Column<Guid>(nullable: false),
                    MaterialTechnicalValueGroupId = table.Column<Guid>(nullable: false),
                    FieldName = table.Column<string>(nullable: true),
                    FieldValue = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTechnicalValueRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialTechnicalValueRecords_MaterialTechnicalValueGroups_MaterialTechnicalValueGroupId",
                        column: x => x.MaterialTechnicalValueGroupId,
                        principalTable: "MaterialTechnicalValueGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialTechnicalValueRecords_MaterialTechnicalValues_MaterialTechnicalValueId",
                        column: x => x.MaterialTechnicalValueId,
                        principalTable: "MaterialTechnicalValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MaterialTechnicalValueId = table.Column<Guid>(nullable: false),
                    SoftwareId = table.Column<Guid>(nullable: false),
                    SetupDescription = table.Column<string>(nullable: true),
                    ClaimNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoftwareRecords_MaterialTechnicalValues_MaterialTechnicalValueId",
                        column: x => x.MaterialTechnicalValueId,
                        principalTable: "MaterialTechnicalValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoftwareRecords_Softwares_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Softwares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DisciplineId = table.Column<Guid>(nullable: false),
                    EducationDirectionId = table.Column<Guid>(nullable: true),
                    Semester = table.Column<int>(nullable: true),
                    ExaminationTemplateName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationTemplates_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationTemplates_EducationDirections_EducationDirectionId",
                        column: x => x.EducationDirectionId,
                        principalTable: "EducationDirections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    EducationDirectionId = table.Column<Guid>(nullable: false),
                    DisciplineId = table.Column<Guid>(nullable: false),
                    TimeNormId = table.Column<Guid>(nullable: false),
                    Semester = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    CountOfPairs = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    DisciplineLessonFile = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplineLessons_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineLessons_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineLessons_EducationDirections_EducationDirectionId",
                        column: x => x.EducationDirectionId,
                        principalTable: "EducationDirections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineLessons_TimeNorms_TimeNormId",
                        column: x => x.TimeNormId,
                        principalTable: "TimeNorms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcademicPlanRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AcademicPlanId = table.Column<Guid>(nullable: false),
                    DisciplineId = table.Column<Guid>(nullable: false),
                    ContingentId = table.Column<Guid>(nullable: true),
                    Semester = table.Column<int>(nullable: true),
                    Zet = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicPlanRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicPlanRecords_AcademicPlans_AcademicPlanId",
                        column: x => x.AcademicPlanId,
                        principalTable: "AcademicPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicPlanRecords_Contingents_ContingentId",
                        column: x => x.ContingentId,
                        principalTable: "Contingents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AcademicPlanRecords_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualPlanNIRContractualWorks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LecturerId = table.Column<Guid>(nullable: false),
                    JobContent = table.Column<string>(nullable: true),
                    Post = table.Column<string>(nullable: true),
                    PlannedTerm = table.Column<string>(nullable: true),
                    ReadyMark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualPlanNIRContractualWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualPlanNIRContractualWorks_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualPlanNIRScientificArticles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LecturerId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TypeOfPublication = table.Column<string>(nullable: true),
                    Volume = table.Column<string>(nullable: true),
                    Publishing = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualPlanNIRScientificArticles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualPlanNIRScientificArticles_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LecturerId = table.Column<Guid>(nullable: false),
                    AcademicYearId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualPlans_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndividualPlans_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LecturerWorkload",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LecturerId = table.Column<Guid>(nullable: false),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    Workload = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerWorkload", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LecturerWorkload_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LecturerWorkload_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EducationDirectionId = table.Column<Guid>(nullable: false),
                    CuratorId = table.Column<Guid>(nullable: true),
                    GroupName = table.Column<string>(maxLength: 20, nullable: false),
                    Course = table.Column<int>(nullable: false),
                    StewardName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentGroups_Lecturers_CuratorId",
                        column: x => x.CuratorId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentGroups_EducationDirections_EducationDirectionId",
                        column: x => x.EducationDirectionId,
                        principalTable: "EducationDirections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationTemplateBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ExaminationTemplateId = table.Column<Guid>(nullable: false),
                    BlockName = table.Column<string>(nullable: true),
                    QuestionTagInTemplate = table.Column<string>(nullable: true),
                    CountQuestionInTicket = table.Column<int>(nullable: false),
                    IsCombine = table.Column<bool>(nullable: false),
                    CombineBlocks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationTemplateBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationTemplateBlocks_ExaminationTemplates_ExaminationTemplateId",
                        column: x => x.ExaminationTemplateId,
                        principalTable: "ExaminationTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationTemplateTickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ExaminationTemplateId = table.Column<Guid>(nullable: false),
                    TicketNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationTemplateTickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationTemplateTickets_ExaminationTemplates_ExaminationTemplateId",
                        column: x => x.ExaminationTemplateId,
                        principalTable: "ExaminationTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ExaminationTemplateId = table.Column<Guid>(nullable: true),
                    XML = table.Column<string>(nullable: true),
                    TemplateName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplates_ExaminationTemplates_ExaminationTemplateId",
                        column: x => x.ExaminationTemplateId,
                        principalTable: "ExaminationTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineLessonTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DisciplineLessonId = table.Column<Guid>(nullable: false),
                    Task = table.Column<string>(nullable: false),
                    IsNecessarily = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    MaxBall = table.Column<decimal>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineLessonTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplineLessonTasks_DisciplineLessons_DisciplineLessonId",
                        column: x => x.DisciplineLessonId,
                        principalTable: "DisciplineLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcademicPlanRecordElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AcademicPlanRecordId = table.Column<Guid>(nullable: false),
                    TimeNormId = table.Column<Guid>(nullable: false),
                    PlanHours = table.Column<decimal>(nullable: false),
                    FactHours = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicPlanRecordElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicPlanRecordElements_AcademicPlanRecords_AcademicPlanRecordId",
                        column: x => x.AcademicPlanRecordId,
                        principalTable: "AcademicPlanRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicPlanRecordElements_TimeNorms_TimeNormId",
                        column: x => x.TimeNormId,
                        principalTable: "TimeNorms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualPlanRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IndividualPlanId = table.Column<Guid>(nullable: false),
                    IndividualPlanKindOfWorkId = table.Column<Guid>(nullable: false),
                    PlanAutumn = table.Column<double>(nullable: false),
                    FactAutumn = table.Column<double>(nullable: false),
                    PlanSpring = table.Column<double>(nullable: false),
                    FactSpring = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualPlanRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualPlanRecords_IndividualPlans_IndividualPlanId",
                        column: x => x.IndividualPlanId,
                        principalTable: "IndividualPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndividualPlanRecords_IndividualPlanKindOfWorks_IndividualPlanKindOfWorkId",
                        column: x => x.IndividualPlanKindOfWorkId,
                        principalTable: "IndividualPlanKindOfWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsultationRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SeasonDatesId = table.Column<Guid>(nullable: false),
                    ClassroomId = table.Column<Guid>(nullable: true),
                    StudentGroupId = table.Column<Guid>(nullable: true),
                    LecturerId = table.Column<Guid>(nullable: true),
                    DisciplineId = table.Column<Guid>(nullable: true),
                    NotParseRecord = table.Column<string>(nullable: true),
                    LessonDiscipline = table.Column<string>(nullable: true),
                    LessonLecturer = table.Column<string>(nullable: true),
                    LessonGroup = table.Column<string>(nullable: true),
                    LessonClassroom = table.Column<string>(nullable: true),
                    DateConsultation = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultationRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultationRecords_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsultationRecords_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsultationRecords_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsultationRecords_SeasonDates_SeasonDatesId",
                        column: x => x.SeasonDatesId,
                        principalTable: "SeasonDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsultationRecords_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineLessonConducteds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DisciplineLessonId = table.Column<Guid>(nullable: false),
                    StudentGroupId = table.Column<Guid>(nullable: false),
                    Subgroup = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineLessonConducteds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplineLessonConducteds_DisciplineLessons_DisciplineLessonId",
                        column: x => x.DisciplineLessonId,
                        principalTable: "DisciplineLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineLessonConducteds_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineTimeDistributions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AcademicPlanRecordId = table.Column<Guid>(nullable: false),
                    StudentGroupId = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    CommentWishesOfTeacher = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineTimeDistributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplineTimeDistributions_AcademicPlanRecords_AcademicPlanRecordId",
                        column: x => x.AcademicPlanRecordId,
                        principalTable: "AcademicPlanRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineTimeDistributions_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SeasonDatesId = table.Column<Guid>(nullable: false),
                    ClassroomId = table.Column<Guid>(nullable: true),
                    StudentGroupId = table.Column<Guid>(nullable: true),
                    LecturerId = table.Column<Guid>(nullable: true),
                    DisciplineId = table.Column<Guid>(nullable: true),
                    NotParseRecord = table.Column<string>(nullable: true),
                    LessonDiscipline = table.Column<string>(nullable: true),
                    LessonLecturer = table.Column<string>(nullable: true),
                    LessonGroup = table.Column<string>(nullable: true),
                    LessonClassroom = table.Column<string>(nullable: true),
                    ConsultationClassroomId = table.Column<Guid>(nullable: true),
                    DateConsultation = table.Column<DateTime>(nullable: false),
                    DateExamination = table.Column<DateTime>(nullable: false),
                    LessonConsultationClassroom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationRecords_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminationRecords_Classrooms_ConsultationClassroomId",
                        column: x => x.ConsultationClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminationRecords_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminationRecords_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminationRecords_SeasonDates_SeasonDatesId",
                        column: x => x.SeasonDatesId,
                        principalTable: "SeasonDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationRecords_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OffsetRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SeasonDatesId = table.Column<Guid>(nullable: false),
                    ClassroomId = table.Column<Guid>(nullable: true),
                    StudentGroupId = table.Column<Guid>(nullable: true),
                    LecturerId = table.Column<Guid>(nullable: true),
                    DisciplineId = table.Column<Guid>(nullable: true),
                    NotParseRecord = table.Column<string>(nullable: true),
                    LessonDiscipline = table.Column<string>(nullable: true),
                    LessonLecturer = table.Column<string>(nullable: true),
                    LessonGroup = table.Column<string>(nullable: true),
                    LessonClassroom = table.Column<string>(nullable: true),
                    Week = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    Lesson = table.Column<int>(nullable: false),
                    IsStreaming = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OffsetRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OffsetRecords_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OffsetRecords_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OffsetRecords_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OffsetRecords_SeasonDates_SeasonDatesId",
                        column: x => x.SeasonDatesId,
                        principalTable: "SeasonDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OffsetRecords_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SemesterRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SeasonDatesId = table.Column<Guid>(nullable: false),
                    ClassroomId = table.Column<Guid>(nullable: true),
                    StudentGroupId = table.Column<Guid>(nullable: true),
                    LecturerId = table.Column<Guid>(nullable: true),
                    DisciplineId = table.Column<Guid>(nullable: true),
                    NotParseRecord = table.Column<string>(nullable: true),
                    LessonDiscipline = table.Column<string>(nullable: true),
                    LessonLecturer = table.Column<string>(nullable: true),
                    LessonGroup = table.Column<string>(nullable: true),
                    LessonClassroom = table.Column<string>(nullable: true),
                    IsFirstHalfSemester = table.Column<bool>(nullable: false),
                    Week = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    Lesson = table.Column<int>(nullable: false),
                    LessonType = table.Column<int>(nullable: false),
                    IsStreaming = table.Column<bool>(nullable: false),
                    IsSubgroup = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SemesterRecords_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SemesterRecords_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SemesterRecords_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SemesterRecords_SeasonDates_SeasonDatesId",
                        column: x => x.SeasonDatesId,
                        principalTable: "SeasonDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterRecords_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Statements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LecturerId = table.Column<Guid>(nullable: false),
                    DisciplineId = table.Column<Guid>(nullable: false),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    StudentGroupId = table.Column<Guid>(nullable: false),
                    TypeOfTest = table.Column<int>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false),
                    AcademicPlanRecordId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statements_AcademicYears_AcademicPlanRecordId",
                        column: x => x.AcademicPlanRecordId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Statements_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statements_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statements_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NumberOfBook = table.Column<string>(maxLength: 10, nullable: false),
                    StudentGroupId = table.Column<Guid>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    Patronymic = table.Column<string>(maxLength: 30, nullable: true),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    StudentState = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    IsSteward = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationTemplateBlockQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ExaminationTemplateBlockId = table.Column<Guid>(nullable: false),
                    QuestionNumber = table.Column<int>(nullable: false),
                    QuestionText = table.Column<string>(nullable: true),
                    QuestionImage = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationTemplateBlockQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationTemplateBlockQuestions_ExaminationTemplateBlocks_ExaminationTemplateBlockId",
                        column: x => x.ExaminationTemplateBlockId,
                        principalTable: "ExaminationTemplateBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineLessonTaskVariants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DisciplineLessonTaskId = table.Column<Guid>(nullable: false),
                    VariantNumber = table.Column<string>(nullable: false),
                    VariantTask = table.Column<string>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineLessonTaskVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplineLessonTaskVariants_DisciplineLessonTasks_DisciplineLessonTaskId",
                        column: x => x.DisciplineLessonTaskId,
                        principalTable: "DisciplineLessonTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcademicPlanRecordMissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LecturerId = table.Column<Guid>(nullable: false),
                    AcademicPlanRecordElementId = table.Column<Guid>(nullable: false),
                    Hours = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicPlanRecordMissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicPlanRecordMissions_AcademicPlanRecordElements_AcademicPlanRecordElementId",
                        column: x => x.AcademicPlanRecordElementId,
                        principalTable: "AcademicPlanRecordElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicPlanRecordMissions_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StreamLessonRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StreamLessonId = table.Column<Guid>(nullable: false),
                    AcademicPlanRecordElementId = table.Column<Guid>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamLessonRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StreamLessonRecords_AcademicPlanRecordElements_AcademicPlanRecordElementId",
                        column: x => x.AcademicPlanRecordElementId,
                        principalTable: "AcademicPlanRecordElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StreamLessonRecords_StreamLessons_StreamLessonId",
                        column: x => x.StreamLessonId,
                        principalTable: "StreamLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineTimeDistributionClassrooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DisciplineTimeDistributionId = table.Column<Guid>(nullable: false),
                    TimeNormId = table.Column<Guid>(nullable: false),
                    ClassroomDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineTimeDistributionClassrooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplineTimeDistributionClassrooms_DisciplineTimeDistributions_DisciplineTimeDistributionId",
                        column: x => x.DisciplineTimeDistributionId,
                        principalTable: "DisciplineTimeDistributions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineTimeDistributionClassrooms_TimeNorms_TimeNormId",
                        column: x => x.TimeNormId,
                        principalTable: "TimeNorms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineTimeDistributionRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DisciplineTimeDistributionId = table.Column<Guid>(nullable: false),
                    TimeNormId = table.Column<Guid>(nullable: false),
                    WeekNumber = table.Column<int>(nullable: false),
                    Hours = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineTimeDistributionRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplineTimeDistributionRecords_DisciplineTimeDistributions_DisciplineTimeDistributionId",
                        column: x => x.DisciplineTimeDistributionId,
                        principalTable: "DisciplineTimeDistributions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineTimeDistributionRecords_TimeNorms_TimeNormId",
                        column: x => x.TimeNormId,
                        principalTable: "TimeNorms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    StudentId = table.Column<Guid>(nullable: true),
                    LecturerId = table.Column<Guid>(nullable: true),
                    Avatar = table.Column<byte[]>(nullable: true),
                    DateLastVisit = table.Column<DateTime>(nullable: true),
                    DateBanned = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentUsers_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentUsers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineLessonConductedStudents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DisciplineLessonConductedId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Ball = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineLessonConductedStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplineLessonConductedStudents_DisciplineLessonConducteds_DisciplineLessonConductedId",
                        column: x => x.DisciplineLessonConductedId,
                        principalTable: "DisciplineLessonConducteds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineLessonConductedStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineLessonTaskStudentAccepts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DisciplineLessonTaskId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    Result = table.Column<int>(nullable: false),
                    Task = table.Column<string>(nullable: false),
                    DateAccept = table.Column<DateTime>(nullable: false),
                    Score = table.Column<decimal>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Log = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineLessonTaskStudentAccepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplineLessonTaskStudentAccepts_DisciplineLessonTasks_DisciplineLessonTaskId",
                        column: x => x.DisciplineLessonTaskId,
                        principalTable: "DisciplineLessonTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineLessonTaskStudentAccepts_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineStudentRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DisciplineId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    Semester = table.Column<int>(nullable: false),
                    Variant = table.Column<string>(nullable: false),
                    SubGroup = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineStudentRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplineStudentRecords_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineStudentRecords_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    LecturerId = table.Column<Guid>(nullable: false),
                    DisciplineId = table.Column<Guid>(nullable: false),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    StudentGroupId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    TypeOfTest = table.Column<int>(nullable: false),
                    Score = table.Column<string>(nullable: true),
                    AcademicPlanRecordId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationLists_AcademicYears_AcademicPlanRecordId",
                        column: x => x.AcademicPlanRecordId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminationLists_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationLists_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationLists_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationLists_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatementRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StatementId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Score = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatementRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatementRecords_Statements_StatementId",
                        column: x => x.StatementId,
                        principalTable: "Statements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatementRecords_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentHistorys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    TextMessage = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentHistorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentHistorys_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationTemplateTicketQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ExaminationTemplateTicketId = table.Column<Guid>(nullable: false),
                    ExaminationTemplateBlockQuestionId = table.Column<Guid>(nullable: false),
                    ExaminationTemplateBlockId = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationTemplateTicketQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationTemplateTicketQuestions_ExaminationTemplateBlockQuestions_ExaminationTemplateBlockQuestionId",
                        column: x => x.ExaminationTemplateBlockQuestionId,
                        principalTable: "ExaminationTemplateBlockQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationTemplateTicketQuestions_ExaminationTemplateTickets_ExaminationTemplateTicketId",
                        column: x => x.ExaminationTemplateTicketId,
                        principalTable: "ExaminationTemplateTickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentUserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentUserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentUserRoles_DepartmentRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "DepartmentRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentUserRoles_DepartmentUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "DepartmentUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketTemplateParagraphs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TicketTemplateBodyId = table.Column<Guid>(nullable: true),
                    TicketTemplateTableCellId = table.Column<Guid>(nullable: true),
                    ParagraphFormatId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateParagraphs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketTemplateTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TicketTemplateBodyId = table.Column<Guid>(nullable: true),
                    PropertiesId = table.Column<Guid>(nullable: true),
                    ColumnsId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketTemplateBodies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BodyFormatId = table.Column<Guid>(nullable: true),
                    TicketTemplateId = table.Column<Guid>(nullable: true),
                    BodyName = table.Column<string>(nullable: true),
                    SectName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateBodies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateBodies_TicketTemplates_TicketTemplateId",
                        column: x => x.TicketTemplateId,
                        principalTable: "TicketTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketTemplateElementaryAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TicketTemplateParagraphDataId = table.Column<Guid>(nullable: true),
                    TicketTemplateParagraphId = table.Column<Guid>(nullable: true),
                    TicketTemplateTableRowId = table.Column<Guid>(nullable: true),
                    TicketTemplateElementaryUnitId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateElementaryAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateElementaryAttributes_TicketTemplateParagraphs_TicketTemplateParagraphId",
                        column: x => x.TicketTemplateParagraphId,
                        principalTable: "TicketTemplateParagraphs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketTemplateParagraphDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TicketTemplateParagraphId = table.Column<Guid>(nullable: true),
                    FontId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TextName = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateParagraphDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateParagraphDatas_TicketTemplateParagraphs_TicketTemplateParagraphId",
                        column: x => x.TicketTemplateParagraphId,
                        principalTable: "TicketTemplateParagraphs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketTemplateElementaryUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TicketTemplateParagraphDataId = table.Column<Guid>(nullable: true),
                    ParentElementaryUnitId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateElementaryUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateElementaryUnits_TicketTemplateElementaryUnits_ParentElementaryUnitId",
                        column: x => x.ParentElementaryUnitId,
                        principalTable: "TicketTemplateElementaryUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketTemplateElementaryUnits_TicketTemplateParagraphDatas_TicketTemplateParagraphDataId",
                        column: x => x.TicketTemplateParagraphDataId,
                        principalTable: "TicketTemplateParagraphDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketTemplateTableRows",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TicketTemplateTableId = table.Column<Guid>(nullable: true),
                    PropertiesId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateTableRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateTableRows_TicketTemplateElementaryUnits_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "TicketTemplateElementaryUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketTemplateTableRows_TicketTemplateTables_TicketTemplateTableId",
                        column: x => x.TicketTemplateTableId,
                        principalTable: "TicketTemplateTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketTemplateTableCells",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TicketTemplateTableRowId = table.Column<Guid>(nullable: true),
                    PropertiesId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTemplateTableCells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTemplateTableCells_TicketTemplateElementaryUnits_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "TicketTemplateElementaryUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketTemplateTableCells_TicketTemplateTableRows_TicketTemplateTableRowId",
                        column: x => x.TicketTemplateTableRowId,
                        principalTable: "TicketTemplateTableRows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicPlanRecordElements_AcademicPlanRecordId",
                table: "AcademicPlanRecordElements",
                column: "AcademicPlanRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicPlanRecordElements_TimeNormId",
                table: "AcademicPlanRecordElements",
                column: "TimeNormId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicPlanRecordMissions_AcademicPlanRecordElementId",
                table: "AcademicPlanRecordMissions",
                column: "AcademicPlanRecordElementId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicPlanRecordMissions_LecturerId",
                table: "AcademicPlanRecordMissions",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicPlanRecords_AcademicPlanId",
                table: "AcademicPlanRecords",
                column: "AcademicPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicPlanRecords_ContingentId",
                table: "AcademicPlanRecords",
                column: "ContingentId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicPlanRecords_DisciplineId",
                table: "AcademicPlanRecords",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicPlans_AcademicYearId",
                table: "AcademicPlans",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicPlans_EducationDirectionId",
                table: "AcademicPlans",
                column: "EducationDirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationRecords_ClassroomId",
                table: "ConsultationRecords",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationRecords_DisciplineId",
                table: "ConsultationRecords",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationRecords_LecturerId",
                table: "ConsultationRecords",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationRecords_SeasonDatesId",
                table: "ConsultationRecords",
                column: "SeasonDatesId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationRecords_StudentGroupId",
                table: "ConsultationRecords",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Contingents_AcademicYearId",
                table: "Contingents",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Contingents_EducationDirectionId",
                table: "Contingents",
                column: "EducationDirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentAccesses_RoleId",
                table: "DepartmentAccesses",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUserRoles_RoleId",
                table: "DepartmentUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUserRoles_UserId",
                table: "DepartmentUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUsers_LecturerId",
                table: "DepartmentUsers",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUsers_StudentId",
                table: "DepartmentUsers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineLessonConducteds_DisciplineLessonId",
                table: "DisciplineLessonConducteds",
                column: "DisciplineLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineLessonConducteds_StudentGroupId",
                table: "DisciplineLessonConducteds",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineLessonConductedStudents_DisciplineLessonConductedId",
                table: "DisciplineLessonConductedStudents",
                column: "DisciplineLessonConductedId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineLessonConductedStudents_StudentId",
                table: "DisciplineLessonConductedStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineLessons_AcademicYearId",
                table: "DisciplineLessons",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineLessons_DisciplineId",
                table: "DisciplineLessons",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineLessons_EducationDirectionId",
                table: "DisciplineLessons",
                column: "EducationDirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineLessons_TimeNormId",
                table: "DisciplineLessons",
                column: "TimeNormId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineLessonTasks_DisciplineLessonId",
                table: "DisciplineLessonTasks",
                column: "DisciplineLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineLessonTaskStudentAccepts_DisciplineLessonTaskId",
                table: "DisciplineLessonTaskStudentAccepts",
                column: "DisciplineLessonTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineLessonTaskStudentAccepts_StudentId",
                table: "DisciplineLessonTaskStudentAccepts",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineLessonTaskVariants_DisciplineLessonTaskId",
                table: "DisciplineLessonTaskVariants",
                column: "DisciplineLessonTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_DisciplineBlockId",
                table: "Disciplines",
                column: "DisciplineBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineStudentRecords_DisciplineId",
                table: "DisciplineStudentRecords",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineStudentRecords_StudentId",
                table: "DisciplineStudentRecords",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineTimeDistributionClassrooms_DisciplineTimeDistributionId",
                table: "DisciplineTimeDistributionClassrooms",
                column: "DisciplineTimeDistributionId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineTimeDistributionClassrooms_TimeNormId",
                table: "DisciplineTimeDistributionClassrooms",
                column: "TimeNormId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineTimeDistributionRecords_DisciplineTimeDistributionId",
                table: "DisciplineTimeDistributionRecords",
                column: "DisciplineTimeDistributionId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineTimeDistributionRecords_TimeNormId",
                table: "DisciplineTimeDistributionRecords",
                column: "TimeNormId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineTimeDistributions_AcademicPlanRecordId",
                table: "DisciplineTimeDistributions",
                column: "AcademicPlanRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineTimeDistributions_StudentGroupId",
                table: "DisciplineTimeDistributions",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationLists_AcademicPlanRecordId",
                table: "ExaminationLists",
                column: "AcademicPlanRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationLists_DisciplineId",
                table: "ExaminationLists",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationLists_LecturerId",
                table: "ExaminationLists",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationLists_StudentGroupId",
                table: "ExaminationLists",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationLists_StudentId",
                table: "ExaminationLists",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationRecords_ClassroomId",
                table: "ExaminationRecords",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationRecords_ConsultationClassroomId",
                table: "ExaminationRecords",
                column: "ConsultationClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationRecords_DisciplineId",
                table: "ExaminationRecords",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationRecords_LecturerId",
                table: "ExaminationRecords",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationRecords_SeasonDatesId",
                table: "ExaminationRecords",
                column: "SeasonDatesId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationRecords_StudentGroupId",
                table: "ExaminationRecords",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationTemplateBlockQuestions_ExaminationTemplateBlockId",
                table: "ExaminationTemplateBlockQuestions",
                column: "ExaminationTemplateBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationTemplateBlocks_ExaminationTemplateId",
                table: "ExaminationTemplateBlocks",
                column: "ExaminationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationTemplates_DisciplineId",
                table: "ExaminationTemplates",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationTemplates_EducationDirectionId",
                table: "ExaminationTemplates",
                column: "EducationDirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationTemplateTicketQuestions_ExaminationTemplateBlockQuestionId",
                table: "ExaminationTemplateTicketQuestions",
                column: "ExaminationTemplateBlockQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationTemplateTicketQuestions_ExaminationTemplateTicketId",
                table: "ExaminationTemplateTicketQuestions",
                column: "ExaminationTemplateTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationTemplateTickets_ExaminationTemplateId",
                table: "ExaminationTemplateTickets",
                column: "ExaminationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanKindOfWorks_IndividualPlanTitleId",
                table: "IndividualPlanKindOfWorks",
                column: "IndividualPlanTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanNIRContractualWorks_LecturerId",
                table: "IndividualPlanNIRContractualWorks",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanNIRScientificArticles_LecturerId",
                table: "IndividualPlanNIRScientificArticles",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanRecords_IndividualPlanId",
                table: "IndividualPlanRecords",
                column: "IndividualPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanRecords_IndividualPlanKindOfWorkId",
                table: "IndividualPlanRecords",
                column: "IndividualPlanKindOfWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlans_AcademicYearId",
                table: "IndividualPlans",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlans_LecturerId",
                table: "IndividualPlans",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_LecturerPostId",
                table: "Lecturers",
                column: "LecturerPostId");

            migrationBuilder.CreateIndex(
                name: "IX_LecturerWorkload_AcademicYearId",
                table: "LecturerWorkload",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_LecturerWorkload_LecturerId",
                table: "LecturerWorkload",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTechnicalValueRecords_MaterialTechnicalValueGroupId",
                table: "MaterialTechnicalValueRecords",
                column: "MaterialTechnicalValueGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTechnicalValueRecords_MaterialTechnicalValueId",
                table: "MaterialTechnicalValueRecords",
                column: "MaterialTechnicalValueId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTechnicalValues_ClassroomId",
                table: "MaterialTechnicalValues",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_OffsetRecords_ClassroomId",
                table: "OffsetRecords",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_OffsetRecords_DisciplineId",
                table: "OffsetRecords",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_OffsetRecords_LecturerId",
                table: "OffsetRecords",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_OffsetRecords_SeasonDatesId",
                table: "OffsetRecords",
                column: "SeasonDatesId");

            migrationBuilder.CreateIndex(
                name: "IX_OffsetRecords_StudentGroupId",
                table: "OffsetRecords",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonDates_AcademicYearId",
                table: "SeasonDates",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterRecords_ClassroomId",
                table: "SemesterRecords",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterRecords_DisciplineId",
                table: "SemesterRecords",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterRecords_LecturerId",
                table: "SemesterRecords",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterRecords_SeasonDatesId",
                table: "SemesterRecords",
                column: "SeasonDatesId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterRecords_StudentGroupId",
                table: "SemesterRecords",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareRecords_MaterialTechnicalValueId",
                table: "SoftwareRecords",
                column: "MaterialTechnicalValueId");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareRecords_SoftwareId",
                table: "SoftwareRecords",
                column: "SoftwareId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementRecords_StatementId",
                table: "StatementRecords",
                column: "StatementId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementRecords_StudentId",
                table: "StatementRecords",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_AcademicPlanRecordId",
                table: "Statements",
                column: "AcademicPlanRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_DisciplineId",
                table: "Statements",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_LecturerId",
                table: "Statements",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_StudentGroupId",
                table: "Statements",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StreamLessonRecords_AcademicPlanRecordElementId",
                table: "StreamLessonRecords",
                column: "AcademicPlanRecordElementId");

            migrationBuilder.CreateIndex(
                name: "IX_StreamLessonRecords_StreamLessonId",
                table: "StreamLessonRecords",
                column: "StreamLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_StreamLessons_AcademicYearId",
                table: "StreamLessons",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_CuratorId",
                table: "StudentGroups",
                column: "CuratorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_EducationDirectionId",
                table: "StudentGroups",
                column: "EducationDirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHistorys_StudentId",
                table: "StudentHistorys",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentGroupId",
                table: "Students",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateBodies_BodyFormatId",
                table: "TicketTemplateBodies",
                column: "BodyFormatId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateBodies_TicketTemplateId",
                table: "TicketTemplateBodies",
                column: "TicketTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateElementaryAttributes_TicketTemplateElementaryUnitId",
                table: "TicketTemplateElementaryAttributes",
                column: "TicketTemplateElementaryUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateElementaryAttributes_TicketTemplateParagraphDataId",
                table: "TicketTemplateElementaryAttributes",
                column: "TicketTemplateParagraphDataId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateElementaryAttributes_TicketTemplateParagraphId",
                table: "TicketTemplateElementaryAttributes",
                column: "TicketTemplateParagraphId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateElementaryAttributes_TicketTemplateTableRowId",
                table: "TicketTemplateElementaryAttributes",
                column: "TicketTemplateTableRowId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateElementaryUnits_ParentElementaryUnitId",
                table: "TicketTemplateElementaryUnits",
                column: "ParentElementaryUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateElementaryUnits_TicketTemplateParagraphDataId",
                table: "TicketTemplateElementaryUnits",
                column: "TicketTemplateParagraphDataId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphDatas_FontId",
                table: "TicketTemplateParagraphDatas",
                column: "FontId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphDatas_TicketTemplateParagraphId",
                table: "TicketTemplateParagraphDatas",
                column: "TicketTemplateParagraphId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphs_ParagraphFormatId",
                table: "TicketTemplateParagraphs",
                column: "ParagraphFormatId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphs_TicketTemplateBodyId",
                table: "TicketTemplateParagraphs",
                column: "TicketTemplateBodyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateParagraphs_TicketTemplateTableCellId",
                table: "TicketTemplateParagraphs",
                column: "TicketTemplateTableCellId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplates_ExaminationTemplateId",
                table: "TicketTemplates",
                column: "ExaminationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTableCells_PropertiesId",
                table: "TicketTemplateTableCells",
                column: "PropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTableCells_TicketTemplateTableRowId",
                table: "TicketTemplateTableCells",
                column: "TicketTemplateTableRowId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTableRows_PropertiesId",
                table: "TicketTemplateTableRows",
                column: "PropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTableRows_TicketTemplateTableId",
                table: "TicketTemplateTableRows",
                column: "TicketTemplateTableId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTables_ColumnsId",
                table: "TicketTemplateTables",
                column: "ColumnsId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTables_PropertiesId",
                table: "TicketTemplateTables",
                column: "PropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTemplateTables_TicketTemplateBodyId",
                table: "TicketTemplateTables",
                column: "TicketTemplateBodyId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeNorms_AcademicYearId",
                table: "TimeNorms",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeNorms_DisciplineBlockId",
                table: "TimeNorms",
                column: "DisciplineBlockId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateParagraphs_TicketTemplateElementaryUnits_ParagraphFormatId",
                table: "TicketTemplateParagraphs",
                column: "ParagraphFormatId",
                principalTable: "TicketTemplateElementaryUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateParagraphs_TicketTemplateBodies_TicketTemplateBodyId",
                table: "TicketTemplateParagraphs",
                column: "TicketTemplateBodyId",
                principalTable: "TicketTemplateBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateParagraphs_TicketTemplateTableCells_TicketTemplateTableCellId",
                table: "TicketTemplateParagraphs",
                column: "TicketTemplateTableCellId",
                principalTable: "TicketTemplateTableCells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateTables_TicketTemplateElementaryUnits_ColumnsId",
                table: "TicketTemplateTables",
                column: "ColumnsId",
                principalTable: "TicketTemplateElementaryUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateTables_TicketTemplateElementaryUnits_PropertiesId",
                table: "TicketTemplateTables",
                column: "PropertiesId",
                principalTable: "TicketTemplateElementaryUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateTables_TicketTemplateBodies_TicketTemplateBodyId",
                table: "TicketTemplateTables",
                column: "TicketTemplateBodyId",
                principalTable: "TicketTemplateBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateBodies_TicketTemplateElementaryUnits_BodyFormatId",
                table: "TicketTemplateBodies",
                column: "BodyFormatId",
                principalTable: "TicketTemplateElementaryUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateElementaryAttributes_TicketTemplateElementaryUnits_TicketTemplateElementaryUnitId",
                table: "TicketTemplateElementaryAttributes",
                column: "TicketTemplateElementaryUnitId",
                principalTable: "TicketTemplateElementaryUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateElementaryAttributes_TicketTemplateParagraphDatas_TicketTemplateParagraphDataId",
                table: "TicketTemplateElementaryAttributes",
                column: "TicketTemplateParagraphDataId",
                principalTable: "TicketTemplateParagraphDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateElementaryAttributes_TicketTemplateTableRows_TicketTemplateTableRowId",
                table: "TicketTemplateElementaryAttributes",
                column: "TicketTemplateTableRowId",
                principalTable: "TicketTemplateTableRows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTemplateParagraphDatas_TicketTemplateElementaryUnits_FontId",
                table: "TicketTemplateParagraphDatas",
                column: "FontId",
                principalTable: "TicketTemplateElementaryUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationTemplates_Disciplines_DisciplineId",
                table: "ExaminationTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationTemplates_EducationDirections_EducationDirectionId",
                table: "ExaminationTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplates_ExaminationTemplates_ExaminationTemplateId",
                table: "TicketTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateBodies_TicketTemplateElementaryUnits_BodyFormatId",
                table: "TicketTemplateBodies");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateParagraphDatas_TicketTemplateElementaryUnits_FontId",
                table: "TicketTemplateParagraphDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateParagraphs_TicketTemplateElementaryUnits_ParagraphFormatId",
                table: "TicketTemplateParagraphs");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateTableCells_TicketTemplateElementaryUnits_PropertiesId",
                table: "TicketTemplateTableCells");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateTableRows_TicketTemplateElementaryUnits_PropertiesId",
                table: "TicketTemplateTableRows");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateTables_TicketTemplateElementaryUnits_ColumnsId",
                table: "TicketTemplateTables");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTemplateTables_TicketTemplateElementaryUnits_PropertiesId",
                table: "TicketTemplateTables");

            migrationBuilder.DropTable(
                name: "AcademicPlanRecordMissions");

            migrationBuilder.DropTable(
                name: "ConsultationRecords");

            migrationBuilder.DropTable(
                name: "CurrentSettings");

            migrationBuilder.DropTable(
                name: "DepartmentAccesses");

            migrationBuilder.DropTable(
                name: "DepartmentUserRoles");

            migrationBuilder.DropTable(
                name: "DisciplineLessonConductedStudents");

            migrationBuilder.DropTable(
                name: "DisciplineLessonTaskStudentAccepts");

            migrationBuilder.DropTable(
                name: "DisciplineLessonTaskVariants");

            migrationBuilder.DropTable(
                name: "DisciplineStudentRecords");

            migrationBuilder.DropTable(
                name: "DisciplineTimeDistributionClassrooms");

            migrationBuilder.DropTable(
                name: "DisciplineTimeDistributionRecords");

            migrationBuilder.DropTable(
                name: "ExaminationLists");

            migrationBuilder.DropTable(
                name: "ExaminationRecords");

            migrationBuilder.DropTable(
                name: "ExaminationTemplateTicketQuestions");

            migrationBuilder.DropTable(
                name: "IndividualPlanNIRContractualWorks");

            migrationBuilder.DropTable(
                name: "IndividualPlanNIRScientificArticles");

            migrationBuilder.DropTable(
                name: "IndividualPlanRecords");

            migrationBuilder.DropTable(
                name: "LecturerWorkload");

            migrationBuilder.DropTable(
                name: "MaterialTechnicalValueRecords");

            migrationBuilder.DropTable(
                name: "OffsetRecords");

            migrationBuilder.DropTable(
                name: "ScheduleLessonTimes");

            migrationBuilder.DropTable(
                name: "SemesterRecords");

            migrationBuilder.DropTable(
                name: "SoftwareRecords");

            migrationBuilder.DropTable(
                name: "StatementRecords");

            migrationBuilder.DropTable(
                name: "StreamingLessons");

            migrationBuilder.DropTable(
                name: "StreamLessonRecords");

            migrationBuilder.DropTable(
                name: "StudentHistorys");

            migrationBuilder.DropTable(
                name: "TicketTemplateElementaryAttributes");

            migrationBuilder.DropTable(
                name: "DepartmentRoles");

            migrationBuilder.DropTable(
                name: "DepartmentUsers");

            migrationBuilder.DropTable(
                name: "DisciplineLessonConducteds");

            migrationBuilder.DropTable(
                name: "DisciplineLessonTasks");

            migrationBuilder.DropTable(
                name: "DisciplineTimeDistributions");

            migrationBuilder.DropTable(
                name: "ExaminationTemplateBlockQuestions");

            migrationBuilder.DropTable(
                name: "ExaminationTemplateTickets");

            migrationBuilder.DropTable(
                name: "IndividualPlans");

            migrationBuilder.DropTable(
                name: "IndividualPlanKindOfWorks");

            migrationBuilder.DropTable(
                name: "MaterialTechnicalValueGroups");

            migrationBuilder.DropTable(
                name: "SeasonDates");

            migrationBuilder.DropTable(
                name: "MaterialTechnicalValues");

            migrationBuilder.DropTable(
                name: "Softwares");

            migrationBuilder.DropTable(
                name: "Statements");

            migrationBuilder.DropTable(
                name: "AcademicPlanRecordElements");

            migrationBuilder.DropTable(
                name: "StreamLessons");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "DisciplineLessons");

            migrationBuilder.DropTable(
                name: "ExaminationTemplateBlocks");

            migrationBuilder.DropTable(
                name: "IndividualPlanTitles");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "AcademicPlanRecords");

            migrationBuilder.DropTable(
                name: "StudentGroups");

            migrationBuilder.DropTable(
                name: "TimeNorms");

            migrationBuilder.DropTable(
                name: "AcademicPlans");

            migrationBuilder.DropTable(
                name: "Contingents");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "AcademicYears");

            migrationBuilder.DropTable(
                name: "LecturerPosts");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "DisciplineBlocks");

            migrationBuilder.DropTable(
                name: "EducationDirections");

            migrationBuilder.DropTable(
                name: "ExaminationTemplates");

            migrationBuilder.DropTable(
                name: "TicketTemplateElementaryUnits");

            migrationBuilder.DropTable(
                name: "TicketTemplateParagraphDatas");

            migrationBuilder.DropTable(
                name: "TicketTemplateParagraphs");

            migrationBuilder.DropTable(
                name: "TicketTemplateTableCells");

            migrationBuilder.DropTable(
                name: "TicketTemplateTableRows");

            migrationBuilder.DropTable(
                name: "TicketTemplateTables");

            migrationBuilder.DropTable(
                name: "TicketTemplateBodies");

            migrationBuilder.DropTable(
                name: "TicketTemplates");
        }
    }
}
