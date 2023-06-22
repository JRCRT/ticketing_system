using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Models
{
    [Table("job_title")]
    public class JobTitleModel
    {
        [Key]
        public int job_title_id { get; set; }
        public string name { get; set; } = string.Empty;
    }
}
