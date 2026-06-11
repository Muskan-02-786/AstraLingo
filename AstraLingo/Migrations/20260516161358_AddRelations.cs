using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AstraLingo.Migrations
{
    /// <inheritdoc />
    public partial class AddRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Lessons_LanguageId",
                table: "Lessons",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Languages_LanguageId",
                table: "Lessons",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Languages_LanguageId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_LanguageId",
                table: "Lessons");
        }
    }
}
