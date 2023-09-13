using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.TicketDto
{
    public class TicketsTodayDto
    {
        public int user_id { get; set; }
        public int offset { get; set; }
        public int items_per_page { get; set; }
    }
}
