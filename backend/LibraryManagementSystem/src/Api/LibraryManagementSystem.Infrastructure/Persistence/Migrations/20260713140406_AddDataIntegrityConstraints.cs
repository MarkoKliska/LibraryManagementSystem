using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDataIntegrityConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rentals_BookCopyId_ReturnDate",
                schema: "Library",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Books_Isbn13",
                schema: "Library",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "Library",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_BookCopyId",
                schema: "Library",
                table: "Rentals",
                column: "BookCopyId",
                unique: true,
                filter: "\"ReturnDate\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Isbn13",
                schema: "Library",
                table: "Books",
                column: "Isbn13",
                unique: true,
                filter: "\"IsDeleted\" = false");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                schema: "Library",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_BookCopyId",
                schema: "Library",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Books_Isbn13",
                schema: "Library",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_BookCopyId_ReturnDate",
                schema: "Library",
                table: "Rentals",
                columns: new[] { "BookCopyId", "ReturnDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_Isbn13",
                schema: "Library",
                table: "Books",
                column: "Isbn13",
                unique: true);
        }
    }
}
