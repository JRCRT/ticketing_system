using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.JobTitleDto
{
    public class GetJobTitleDto
    {
        public int job_title_id { get; set; }
        public string name { get; set; } = string.Empty;
    }
}
