using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jts_backend.Models
{
    [Table("role")]
    public class RoleModel
    {
        [Key]
        public int role_id { get; set; }
        public string name { get; set; } = string.Empty;
    }
}
