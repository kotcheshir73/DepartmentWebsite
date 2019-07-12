using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class IndividualPlanUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndividualPlanNIRContractualWorks_Lecturers_LecturerId",
                table: "IndividualPlanNIRContractualWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualPlanNIRScientificArticles_Lecturers_LecturerId",
                table: "IndividualPlanNIRScientificArticles");

            migrationBuilder.RenameColumn(
                name: "LecturerId",
                table: "IndividualPlanNIRScientificArticles",
                newName: "IndividualPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_IndividualPlanNIRScientificArticles_LecturerId",
                table: "IndividualPlanNIRScientificArticles",
                newName: "IX_IndividualPlanNIRScientificArticles_IndividualPlanId");

            migrationBuilder.RenameColumn(
                name: "LecturerId",
                table: "IndividualPlanNIRContractualWorks",
                newName: "IndividualPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_IndividualPlanNIRContractualWorks_LecturerId",
                table: "IndividualPlanNIRContractualWorks",
                newName: "IX_IndividualPlanNIRContractualWorks_IndividualPlanId");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "IndividualPlanNIRScientificArticles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Volume",
                table: "IndividualPlanNIRScientificArticles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "IndividualPlanNIRScientificArticles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "ReadyMark",
                table: "IndividualPlanNIRContractualWorks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualPlanNIRContractualWorks_IndividualPlans_IndividualPlanId",
                table: "IndividualPlanNIRContractualWorks",
                column: "IndividualPlanId",
                principalTable: "IndividualPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualPlanNIRScientificArticles_IndividualPlans_IndividualPlanId",
                table: "IndividualPlanNIRScientificArticles",
                column: "IndividualPlanId",
                principalTable: "IndividualPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndividualPlanNIRContractualWorks_IndividualPlans_IndividualPlanId",
                table: "IndividualPlanNIRContractualWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualPlanNIRScientificArticles_IndividualPlans_IndividualPlanId",
                table: "IndividualPlanNIRScientificArticles");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "IndividualPlanNIRScientificArticles");

            migrationBuilder.RenameColumn(
                name: "IndividualPlanId",
                table: "IndividualPlanNIRScientificArticles",
                newName: "LecturerId");

            migrationBuilder.RenameIndex(
                name: "IX_IndividualPlanNIRScientificArticles_IndividualPlanId",
                table: "IndividualPlanNIRScientificArticles",
                newName: "IX_IndividualPlanNIRScientificArticles_LecturerId");

            migrationBuilder.RenameColumn(
                name: "IndividualPlanId",
                table: "IndividualPlanNIRContractualWorks",
                newName: "LecturerId");

            migrationBuilder.RenameIndex(
                name: "IX_IndividualPlanNIRContractualWorks_IndividualPlanId",
                table: "IndividualPlanNIRContractualWorks",
                newName: "IX_IndividualPlanNIRContractualWorks_LecturerId");

            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "IndividualPlanNIRScientificArticles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Volume",
                table: "IndividualPlanNIRScientificArticles",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "ReadyMark",
                table: "IndividualPlanNIRContractualWorks",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualPlanNIRContractualWorks_Lecturers_LecturerId",
                table: "IndividualPlanNIRContractualWorks",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualPlanNIRScientificArticles_Lecturers_LecturerId",
                table: "IndividualPlanNIRScientificArticles",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
