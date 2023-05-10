using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace jts_backend.Models
{
    public class JtsContext : DbContext
    {
        public JtsContext(DbContextOptions<JtsContext> options) : base(options) { }
        public DbSet<UserModel> user
        {
            get;
            set;
        }
    }

}
