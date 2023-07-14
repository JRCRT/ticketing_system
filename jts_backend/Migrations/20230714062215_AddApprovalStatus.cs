using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jts_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddApprovalStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "password_salt",
                table: "user",
                type: "varbinary(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "password_hash",
                table: "user",
                type: "varbinary(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddColumn<int>(
                name: "approval_statusstatus_id",
                table: "signatory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_signatory_approval_statusstatus_id",
                table: "signatory",
                column: "approval_statusstatus_id");

            migrationBuilder.AddForeignKey(
                name: "FK_signatory_status_approval_statusstatus_id",
                table: "signatory",
                column: "approval_statusstatus_id",
                principalTable: "status",
                principalColumn: "status_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_signatory_status_approval_statusstatus_id",
                table: "signatory");

            migrationBuilder.DropIndex(
                name: "IX_signatory_approval_statusstatus_id",
                table: "signatory");

            migrationBuilder.DropColumn(
                name: "approval_statusstatus_id",
                table: "signatory");

            migrationBuilder.AlterColumn<byte[]>(
                name: "password_salt",
                table: "user",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<byte[]>(
                name: "password_hash",
                table: "user",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(200)",
                oldMaxLength: 200);
        }
    }
}
