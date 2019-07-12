using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class DecimalToDoubleInDisciplineLessons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Score",
                table: "DisciplineLessonTaskStudentAccepts",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "MaxBall",
                table: "DisciplineLessonTasks",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Ball",
                table: "DisciplineLessonConductedStudents",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Score",
                table: "DisciplineLessonTaskStudentAccepts",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxBall",
                table: "DisciplineLessonTasks",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ball",
                table: "DisciplineLessonConductedStudents",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
