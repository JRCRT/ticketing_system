using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Models;

namespace jts_backend.Dtos.TicketDto
{
    public class GetTicketDto
    {
        public TicketDto ticket { get; set; } = new TicketDto();
        public ICollection<UserModel> signatories { get; set; } = new Collection<UserModel>();
    }
}
