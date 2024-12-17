using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverFlow.Migrations
{
    /// <inheritdoc />
    public partial class Update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Questions",
                newName: "QusContent");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Answers",
                newName: "AnsContent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QusContent",
                table: "Questions",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "AnsContent",
                table: "Answers",
                newName: "Content");
        }
    }
}
