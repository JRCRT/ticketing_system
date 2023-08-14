﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using jts_backend.Context;

#nullable disable

namespace jts_backend.Migrations
{
    [DbContext(typeof(JtsContext))]
    partial class JtsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("jts_backend.Models.DepartmentModel", b =>
                {
                    b.Property<int>("department_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("department_id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("department_id");

                    b.ToTable("department");
                });

            modelBuilder.Entity("jts_backend.Models.FileModel", b =>
                {
                    b.Property<int>("file_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("file_id"));

                    b.Property<string>("content_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("file_url")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("original_file_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("owner_id")
                        .HasColumnType("int");

                    b.Property<string>("owner_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("stored_file_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("file_id");

                    b.ToTable("file");
                });

            modelBuilder.Entity("jts_backend.Models.JobTitleModel", b =>
                {
                    b.Property<int>("job_title_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("job_title_id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("job_title_id");

                    b.ToTable("job_title");
                });

            modelBuilder.Entity("jts_backend.Models.PriorityModel", b =>
                {
                    b.Property<int>("priority_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("priority_id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("priority_id");

                    b.ToTable("priority");
                });

            modelBuilder.Entity("jts_backend.Models.RoleManagerModel", b =>
                {
                    b.Property<int>("role_manager_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("role_manager_id"));

                    b.HasKey("role_manager_id");

                    b.ToTable("role_manager");
                });

            modelBuilder.Entity("jts_backend.Models.RoleModel", b =>
                {
                    b.Property<int>("role_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("role_id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("role_id");

                    b.ToTable("role");
                });

            modelBuilder.Entity("jts_backend.Models.SignatoryModel", b =>
                {
                    b.Property<int>("signatory_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("signatory_id"));

                    b.Property<DateTime>("action_date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("can_approve")
                        .HasColumnType("bit");

                    b.Property<int>("status_id")
                        .HasColumnType("int");

                    b.Property<int?>("ticket_id")
                        .HasColumnType("int");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("user_id")
                        .HasColumnType("int");

                    b.HasKey("signatory_id");

                    b.HasIndex("status_id");

                    b.HasIndex("ticket_id");

                    b.HasIndex("user_id");

                    b.ToTable("signatory");
                });

            modelBuilder.Entity("jts_backend.Models.StatusModel", b =>
                {
                    b.Property<int>("status_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("status_id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("status_id");

                    b.ToTable("status");
                });

            modelBuilder.Entity("jts_backend.Models.TicketModel", b =>
                {
                    b.Property<int>("ticket_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ticket_id"));

                    b.Property<string>("background")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("condition")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date_approved")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("date_created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("date_declined")
                        .HasColumnType("datetime2");

                    b.Property<string>("declined_reason")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("others")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("priority_id")
                        .HasColumnType("int");

                    b.Property<string>("reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("status_id")
                        .HasColumnType("int");

                    b.Property<string>("subject")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("ticket_id");

                    b.HasIndex("priority_id");

                    b.HasIndex("status_id");

                    b.HasIndex("user_id");

                    b.ToTable("ticket");
                });

            modelBuilder.Entity("jts_backend.Models.UserModel", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("user_id"));

                    b.Property<int>("department_id")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ext_name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("job_title_id")
                        .HasColumnType("int");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("middle_name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte[]>("password_hash")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varbinary(200)");

                    b.Property<byte[]>("password_salt")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varbinary(200)");

                    b.Property<int>("role_id")
                        .HasColumnType("int");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("user_id");

                    b.HasIndex("department_id");

                    b.HasIndex("job_title_id");

                    b.HasIndex("role_id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("jts_backend.Models.ViewModel", b =>
                {
                    b.Property<int>("view_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("view_id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("view_id");

                    b.ToTable("view");
                });

            modelBuilder.Entity("jts_backend.Models.SignatoryModel", b =>
                {
                    b.HasOne("jts_backend.Models.StatusModel", "status")
                        .WithMany()
                        .HasForeignKey("status_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("jts_backend.Models.TicketModel", "ticket")
                        .WithMany()
                        .HasForeignKey("ticket_id");

                    b.HasOne("jts_backend.Models.UserModel", "user")
                        .WithMany()
                        .HasForeignKey("user_id");

                    b.Navigation("status");

                    b.Navigation("ticket");

                    b.Navigation("user");
                });

            modelBuilder.Entity("jts_backend.Models.TicketModel", b =>
                {
                    b.HasOne("jts_backend.Models.PriorityModel", "priority")
                        .WithMany()
                        .HasForeignKey("priority_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("jts_backend.Models.StatusModel", "status")
                        .WithMany()
                        .HasForeignKey("status_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("jts_backend.Models.UserModel", "user")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("priority");

                    b.Navigation("status");

                    b.Navigation("user");
                });

            modelBuilder.Entity("jts_backend.Models.UserModel", b =>
                {
                    b.HasOne("jts_backend.Models.DepartmentModel", "department")
                        .WithMany()
                        .HasForeignKey("department_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("jts_backend.Models.JobTitleModel", "job_title")
                        .WithMany()
                        .HasForeignKey("job_title_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("jts_backend.Models.RoleModel", "role")
                        .WithMany()
                        .HasForeignKey("role_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("department");

                    b.Navigation("job_title");

                    b.Navigation("role");
                });
#pragma warning restore 612, 618
        }
    }
}
