using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITI_Tanta_Final_Project.Migrations
{
    /// <inheritdoc />
    public partial class timestamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Sessions");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndingTime",
                table: "Sessions",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartingTime",
                table: "Sessions",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndingTime",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "StartingTime",
                table: "Sessions");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
