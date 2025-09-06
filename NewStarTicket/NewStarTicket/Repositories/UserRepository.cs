using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using NewStarTicket.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(User user)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = @"Insert into [UserTable] " +
                    "(NameUser, [passwordUser], Email, UserIdLevel) " +
                    "values (@name, @passwd, @mail, @level)";

                command.Parameters.AddWithValue("@name", user.NameUser);
                command.Parameters.AddWithValue("@passwd", user.PasswordUser);
                command.Parameters.AddWithValue("@mail", user.EmailUser);
                command.Parameters.AddWithValue("@level", user.UserIdLevel);

                command.ExecuteNonQuery();
            }
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command=new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [UserTable] where NameUser=@username and [passwordUser]=@password";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value=credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value=credential.Password;

                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;
        }

        public void Edit(User user)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<User> GetByAll()
        {
            List<User> usersList = new List<User>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [UserTable] Group by UserIdLevel";
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            IdUser = (Guid)reader["IdUser"],
                            NameUser = reader["NameUser"].ToString(),
                            PasswordUser = reader["passwordUser"].ToString(),
                            EmailUser = reader["Email"].ToString(),
                            UserIdLevel = (int)reader["UserIdLevel"]
                        };
                        usersList.Add(user);
                    }
                }
            }

            return usersList;
        }
        public User GetUserById(Guid IdUser)
        {
            throw new NotImplementedException();
        }
        public User GetUserByUsername(string username)
        {
            User user=null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [UserTable] where NameUser=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        user = new User()
                        {
                            IdUser = (Guid)reader[0],
                            NameUser = reader[1].ToString(),
                            PasswordUser = string.Empty,
                            EmailUser = reader[3].ToString(),
                            UserIdLevel = Convert.ToInt32(reader[4]),
                        };
                    }
                }
            }
            return user;
        }
        public void Remove(Guid IdUser)
        {
            throw new NotImplementedException();
        }
    }
}
