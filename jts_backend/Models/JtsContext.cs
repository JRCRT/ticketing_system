using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Models
{
    public class JtsContext : DbContext
    {
        public JtsContext(DbContextOptions options) : base(options) { }
        DbSet<UserModel> user
        {
            get;
            set;
        }
    }

}
