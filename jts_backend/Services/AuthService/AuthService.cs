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
using Microsoft.OpenApi.Validations;

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

        public async Task<ServiceResponse<AuthUserDto>> Login(LoginDto request)
        {
            var response = new ServiceResponse<AuthUserDto>();
            var user = await _context.user
                .Include(u => u.role)
                .Include(u => u.department)
                .FirstOrDefaultAsync(u => u.username.ToLower() == request.username.ToLower());
            if (
                user is null
                || !Helper.Helper.VerifyPasswordHash(
                    request.password,
                    user.password_hash,
                    user.password_salt
                )
            )
            {
                response.message = "Incorrect username/password.";
                response.success = false;
                return response;
            }

            var authUser = new AuthUserDto
            {
                user_id = user.user_id,
                username = user.username,
                access_token = CreateToken(user),
                role = user.role,
                department = user.department
            };
            response.data = authUser;

            return response;
        }

        private string CreateToken(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.user_id.ToString()),
                new Claim(ClaimTypes.Name, user.username),
            };

            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
            if (appSettingsToken is null)
                throw new Exception("AppSettings Token is null!");

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(appSettingsToken)
            );

            SigningCredentials creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha512Signature
            );

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
