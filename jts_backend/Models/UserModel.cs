using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace jts_backend.Models
{
    public class UserModel
    {
        [Key]
        public int user_id { get; set; }
        [Required]
        public string first_name { get; set; }
        public string middle_name { get; set; }
        [Required]
        public string last_name { get; set; }
    }
}