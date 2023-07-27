using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.FileDto;
using jts_backend.Dtos.SignatoryDto;

namespace jts_backend.Dtos.TicketDto
{
    public class GetTicketForApprovalDto
    {
        public TicketDto ticket { get; set; } = new TicketDto();
        public ICollection<GetFileDto> files { get; set; } = new Collection<GetFileDto>();
        public GetSignatoryDto signatory { get; set; } = new GetSignatoryDto();
    }
}