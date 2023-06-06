using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.UserDto;
using jts_backend.Models;

namespace jts_backend.Dtos.TicketDto
{
    public class TicketDto
    {
        public int ticket_id { get; set; }

        public string subject { get; set; } = string.Empty;

        public string background { get; set; } = string.Empty;

        public string content { get; set; } = string.Empty;

        public string reason { get; set; } = string.Empty;

        public string declined_reason { get; set; } = string.Empty;
        public StatusModel status { get; set; } = new StatusModel();
        public GetUserDto user { get; set; } = new GetUserDto();

        public PriorityModel priority { get; set; } = new PriorityModel();
    }
}
