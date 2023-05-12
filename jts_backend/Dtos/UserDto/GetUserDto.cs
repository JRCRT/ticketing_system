using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.UserDto
{
    public class GetUserDto
    {
        public int user_id {get;set;}
        public string first_name { get; set; } = string.Empty;
        public string? middle_name { get; set; }
        public string last_name { get; set; } = string.Empty;
        public string username {get; set;} = string.Empty;
    }
}