using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Models;
using jts_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace jts_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userSevice)
        {
            _userService = userSevice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUsers(){
            var users = await _userService.GetAllUser();
            return Ok(users);
        }
    }
}