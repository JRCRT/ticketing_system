using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.SignatoryDto
{
    public class CreateSignatoryDto
    {
        public int user_id { get; set; }
        public string type { get; set; } = string.Empty;
    }
}
