using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jts_backend.Migrations
{
    /// <inheritdoc />
    public partial class ShortNameColumnAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "short_name",
                table: "user",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "short_name",
                table: "user");
        }
    }
}
