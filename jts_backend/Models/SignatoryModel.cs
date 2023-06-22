using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Models
{
    [Table("signatory")]
    public class SignatoryModel
    {
        [Key]
        public int signatory_id { get; set; }
        public UserModel? user { get; set; } = new UserModel();
        public TicketModel? ticket { get; set; } = new TicketModel();
        public string type { get; set; } = string.Empty;
    }
}
