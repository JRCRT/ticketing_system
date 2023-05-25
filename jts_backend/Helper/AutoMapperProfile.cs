using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using jts_backend.Dtos.AuthDto;
using jts_backend.Dtos.DepartmentDto;
using jts_backend.Dtos.UserDto;
using jts_backend.Models;

namespace jts_backend.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserModel, GetUserDto>();
            CreateMap<DepartmentModel, GetDepartmentDto>();
            CreateMap<RoleModel, GetUserDto>();
            CreateMap<UserModel, AuthUserDto>();
        }
    }
}
