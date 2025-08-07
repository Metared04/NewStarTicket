using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Models
{
    public class Ticket
    {
        public int IdTicket { get; set; }
        public string TitleTicket { get; set; }
        public string DescriptionTicket { get; set; }
        public DateTime BroadcastDateTicket { get; set; }
        public DateTime? ResolvedDateTicket { get; set; }
        public int StatusIdTicket { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public int EmergencyLevelIdTicket { get; set; }
        public EmergencyLevel EmergencyLevel { get; set; }
        public int EquipmentIdTicket { get; set; }
        public Equipment Equipment { get; set; }
        public int LocationIdTicket { get; set; }
        public Location Location { get; set; }
        public int UserBroadcastedIdTicket { get; set; }
        public int UserResolvedIdTicket { get; set; }
    }
}
