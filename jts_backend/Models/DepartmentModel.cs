using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jts_backend.Models
{
    [Table("department")]
    public class DepartmentModel
    {
        [Key]
        public int department_id { get; set; }

        [Required]
        [MaxLength(400)]
        public string name { get; set; } = string.Empty;
    }
}
