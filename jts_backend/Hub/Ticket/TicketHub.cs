using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace jts_backend.Ticket
{
    public class TicketHub : Hub<ITicketHub> { }
}
