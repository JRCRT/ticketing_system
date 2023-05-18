using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.UserDto;
using jts_backend.Models;
using jts_backend.Services.UserService;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetUserDto>>>> GetAllUsers()
        {
            var users = await _userService.GetAllUser();
            return Ok(users);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<ServiceResponse<UserModel>>> CreateUser(
            CreateUserDto newUser
        )
        {
            var user = await _userService.CreateUser(newUser);
            return Ok(user);
        }

        [HttpPost("GetUser")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUser(int user_id)
        {
            var user = await _userService.GetUser(user_id);
            if (user.data == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
