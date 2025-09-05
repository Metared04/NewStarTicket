using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Models
{
    public interface ITicketRepository
    {
        void Add(Ticket ticket);
        void Edit(Ticket ticket);
        void EditStatus(Ticket ticket, int newStatusId);
        void TakingTicket(Ticket ticket, Guid IdUser);
        void Remove(Ticket ticket);
        Ticket GetTicketById(Guid IdTicket);
        Ticket GetTicketByTitleTicket(string TitleTicket);
        Ticket GetTicketByUserIdBroadcastedTicket(Guid IdUser);
        int GetTicketCountResolvedByAdmin(Guid IdUser);
        int GetTicketsWaitingCount();
        int GetTicketsCreatedToday();
        Dictionary<int, int> GetTicketsByStatus();
        IEnumerable<Ticket> GetAll();
        IEnumerable<Ticket> GetAllByUserId(Guid userId);
        IEnumerable<Ticket> GetTicketsByEmergencyLevel(int emergencyLevelId);
        IEnumerable<Ticket> GetTicketsByLocation(int locationId);
    }
}
