using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.DTOs
{
    public class UpdateTicketDto
    {
        public string TitleTicket { get; set; }
        public string DescriptionTicket { get; set; }
        public int EmergencyLevelIdTicket { get; set; }
        public int EquipmentIdTicket { get; set; }
        public int LocationIdTicket { get; set; }
        public int StatusIdTicket { get; set; }
    }
}
