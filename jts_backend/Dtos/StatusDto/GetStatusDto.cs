using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.StatusDto
{
    public class GetStatusDto
    {
        public int status_id { get; set; }
        public string name { get; set; } = string.Empty;
    }
}
