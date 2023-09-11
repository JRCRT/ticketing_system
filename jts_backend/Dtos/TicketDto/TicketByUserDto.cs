using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.TicketDto
{
    public class TicketByUserDto
    {
        public int user_id { get; set; }
        public int status_id { get; set; }
        public int items_per_page { get; set; }
        public int offset { get; set; }

        public int ticket_id { get; set; }
        public DateTime date_created { get; set; }
        public int? prepared_by { get; set; }
    }
}
