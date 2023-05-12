using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using jts_backend.Context;
using jts_backend.Dtos.AuthDto;
using jts_backend.Dtos.UserDto;
using jts_backend.Helper;
using jts_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace jts_backend.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly JtsContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AuthService(JtsContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<string>> Login(LoginDto request)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            UserModel? user = await _context.user.Where(u => u.username.ToLower() == request.username.ToLower()).FirstOrDefaultAsync();
            if(user is null){
                response.message = "Incorrect username/password.";
                response.success = false;
            }
            else if(!Helper.Helper.VerifyPasswordHash(request.password, user.password_hash, user.password_salt)){
                response.message = "Incorrect username/password.";
                response.success = false;        
            }else{
                GetUserDto userDto = _mapper.Map<GetUserDto>(user);
                response.data = CreateToken(userDto);
            }
            
            return response;
        }

         private string CreateToken(GetUserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.user_id.ToString()),
                new Claim(ClaimTypes.Name, user.username)
            };

            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
            if (appSettingsToken is null)
                throw new Exception("AppSettings Token is null!");

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(appSettingsToken));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}