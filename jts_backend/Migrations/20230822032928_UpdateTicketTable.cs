using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jts_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTicketTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date_approved",
                table: "ticket");

            migrationBuilder.RenameColumn(
                name: "date_declined",
                table: "ticket",
                newName: "action_date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "action_date",
                table: "ticket",
                newName: "date_declined");

            migrationBuilder.AddColumn<DateTime>(
                name: "date_approved",
                table: "ticket",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
