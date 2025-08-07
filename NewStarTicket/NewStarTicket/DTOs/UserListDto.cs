using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.DTOs
{
    public class UserListDto
    {
        public int IdUser { get; set; }
        public string NameUser { get; set; }
        public string EmailUser { get; set; }
        public string UserLevelName { get; set; }
    }
}
