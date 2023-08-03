using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.StatusDto;
using jts_backend.Dtos.UserDto;

namespace jts_backend.Dtos.SignatoryDto
{
    public class GetSignatoryDto
    {
        public int signatory_id { get; set; }
        public GetUserDto user { get; set; } = new GetUserDto();
        public string type { get; set; } = string.Empty;
        public GetStatusDto status { get; set; } = new GetStatusDto();

        private DateTime _action_date;
        public DateTime action_date
        {
            get { return _action_date.Date.ToUniversalTime(); }
            set { _action_date = value; }
        }
    }
}
