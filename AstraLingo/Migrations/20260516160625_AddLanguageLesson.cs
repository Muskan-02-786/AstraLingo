using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AstraLingo.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguageLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "XPReward",
                table: "Lessons",
                newName: "LanguageId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Lessons",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Flag",
                table: "Languages",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LanguageId",
                table: "Lessons",
                newName: "XPReward");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Lessons",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Languages",
                newName: "Flag");
        }
    }
}
