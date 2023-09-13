using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.UserDto
{
    public class GetUsersDto
    {
        public ICollection<GetUserDto> users { get; set; } = new Collection<GetUserDto>();
        public int total_items { get; set; }
    }
}
