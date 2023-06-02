using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Models
{
    [Table("ticket")]
    public class TicketModel
    {
        [Key]
        public int ticket_id { get; set; }

        [Required]
        [MaxLength(500)]
        public string subject { get; set; } = string.Empty;

        [Required]
        public string background { get; set; } = string.Empty;

        [Required]
        public string content { get; set; } = string.Empty;

        [Required]
        public string reason { get; set; } = string.Empty;

        [MaxLength(500)]
        public string declined_reason { get; set; } = string.Empty;
        public StatusModel status { get; set; } = new StatusModel();
        public UserModel user { get; set; } = new UserModel();

        public PriorityModel priority { get; set; } = new PriorityModel();
    }
}
