using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeWork04._01.Data.Migrations
{
    /// <inheritdoc />
    public partial class anothertime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Questions");

            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
