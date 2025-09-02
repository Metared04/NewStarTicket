using Microsoft.Data.SqlClient;
using NewStarTicket.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Repositories
{
    public class TicketRepository : RepositoryBase, ITicketRepository
    {
        public void Add(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public void Edit(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ticket> GetAll()
        {
            List<Ticket> ticketsList = new List<Ticket>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
            SELECT t.IdTicket, t.TitleTicket, t.DescriptionTicket, 
                   t.BroadcastDateTicket, t.ResolvedDateTicket, 
                   t.StatusIdTicket, t.EmergencyLevelIdTicket, 
                   t.EquipmentIdTicket, t.LocationIdTicket, 
                   t.UserBroadcastedIdTicket, t.UserResolvedIdTicket,
                   ub.NameUser AS UserBroadcastedName,
                   ur.NameUser AS UserResolvedName
            FROM TicketTable t
            INNER JOIN UserTable ub ON t.UserBroadcastedIdTicket = ub.IdUser
            LEFT JOIN UserTable ur ON t.UserResolvedIdTicket = ur.IdUser";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ticket = new Ticket
                        {
                            IdTicket = (Guid)reader["IdTicket"],
                            TitleTicket = reader["TitleTicket"].ToString(),
                            DescriptionTicket = reader["DescriptionTicket"]?.ToString(),
                            BroadcastDateTicket = (DateTime)reader["BroadcastDateTicket"],
                            ResolvedDateTicket = reader["ResolvedDateTicket"] as DateTime?,
                            StatusIdTicket = (int)reader["StatusIdTicket"],
                            EmergencyLevelIdTicket = (int)reader["EmergencyLevelIdTicket"],
                            EquipmentIdTicket = (int)reader["EquipmentIdTicket"],
                            LocationIdTicket = (int)reader["LocationIdTicket"],
                            UserBroadcastedIdTicket = (Guid)reader["UserBroadcastedIdTicket"],
                            UserResolvedIdTicket = reader["UserResolvedIdTicket"] as Guid?,
                            UserBroadcastedTicketName = reader["UserBroadcastedName"].ToString(),
                            UserResovelvedTicketName = reader["UserResolvedName"] as string,
                        };
                        ticketsList.Add(ticket);
                    }
                }
            }

            return ticketsList;
        }

        public IEnumerable<Ticket> GetAllByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Ticket GetTicketById(int IdTicket)
        {
            /*
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from [TicketTable] WHERE IdTicket = @IdTicket";
                command.Parameters.Add("@IdTicket", SqlDbType.UniqueIdentifier).Value = IdTicket;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Ticket
                        {
                            IdTicket = (Guid)reader["IdTicket"],
                            TitleTicket = reader["TitleTicket"].ToString(),
                            DescriptionTicket = reader["DescriptionTicket"]?.ToString(),
                            BroadcastDateTicket = (DateTime)reader["BroadcastDateTicket"],
                            ResolvedDateTicket = reader["ResolvedDateTicket"] as DateTime?,
                            StatusIdTicket = (int)reader["StatusIdTicket"],
                            EmergencyLevelIdTicket = (int)reader["EmergencyLevelIdTicket"],
                            EquipmentIdTicket = (int)reader["EquipmentIdTicket"],
                            LocationIdTicket = (int)reader["LocationIdTicket"],
                            UserBroadcastedIdTicket = (Guid)reader["UserBroadcastedIdTicket"],
                            UserResolvedIdTicket = reader["UserResolvedIdTicket"] as Guid?
                        };
                    }
                }
            }

            return null;*/
            throw new NotImplementedException();
        }

        public Ticket GetTicketById(Guid IdTicket)
        {
            throw new NotImplementedException();
        }

        public Ticket GetTicketByTitleTicket(string TitleTicket)
        {
            throw new NotImplementedException();
        }

        public Ticket GetTicketByUserIdBroadcastedTicket(Guid IdUser)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ticket> GetTicketsByEmergencyLevel(int emergencyLevelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ticket> GetTicketsByLocation(int locationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ticket> GetTicketsByStatus(int statusId)
        {
            throw new NotImplementedException();
        }

        public void Remove(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
