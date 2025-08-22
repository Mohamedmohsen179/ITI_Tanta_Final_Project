using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITI_Tanta_Final_Project.Migrations
{
    /// <inheritdoc />
    public partial class addprop_titlesession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Sessions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Sessions");
        }
    }
}
