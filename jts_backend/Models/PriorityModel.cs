using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Models
{
    [Table("priority")]
    public class PriorityModel
    {
        [Key]
        public int priority_id { get; set; }

        [Required]
        [MaxLength(400)]
        public string name { get; set; } = string.Empty;
    }
}
