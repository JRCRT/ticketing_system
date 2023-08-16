using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.UserDto
{
    public class TestDto
    {
        public UserDto user { get; set; } = new UserDto();
        public IFormFile file { get; set; }
    }
}
