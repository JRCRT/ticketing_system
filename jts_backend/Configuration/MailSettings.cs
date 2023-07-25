using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Configuration
{
    public class MailSettings
    {
        public string? Host {get;set;}
        public int Port {get;set;}
        public string? Username {get;set;}
        public string? Password {get;set;}
        public bool SSL{get;set;}
    }
}