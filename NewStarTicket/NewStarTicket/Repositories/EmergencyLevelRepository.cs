using Microsoft.Data.SqlClient;
using NewStarTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Repositories
{
    public class EmergencyLevelRepository : RepositoryBase, IEmergencyLevelRepository
    {
        public void AddEmergencyLevel(EmergencyLevel emergencyLevel)
        {
            throw new NotImplementedException();
        }
        public void EditEmergencyLevel(EmergencyLevel emergencyLevel)
        {
            throw new NotImplementedException();
        }
        public void RemoveEmergencyLevel(EmergencyLevel emergencyLevel)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<EmergencyLevel> GetAllEmergencyLevel()
        {
            List<EmergencyLevel> emergencyLevelList = new List<EmergencyLevel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * From EmergencyLevelTable";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var emergencylevel = new EmergencyLevel
                        {
                            IdEmergencyLevel = reader.GetInt32(0),
                            NameEmergencyLevel = reader.GetString(1)
                        };
                        emergencyLevelList.Add(emergencylevel);
                    }
                }
            }

            return emergencyLevelList;
        }        
    }
}
