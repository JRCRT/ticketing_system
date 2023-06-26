using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.FileDto;
using jts_backend.Dtos.SignatoryDto;
using jts_backend.Models;

namespace jts_backend.Dtos.TicketDto
{
    public class CreateTicketDto
    {
        public string subject { get; set; } = string.Empty;

        public string background { get; set; } = string.Empty;
        public string condition { get; set; } = string.Empty;
        public string content { get; set; } = string.Empty;

        public string reason { get; set; } = string.Empty;

        public string declined_reason { get; set; } = string.Empty;
        public int status_id { get; set; }
        public int user_id { get; set; }

        public int priority_id { get; set; }
        public DateTime date_created { get; set; }
        public DateTime date_approved { get; set; }
        public DateTime date_declined { get; set; }

        public ICollection<CreateSignatoryDto> signatories { get; set; } =
            new Collection<CreateSignatoryDto>();

        public ICollection<IFormFile> files { get; set; } = new Collection<IFormFile>();
    }
}
