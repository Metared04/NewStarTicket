using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.DTOs.Request
{
    public class UpdateUserDto
    {
        public string NameUser { get; set; }
        public string EmailUser { get; set; }
        public int UserIdLevel { get; set; }
    }
}
