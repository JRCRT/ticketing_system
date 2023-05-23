using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Models
{
    [Table("status")]
    public class StatusModel
    {
        [Key]
        public int status_id { get; set; }

        [Required]
        [MaxLength(400)]
        public string name { get; set; } = string.Empty;
    }
}
