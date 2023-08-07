using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.TicketDto
{
    public class ApproveTicketDto
    {
        public int signatory_id { get; set; }

        public string connection_id { get; set; } = string.Empty;
    }
}
