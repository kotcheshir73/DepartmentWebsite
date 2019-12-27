using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class UpdateSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleLessonTimes");

            migrationBuilder.DropTable(
                name: "StreamingLessons");

            migrationBuilder.DropColumn(
                name: "IsStreaming",
                table: "SemesterRecords");

            migrationBuilder.DropColumn(
                name: "IsSubgroup",
                table: "SemesterRecords");

            migrationBuilder.DropColumn(
                name: "IsStreaming",
                table: "OffsetRecords");

            migrationBuilder.RenameColumn(
                name: "DateConsultation",
                table: "ConsultationRecords",
                newName: "DateEndConsultation");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateBeginConsultation",
                table: "ConsultationRecords",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateBeginConsultation",
                table: "ConsultationRecords");

            migrationBuilder.RenameColumn(
                name: "DateEndConsultation",
                table: "ConsultationRecords",
                newName: "DateConsultation");

            migrationBuilder.AddColumn<bool>(
                name: "IsStreaming",
                table: "SemesterRecords",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSubgroup",
                table: "SemesterRecords",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStreaming",
                table: "OffsetRecords",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ScheduleLessonTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateBeginLesson = table.Column<DateTime>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    DateEndLesson = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleLessonTimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StreamingLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateDelete = table.Column<DateTime>(nullable: true),
                    IncomingGroups = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StreamName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamingLessons", x => x.Id);
                });
        }
    }
}
