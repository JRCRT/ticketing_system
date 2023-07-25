using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.EmailDto
{
    public class CreateEmailDto
    {
        public string from {get;set;} = string.Empty;
        public string to {get;set;} = string.Empty;
        public string subject {get;set;} = string.Empty;
        public string body{get;set;} = string.Empty;

    }
}