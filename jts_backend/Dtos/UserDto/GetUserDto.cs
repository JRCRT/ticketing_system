using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Models;

namespace jts_backend.Dtos.UserDto
{
    public class GetUserDto
    {
        public int user_id { get; set; }
        public string first_name { get; set; } = string.Empty;
        public string? middle_name { get; set; }
        public string last_name { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;

        public string ext_name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public RoleModel role { get; set; } = new RoleModel();
        public DepartmentModel department { get; set; } = new DepartmentModel();
        public JobTitleModel job_title { get; set; } = new JobTitleModel();
    }
}
