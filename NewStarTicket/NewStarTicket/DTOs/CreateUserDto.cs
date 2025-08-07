using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewStarTicket.Models;

namespace NewStarTicket.DTOs
{
    public class CreateUserDto
    {
        public string NameUser { get; set; }
        public string PasswordUser { get; set; }
        public string EmailUser { get; set; }
        public int UserIdLevel { get; set; }
    }
}
