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

        [MaxLength(200)]
        public string? condition { get; set; }

        [MaxLength(500)]
        public string? others { get; set; } = string.Empty;

        [Required]
        public string background { get; set; } = string.Empty;

        [Required]
        public string content { get; set; } = string.Empty;

        [Required]
        public string reason { get; set; } = string.Empty;

        public DateTime date_created { get; set; }
        public DateTime action_date { get; set; }

        [MaxLength(500)]
        public string? rejection_reason { get; set; }
        public StatusModel status { get; set; } = new StatusModel();

        [Column("created_by")]
        public UserModel? created_by { get; set; } = new UserModel();

        [Column("received_by")]
        public UserModel? received_by { get; set; } = new UserModel();

        [Column("declined_by")]
        public UserModel? rejected_by { get; set; } = new UserModel();
        public PriorityModel priority { get; set; } = new PriorityModel();
    }
}
