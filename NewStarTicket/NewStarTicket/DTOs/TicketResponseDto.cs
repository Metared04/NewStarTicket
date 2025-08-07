using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.DTOs
{
    public class TicketResponseDto
    {
        public int IdTicket { get; set; }
        public string TitleTicket { get; set; }
        public string DescriptionTicket { get; set; }
        public DateTime BroadcastDateTicket { get; set; }
        public DateTime? ResolvedDateTicket { get; set; }
        public string StatusName { get; set; }
        public string EmergencyLevelName { get; set; }
        public string EquipmentName { get; set; }
        public string LocationName { get; set; }
        public string UserBroadcastedName { get; set; }
        public string UserResolvedName { get; set; }
    }
}
