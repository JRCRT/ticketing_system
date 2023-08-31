using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Models;

namespace jts_backend.Dtos.AuthDto
{
    public class AuthUserDto
    {
        public int user_id { get; set; }
        public string username { get; set; } = string.Empty;
        public string access_token { get; set; } = string.Empty;
        public RoleModel role { get; set; } = new RoleModel();
        public DepartmentModel department { get; set; } = new DepartmentModel();
    }
}
