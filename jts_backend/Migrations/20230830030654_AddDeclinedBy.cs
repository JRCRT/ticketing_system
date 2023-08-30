using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jts_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddDeclinedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "declined_byuser_id",
                table: "ticket",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ticket_declined_byuser_id",
                table: "ticket",
                column: "declined_byuser_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ticket_user_declined_byuser_id",
                table: "ticket",
                column: "declined_byuser_id",
                principalTable: "user",
                principalColumn: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ticket_user_declined_byuser_id",
                table: "ticket");

            migrationBuilder.DropIndex(
                name: "IX_ticket_declined_byuser_id",
                table: "ticket");

            migrationBuilder.DropColumn(
                name: "declined_byuser_id",
                table: "ticket");
        }
    }
}
