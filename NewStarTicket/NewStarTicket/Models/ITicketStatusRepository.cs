using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Models
{
    public interface ITicketStatusRepository
    {
        void AddTicketStatus(TicketStatus ticketStatus);
        void EditTicketStatus(TicketStatus ticketStatus);
        void RemoveTicketStatus(TicketStatus ticketStatus);
        IEnumerable<TicketStatus> GetAllTicketStatus();
    }
}
