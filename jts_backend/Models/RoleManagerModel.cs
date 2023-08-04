using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Models
{
    [Table("role_manager")]
    public class RoleManagerModel
    {
        [Key]
        public int role_manager_id { get; set; }
        public RoleModel role = new RoleModel();
        public ViewModel view = new ViewModel();
    }
}