using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.RoleDto
{
    public class GetRoleDto
    {
        public int role_id { get; set; }
        public string name { get; set; } = string.Empty;
    }
}
