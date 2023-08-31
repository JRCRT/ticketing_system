using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.UserDto;
using jts_backend.Models;

namespace jts_backend.Dtos.TicketDto
{
    public class TicketDto
    {
        public int ticket_id { get; set; }

        public string subject { get; set; } = string.Empty;

        public string condition { get; set; } = string.Empty;

        public string background { get; set; } = string.Empty;

        public string content { get; set; } = string.Empty;

        public string reason { get; set; } = string.Empty;

        public string others { get; set; } = string.Empty;

        public string rejection_reason { get; set; } = string.Empty;

        private DateTime _date_created;
        public DateTime date_created
        {
            get { return _date_created.Date.ToUniversalTime(); }
            set { _date_created = value; }
        }

        private DateTime _action_date;

        public DateTime action_date
        {
            get { return _action_date.Date.ToUniversalTime(); }
            set { _action_date = value; }
        }

        public StatusModel status { get; set; } = new StatusModel();
        public GetUserDto created_by { get; set; } = new GetUserDto();
        public GetUserDto received_by { get; set; } = new GetUserDto();
        public GetUserDto rejected_by { get; set; } = new GetUserDto();
        public PriorityModel priority { get; set; } = new PriorityModel();
    }
}
