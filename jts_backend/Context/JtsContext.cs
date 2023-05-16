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
    }
}
