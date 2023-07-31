using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.TicketDto
{
    public class TicketForApprovalDto
    {
        public int userId { get; set; }
        public string status { get; set; } = string.Empty;
    }
}
