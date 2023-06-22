using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.UserDto;

namespace jts_backend.Dtos.SignatoryDto
{
    public class GetSignatoryDto
    {
        public GetUserDto user { get; set; } = new GetUserDto();
        public string type { get; set; } = string.Empty;
    }
}
