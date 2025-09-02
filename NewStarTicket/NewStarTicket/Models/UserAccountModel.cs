using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Models
{
    public class UserAccountModel
    {
        public Guid Id {  get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool IsAdmin { get; set; }
    }
}
