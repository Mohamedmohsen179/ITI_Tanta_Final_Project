using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITI_Tanta_Final_Project.Migrations
{
    /// <inheritdoc />
    public partial class cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Users_TraineeId",
                table: "Grades");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Users_TraineeId",
                table: "Grades",
                column: "TraineeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Users_TraineeId",
                table: "Grades");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Users_TraineeId",
                table: "Grades",
                column: "TraineeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
