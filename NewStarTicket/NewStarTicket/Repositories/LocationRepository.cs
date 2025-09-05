using Microsoft.Data.SqlClient;
using NewStarTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Repositories
{
    public class LocationRepository : RepositoryBase, ILocationRepository
    {
        public void AddLocation(Location location)
        {
            throw new NotImplementedException();
        }
        public void EditLocation(Location location)
        {
            throw new NotImplementedException();
        }
        public void RemoveLocation(Location location)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Location> GetAllLocations()
        {
            List<Location> locationList = new List<Location>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * From LocationTable";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var location = new Location
                        {
                            IdLocation = reader.GetInt32(0),
                            NameLocation = reader.GetString(1)
                        };
                        locationList.Add(location);
                    }
                }
            }

            return locationList;
        }
    }
}
