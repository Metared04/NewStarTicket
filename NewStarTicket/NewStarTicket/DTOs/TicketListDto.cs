using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.DTOs
{
    public class TicketListDto
    {
        public int IdTicket { get; set; }
        public string TitleTicket { get; set; }
        public DateTime BroadcastDateTicket { get; set; }
        public string StatusName { get; set; }
        public string EmergencyLevelName { get; set; }
        public string LocationName { get; set; }
    }
}
