using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.SignatoryDto
{
    public class UpdateSignatoryDto
    {
        public int signatory_id {get;set;}
        public int user_id { get; set; }
        public string type { get; set; } = string.Empty;
        public int ticket_id {get;set;}
        public int status_id{get;set;} 
    }
}