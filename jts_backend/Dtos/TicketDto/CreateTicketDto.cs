using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Models;

namespace jts_backend.Dtos.TicketDto
{
    public class CreateTicketDto
    {
        public string subject { get; set; } = string.Empty;

        public string background { get; set; } = string.Empty;

        public string content { get; set; } = string.Empty;

        public string reason { get; set; } = string.Empty;

        public string declined_reason { get; set; } = string.Empty;
        public int status_id { get; set; }
        public int user_id { get; set; }

        public int priority_id { get; set; }

        public ICollection<int> signatories { get; set; } = new Collection<int>();
    }
}
