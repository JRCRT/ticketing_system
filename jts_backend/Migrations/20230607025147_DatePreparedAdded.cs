using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jts_backend.Migrations
{
    /// <inheritdoc />
    public partial class DatePreparedAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_signatory_ticket_ticket_id",
                table: "signatory");

            migrationBuilder.DropForeignKey(
                name: "FK_signatory_user_user_id",
                table: "signatory");

            migrationBuilder.AddColumn<DateTime>(
                name: "datePrepared",
                table: "ticket",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "signatory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ticket_id",
                table: "signatory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_signatory_ticket_ticket_id",
                table: "signatory",
                column: "ticket_id",
                principalTable: "ticket",
                principalColumn: "ticket_id");

            migrationBuilder.AddForeignKey(
                name: "FK_signatory_user_user_id",
                table: "signatory",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_signatory_ticket_ticket_id",
                table: "signatory");

            migrationBuilder.DropForeignKey(
                name: "FK_signatory_user_user_id",
                table: "signatory");

            migrationBuilder.DropColumn(
                name: "datePrepared",
                table: "ticket");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "signatory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ticket_id",
                table: "signatory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_signatory_ticket_ticket_id",
                table: "signatory",
                column: "ticket_id",
                principalTable: "ticket",
                principalColumn: "ticket_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_signatory_user_user_id",
                table: "signatory",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
