using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.FileDto;
using jts_backend.Models;

namespace jts_backend.Dtos.UserDto
{
    public class GetUserDto
    {
        public UserDto user { get; set; } = new UserDto();
        public GetFileDto file { get; set; } = new GetFileDto();
    }
}
