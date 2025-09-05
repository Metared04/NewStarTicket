using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Models
{
    public interface IUserLevelRepository
    {
        void AddUserLevel(UserLevel userLevel);
        void EditUserLevel(UserLevel userLevel);
        void RemoveUserLevel(UserLevel userLevel);
        IEnumerable<UserLevel> GetAllUserLevel();
    }
}
