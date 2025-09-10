using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using NewStarTicket.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

        public void Edit(User user, User newUser)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Update [UserTable] set NameUser = @name, " +
                    "[passwordUser] = @passwd, Email = @mail, UserIdLevel = @level " +
                    "where IdUser = @id";
                command.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = newUser.NameUser;
                command.Parameters.Add("@passwd", SqlDbType.NVarChar, 200).Value = newUser.PasswordUser;
                command.Parameters.Add("@mail", SqlDbType.NVarChar, 200).Value = newUser.EmailUser;
                command.Parameters.Add("@level", SqlDbType.Int).Value = newUser.UserIdLevel;
                command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = user.IdUser;
                int rowsAffected = command.ExecuteNonQuery();
            }
        }
        public IEnumerable<User> GetByAll()
        {
            List<User> usersList = new List<User>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [UserTable] Order by UserIdLevel desc";
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
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Delete from [UserTable] Where IdUser = @id";
                command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = IdUser;
                int rowsAffected = command.ExecuteNonQuery();
            }
        }
    }
}
