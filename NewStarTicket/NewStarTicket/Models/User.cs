using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Models
{
    public class User
    {
        private int idUser;
        private string nameUser;
        private string passwordUser;
        private string emailUser;
        private int userIdLevel;
        public int IdUser { get; set; }
        public string NameUser { get; set; }
        public string PasswordUser { get; set; }
        public string EmailUser { get; set; }
        public int UserIdLevel { get; set; }
        public UserLevel UserLevel { get; set; }
    }
}
