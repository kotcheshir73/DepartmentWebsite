using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class UpdateOffsetAndSemestrRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultationRecords_SeasonDates_SeasonDatesId",
                table: "ConsultationRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationRecords_SeasonDates_SeasonDatesId",
                table: "ExaminationRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_OffsetRecords_SeasonDates_SeasonDatesId",
                table: "OffsetRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterRecords_SeasonDates_SeasonDatesId",
                table: "SemesterRecords");

            migrationBuilder.DropIndex(
                name: "IX_SemesterRecords_SeasonDatesId",
                table: "SemesterRecords");

            migrationBuilder.DropIndex(
                name: "IX_OffsetRecords_SeasonDatesId",
                table: "OffsetRecords");

            migrationBuilder.DropIndex(
                name: "IX_ExaminationRecords_SeasonDatesId",
                table: "ExaminationRecords");

            migrationBuilder.DropIndex(
                name: "IX_ConsultationRecords_SeasonDatesId",
                table: "ConsultationRecords");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "SemesterRecords");

            migrationBuilder.DropColumn(
                name: "IsFirstHalfSemester",
                table: "SemesterRecords");

            migrationBuilder.DropColumn(
                name: "Lesson",
                table: "SemesterRecords");

            migrationBuilder.DropColumn(
                name: "LessonGroup",
                table: "SemesterRecords");

            migrationBuilder.DropColumn(
                name: "SeasonDatesId",
                table: "SemesterRecords");

            migrationBuilder.DropColumn(
                name: "Week",
                table: "SemesterRecords");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "OffsetRecords");

            migrationBuilder.DropColumn(
                name: "Lesson",
                table: "OffsetRecords");

            migrationBuilder.DropColumn(
                name: "LessonGroup",
                table: "OffsetRecords");

            migrationBuilder.DropColumn(
                name: "SeasonDatesId",
                table: "OffsetRecords");

            migrationBuilder.DropColumn(
                name: "Week",
                table: "OffsetRecords");

            migrationBuilder.DropColumn(
                name: "LessonGroup",
                table: "ExaminationRecords");

            migrationBuilder.DropColumn(
                name: "SeasonDatesId",
                table: "ExaminationRecords");

            migrationBuilder.DropColumn(
                name: "DateBeginConsultation",
                table: "ConsultationRecords");

            migrationBuilder.DropColumn(
                name: "LessonGroup",
                table: "ConsultationRecords");

            migrationBuilder.DropColumn(
                name: "SeasonDatesId",
                table: "ConsultationRecords");

            migrationBuilder.RenameColumn(
                name: "DateExamination",
                table: "ExaminationRecords",
                newName: "ScheduleDate");

            migrationBuilder.RenameColumn(
                name: "DateEndConsultation",
                table: "ConsultationRecords",
                newName: "ScheduleDate");

            migrationBuilder.AlterColumn<string>(
                name: "LessonLecturer",
                table: "SemesterRecords",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LessonDiscipline",
                table: "SemesterRecords",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LessonClassroom",
                table: "SemesterRecords",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LessonStudentGroup",
                table: "SemesterRecords",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduleDate",
                table: "SemesterRecords",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "LessonLecturer",
                table: "OffsetRecords",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LessonDiscipline",
                table: "OffsetRecords",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LessonClassroom",
                table: "OffsetRecords",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LessonStudentGroup",
                table: "OffsetRecords",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LessonType",
                table: "OffsetRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduleDate",
                table: "OffsetRecords",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "LessonLecturer",
                table: "ExaminationRecords",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LessonDiscipline",
                table: "ExaminationRecords",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LessonClassroom",
                table: "ExaminationRecords",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LessonStudentGroup",
                table: "ExaminationRecords",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LessonType",
                table: "ExaminationRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "LessonLecturer",
                table: "ConsultationRecords",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LessonDiscipline",
                table: "ConsultationRecords",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LessonClassroom",
                table: "ConsultationRecords",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConsultationTime",
                table: "ConsultationRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LessonStudentGroup",
                table: "ConsultationRecords",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LessonType",
                table: "ConsultationRecords",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessonStudentGroup",
                table: "SemesterRecords");

            migrationBuilder.DropColumn(
                name: "ScheduleDate",
                table: "SemesterRecords");

            migrationBuilder.DropColumn(
                name: "LessonStudentGroup",
                table: "OffsetRecords");

            migrationBuilder.DropColumn(
                name: "LessonType",
                table: "OffsetRecords");

            migrationBuilder.DropColumn(
                name: "ScheduleDate",
                table: "OffsetRecords");

            migrationBuilder.DropColumn(
                name: "LessonStudentGroup",
                table: "ExaminationRecords");

            migrationBuilder.DropColumn(
                name: "LessonType",
                table: "ExaminationRecords");

            migrationBuilder.DropColumn(
                name: "ConsultationTime",
                table: "ConsultationRecords");

            migrationBuilder.DropColumn(
                name: "LessonStudentGroup",
                table: "ConsultationRecords");

            migrationBuilder.DropColumn(
                name: "LessonType",
                table: "ConsultationRecords");

            migrationBuilder.RenameColumn(
                name: "ScheduleDate",
                table: "ExaminationRecords",
                newName: "DateExamination");

            migrationBuilder.RenameColumn(
                name: "ScheduleDate",
                table: "ConsultationRecords",
                newName: "DateEndConsultation");

            migrationBuilder.AlterColumn<string>(
                name: "LessonLecturer",
                table: "SemesterRecords",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LessonDiscipline",
                table: "SemesterRecords",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LessonClassroom",
                table: "SemesterRecords",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "SemesterRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstHalfSemester",
                table: "SemesterRecords",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Lesson",
                table: "SemesterRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LessonGroup",
                table: "SemesterRecords",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SeasonDatesId",
                table: "SemesterRecords",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Week",
                table: "SemesterRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "LessonLecturer",
                table: "OffsetRecords",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LessonDiscipline",
                table: "OffsetRecords",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LessonClassroom",
                table: "OffsetRecords",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "OffsetRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Lesson",
                table: "OffsetRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LessonGroup",
                table: "OffsetRecords",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SeasonDatesId",
                table: "OffsetRecords",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Week",
                table: "OffsetRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "LessonLecturer",
                table: "ExaminationRecords",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LessonDiscipline",
                table: "ExaminationRecords",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LessonClassroom",
                table: "ExaminationRecords",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "LessonGroup",
                table: "ExaminationRecords",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SeasonDatesId",
                table: "ExaminationRecords",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "LessonLecturer",
                table: "ConsultationRecords",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LessonDiscipline",
                table: "ConsultationRecords",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LessonClassroom",
                table: "ConsultationRecords",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateBeginConsultation",
                table: "ConsultationRecords",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LessonGroup",
                table: "ConsultationRecords",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SeasonDatesId",
                table: "ConsultationRecords",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SemesterRecords_SeasonDatesId",
                table: "SemesterRecords",
                column: "SeasonDatesId");

            migrationBuilder.CreateIndex(
                name: "IX_OffsetRecords_SeasonDatesId",
                table: "OffsetRecords",
                column: "SeasonDatesId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationRecords_SeasonDatesId",
                table: "ExaminationRecords",
                column: "SeasonDatesId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationRecords_SeasonDatesId",
                table: "ConsultationRecords",
                column: "SeasonDatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultationRecords_SeasonDates_SeasonDatesId",
                table: "ConsultationRecords",
                column: "SeasonDatesId",
                principalTable: "SeasonDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationRecords_SeasonDates_SeasonDatesId",
                table: "ExaminationRecords",
                column: "SeasonDatesId",
                principalTable: "SeasonDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OffsetRecords_SeasonDates_SeasonDatesId",
                table: "OffsetRecords",
                column: "SeasonDatesId",
                principalTable: "SeasonDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterRecords_SeasonDates_SeasonDatesId",
                table: "SemesterRecords",
                column: "SeasonDatesId",
                principalTable: "SeasonDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
