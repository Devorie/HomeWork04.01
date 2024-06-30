using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeWork04._01.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateanothertime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Questions");
        }
    }
}
