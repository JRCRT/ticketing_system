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
        [MaxLength(500)]
        public string first_name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? middle_name { get; set; }

        [Required()]
        [MaxLength(500)]
        public string last_name { get; set; } = string.Empty;
        
        [Required()]
        [MaxLength(100)]
        public string username {get; set;} = string.Empty;

        [Required()]
        [MaxLength(100)]
        public string password {get; set;} = string.Empty;
    }
}