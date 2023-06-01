using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.UserDto;
using jts_backend.Models;
using Microsoft.AspNetCore.SignalR;

namespace jts_backend.Hub.User
{
    public class UserHub : Hub<IUserHub> { }
}
