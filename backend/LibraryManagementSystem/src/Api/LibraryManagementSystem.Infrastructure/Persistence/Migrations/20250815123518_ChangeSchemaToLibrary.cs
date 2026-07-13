using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSchemaToLibrary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Library");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "LibraryDb",
                newName: "Users",
                newSchema: "Library");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "LibraryDb");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Library",
                newName: "Users",
                newSchema: "LibraryDb");
        }
    }
}
