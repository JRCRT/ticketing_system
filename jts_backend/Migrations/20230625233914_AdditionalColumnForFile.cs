using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jts_backend.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalColumnForFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "content_type",
                table: "file",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "original_file_name",
                table: "file",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "stored_file_name",
                table: "file",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "content_type",
                table: "file");

            migrationBuilder.DropColumn(
                name: "original_file_name",
                table: "file");

            migrationBuilder.DropColumn(
                name: "stored_file_name",
                table: "file");
        }
    }
}
