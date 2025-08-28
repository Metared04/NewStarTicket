using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Models
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(User user);
        void Edit(User user);
        void Remove(int IdUser);
        User GetUserById(int IdUser);
        User GetUserByUsername(string username);
        IEnumerable<User> GetByAll();

    }
}
