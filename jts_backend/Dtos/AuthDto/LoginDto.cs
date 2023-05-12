using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.AuthDto
{
    public class LoginDto
    {
        public string username{get;set;} = string.Empty;
        public string password{get;set;} = string.Empty;
    }
}