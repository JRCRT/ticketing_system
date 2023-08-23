using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.UserDto
{
    public class UpdateUserDto
    {
        public int user_id { get; set; }
        public string first_name { get; set; } = string.Empty;
        public string? middle_name { get; set; }
        public string? short_name { get; set; }
        public string last_name { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public int role_id { get; set; }
        public int department_id { get; set; }
        public int job_title_id { get; set; }
        public IFormFile? file { get; set; }
    }
}
