using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.TicketDto;
using jts_backend.Dtos.UserDto;

namespace jts_backend.Hub
{
    public interface IJtsHub
    {   
        Task GetUser(GetUserDto user);
        Task GetTicket();
        Task GetMyTicket();
        Task GetTicketForApproval();  

    }
}