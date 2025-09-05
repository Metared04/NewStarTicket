using Microsoft.Data.SqlClient;
using NewStarTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Repositories
{
    public class TicketStatusRepository : RepositoryBase, ITicketStatusRepository
    {
        public void AddTicketStatus(TicketStatus ticketStatus)
        {
            throw new NotImplementedException();
        }
        public void EditTicketStatus(TicketStatus ticketStatus)
        {
            throw new NotImplementedException();
        }
        public void RemoveTicketStatus(TicketStatus ticketStatus)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<TicketStatus> GetAllTicketStatus()
        {
            List<TicketStatus> ticketStatusList = new List<TicketStatus>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * From TicketStatusTable";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ticketStatus = new TicketStatus
                        {
                            IdTicketStatus = reader.GetInt32(0),
                            NameTicketStatus = reader.GetString(1)
                        };
                        ticketStatusList.Add(ticketStatus);
                    }
                }
            }

            return ticketStatusList;
        }
    }
}
