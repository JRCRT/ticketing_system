using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Models
{
    [Table("file")]
    public class FileModel
    {
        [Key]
        public int file_id { get; set; }

        [Required]
        [MaxLength(500)]
        public string file_url { get; set; } = string.Empty;
        public string stored_file_name { get; set; } = string.Empty;
        public string original_file_name { get; set; } = string.Empty;
        public string content_type { get; set; } = string.Empty;
        public TicketModel ticket { get; set; } = new TicketModel();
    }
}
