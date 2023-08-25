using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.TicketDto
{
    public class DoneTicketDto
    {
        public string connection_id { get; set; } = string.Empty;
        public int ticket_id { get; set; }
        public int user_id { get; set; }
    }
}
