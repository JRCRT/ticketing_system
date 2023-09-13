using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.UserDto
{
    public class AllUserDto
    {
        public int items_per_page { get; set; }
        public int offset { get; set; }
        public string username { get; set; } = string.Empty;
        public string full_name { get; set; } = string.Empty;
    }
}
