using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jts_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    department_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.department_id);
                });

            migrationBuilder.CreateTable(
                name: "job_title",
                columns: table => new
                {
                    job_title_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_title", x => x.job_title_id);
                });

            migrationBuilder.CreateTable(
                name: "priority",
                columns: table => new
                {
                    priority_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_priority", x => x.priority_id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    status_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.status_id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    middle_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ext_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password_hash = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    password_salt = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    job_title_id = table.Column<int>(type: "int", nullable: false),
                    department_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_user_department_department_id",
                        column: x => x.department_id,
                        principalTable: "department",
                        principalColumn: "department_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_job_title_job_title_id",
                        column: x => x.job_title_id,
                        principalTable: "job_title",
                        principalColumn: "job_title_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    ticket_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subject = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    condition = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    others = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    background = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    date_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    date_approved = table.Column<DateTime>(type: "datetime2", nullable: false),
                    date_declined = table.Column<DateTime>(type: "datetime2", nullable: false),
                    declined_reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    status_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    priority_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket", x => x.ticket_id);
                    table.ForeignKey(
                        name: "FK_ticket_priority_priority_id",
                        column: x => x.priority_id,
                        principalTable: "priority",
                        principalColumn: "priority_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticket_status_status_id",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticket_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "file",
                columns: table => new
                {
                    file_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    file_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    stored_file_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    original_file_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ticket_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file", x => x.file_id);
                    table.ForeignKey(
                        name: "FK_file_ticket_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "ticket",
                        principalColumn: "ticket_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "signatory",
                columns: table => new
                {
                    signatory_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    ticket_id = table.Column<int>(type: "int", nullable: true),
                    approval_statusstatus_id = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_signatory", x => x.signatory_id);
                    table.ForeignKey(
                        name: "FK_signatory_status_approval_statusstatus_id",
                        column: x => x.approval_statusstatus_id,
                        principalTable: "status",
                        principalColumn: "status_id");
                    table.ForeignKey(
                        name: "FK_signatory_ticket_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "ticket",
                        principalColumn: "ticket_id");
                    table.ForeignKey(
                        name: "FK_signatory_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_file_ticket_id",
                table: "file",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_signatory_approval_statusstatus_id",
                table: "signatory",
                column: "approval_statusstatus_id");

            migrationBuilder.CreateIndex(
                name: "IX_signatory_ticket_id",
                table: "signatory",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_signatory_user_id",
                table: "signatory",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_priority_id",
                table: "ticket",
                column: "priority_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_status_id",
                table: "ticket",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_user_id",
                table: "ticket",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_department_id",
                table: "user",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_job_title_id",
                table: "user",
                column: "job_title_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_id",
                table: "user",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "file");

            migrationBuilder.DropTable(
                name: "signatory");

            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "priority");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.DropTable(
                name: "job_title");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
