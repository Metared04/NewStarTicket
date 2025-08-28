using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Models
{
    public class User
    {
        private string idUser;
        private string nameUser;
        private string passwordUser;
        private string emailUser;
        private int userIdLevel;
        public string IdUser { get; set; }
        public string NameUser { get; set; }
        public string PasswordUser { get; set; }
        public string EmailUser { get; set; }
        public int UserIdLevel { get; set; }
        public UserLevel UserLevel { get; set; }
    }
}
