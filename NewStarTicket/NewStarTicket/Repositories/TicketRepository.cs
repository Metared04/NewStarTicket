using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
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
        public void EditStatus(Ticket ticket, int newStatusId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [TicketTable] Set StatusIdTicket = @status, ResolvedDateTicket = @date Where IdTicket = @id";
                command.Parameters.Add("@status", SqlDbType.Int).Value = newStatusId;
                command.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTime.Now;
                command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = ticket.IdTicket;
                int rowsAffected = command.ExecuteNonQuery();
            }
        }
        public void TakingTicket(Ticket ticket, Guid IdUser)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [TicketTable] Set UserResolvedIdTicket = @uid, StatusIdTicket = 2 Where IdTicket = @id";
                command.Parameters.Add("@uid", SqlDbType.UniqueIdentifier).Value = IdUser;
                command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = ticket.IdTicket;
                int rowsAffected = command.ExecuteNonQuery();
            }
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
            LEFT JOIN UserTable ur ON t.UserResolvedIdTicket = ur.IdUser
            order by t.StatusIdTicket";
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
                            UserResovelvedTicketName = reader["UserResolvedName"]?.ToString(),
                        };
                        ticketsList.Add(ticket);
                    }
                }
            }

            return ticketsList;
        }

        public IEnumerable<Ticket> GetAllByUserId(Guid userId)
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
            LEFT JOIN UserTable ur ON t.UserResolvedIdTicket = ur.IdUser
            where t.UserBroadcastedIdTicket = @userid
            order by t.StatusIdTicket";
                command.Parameters.Add("@userid", SqlDbType.UniqueIdentifier).Value = userId;
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
                            UserResovelvedTicketName = reader["UserResolvedName"]?.ToString(),
                        };
                        ticketsList.Add(ticket);
                    }
                }
            }
            return ticketsList;
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
        public void Remove(Ticket ticket)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Delete from [TicketTable] Where IdTicket = @id";
                command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = ticket.IdTicket;
                int rowsAffected = command.ExecuteNonQuery();
            }
        }

        public int GetTicketCountResolvedByAdmin(Guid IdUser)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open(); 
                command.Connection = connection;
                command.CommandText = "select count(*) from TicketTable " +
                    "where UserResolvedIdTicket = @adminId and " +
                    "statusIdTicket = 4";
                command.Parameters.Add("@adminId", SqlDbType.UniqueIdentifier).Value = IdUser;
                var result = command.ExecuteScalar();
                return (result != null) ? Convert.ToInt32(result) : 0;
            }
        }

        public int GetTicketsWaitingCount()
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select count(*) " +
                    "from TicketTable " +
                    "where statusIdTicket = 1";
                var result = command.ExecuteScalar();
                return (result != null) ? Convert.ToInt32(result) : 0;
            }
        }

        public int GetTicketsCreatedToday()
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open(); 
                command.Connection = connection;
                command.CommandText = "select count(*) " +
                    "from TicketTable " +
                    "WHERE BroadcastDateTicket >= @startOfDay " +
                    "AND BroadcastDateTicket < @startOfNextDay";

                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);

                command.Parameters.Add("@startOfDay", SqlDbType.DateTime).Value = today;
                command.Parameters.Add("@startOfNextDay", SqlDbType.DateTime).Value = tomorrow;

                var result = command.ExecuteScalar();
                return (result != null) ? Convert.ToInt32(result) : 0;
            }
        }
        public Dictionary<int, int> GetTicketsByStatus()
        {
            var result = new Dictionary<int, int>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT StatusIdTicket, COUNT(*) AS Count " +
                    "FROM TicketTable " +
                    "GROUP BY StatusIdTicket";
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int statusId = reader.GetInt32(0);
                        int count = reader.GetInt32(1);
                        result[statusId] = count;
                    }
                }
            }
            return result;
        }
    }
}
