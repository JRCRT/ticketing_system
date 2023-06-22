using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace jts_backend.Context
{
    public class JtsContext : DbContext
    {
        public JtsContext(DbContextOptions<JtsContext> options)
            : base(options) { }

        public DbSet<UserModel> user => Set<UserModel>();
        public DbSet<RoleModel> role => Set<RoleModel>();
        public DbSet<DepartmentModel> department => Set<DepartmentModel>();
        public DbSet<PriorityModel> priority => Set<PriorityModel>();
        public DbSet<StatusModel> status => Set<StatusModel>();
        public DbSet<TicketModel> ticket => Set<TicketModel>();
        public DbSet<SignatoryModel> approver => Set<SignatoryModel>();
        public DbSet<FileModel> file => Set<FileModel>();
        public DbSet<JobTitleModel> jobTitle => Set<JobTitleModel>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* builder.Entity<SignatoryModel>().HasKey(i => new { i.user, i.ticket }); */
        }
    }
}
