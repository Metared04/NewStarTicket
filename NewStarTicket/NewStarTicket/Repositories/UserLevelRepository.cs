using Microsoft.Data.SqlClient;
using NewStarTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Repositories
{
    public class UserLevelRepository : RepositoryBase, IUserLevelRepository
    {
        public void AddUserLevel(UserLevel userLevel)
        {
            throw new NotImplementedException();
        }
        public void EditUserLevel(UserLevel userLevel)
        {
            throw new NotImplementedException();
        }
        public void RemoveUserLevel(UserLevel userLevel)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserLevel> GetAllUserLevel()
        {
            List<UserLevel> userLevelList = new List<UserLevel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * From UserLevelTable";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var userLevel = new UserLevel
                        {
                            IdUserLevel = reader.GetInt32(0),
                            NameUserLevel = reader.GetString(1)
                        };
                        userLevelList.Add(userLevel);
                    }
                }
            }

            return userLevelList;
        }
    }
}
