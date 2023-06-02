using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.PriorityDto
{
    public class GetPriorityDto
    {
        public int priority_id { get; set; }
        public string name { get; set; } = string.Empty;
    }
}
