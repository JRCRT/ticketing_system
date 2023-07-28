using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jts_backend.Migrations
{
    /// <inheritdoc />
    public partial class ModifySignatoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_signatory_status_status_id",
                table: "signatory");

            migrationBuilder.AlterColumn<int>(
                name: "status_id",
                table: "signatory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_signatory_status_status_id",
                table: "signatory",
                column: "status_id",
                principalTable: "status",
                principalColumn: "status_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_signatory_status_status_id",
                table: "signatory");

            migrationBuilder.AlterColumn<int>(
                name: "status_id",
                table: "signatory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_signatory_status_status_id",
                table: "signatory",
                column: "status_id",
                principalTable: "status",
                principalColumn: "status_id");
        }
    }
}
