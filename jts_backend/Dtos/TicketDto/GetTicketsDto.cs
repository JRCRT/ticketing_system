using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.TicketDto
{
    public class GetTicketsDto
    {
        public ICollection<GetTicketDto> tickets { get; set; } = new Collection<GetTicketDto>();
        public int total_items { get; set; }
    }
}
