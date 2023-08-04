using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.ViewDto
{
    public class GetViewDto
    {
        public int view_id { get; set; }
        public string name { get; set; } = string.Empty;
    }
}