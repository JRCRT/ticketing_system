using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.FileDto
{
    public class GetFileDto
    {
        public int file_id { get; set; }
        public string file_url { get; set; } = string.Empty;

        public string stored_file_name { get; set; } = string.Empty;
        public string original_file_name { get; set; } = string.Empty;
        public string content_type { get; set; } = string.Empty;
    }
}
