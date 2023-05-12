using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos;
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
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers(){
            var users = await _userService.GetAllUser();
            return Ok(users);
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult<UserModel>> CreateUser(UserModel newUser){
            var user = await _userService.AddUser(newUser);
            return Ok(user);
        }
    }
}