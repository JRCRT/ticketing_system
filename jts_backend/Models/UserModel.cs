using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jts_backend.Models
{
    [Table("user")]
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
        [MaxLength(500)]
        public string ext_name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? short_name { get; set; }

        [Required()]
        [MaxLength(200)]
        public string email { get; set; } = string.Empty;

        [Required()]
        [MaxLength(100)]
        public string username { get; set; } = string.Empty;

        [Required()]
        [MaxLength(200)]
        public byte[] password_hash { get; set; } = new byte[0];

        [Required()]
        [MaxLength(200)]
        public byte[] password_salt { get; set; } = new byte[0];
        public RoleModel role { get; set; } = new RoleModel();
        public JobTitleModel job_title { get; set; } = new JobTitleModel();
        public DepartmentModel department { get; set; } = new DepartmentModel();
    }
}
