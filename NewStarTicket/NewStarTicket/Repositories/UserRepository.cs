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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public User GetUserById(int IdUser)
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
                            IdUser = reader[0].ToString(),
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

        public void Remove(int IdUser)
        {
            throw new NotImplementedException();
        }
    }
}
