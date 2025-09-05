using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Models
{
    public interface IEmergencyLevelRepository
    {
        void AddEmergencyLevel(EmergencyLevel emergencyLevel);
        void EditEmergencyLevel(EmergencyLevel emergencyLevel);
        void RemoveEmergencyLevel(EmergencyLevel emergencyLevel);
        IEnumerable<EmergencyLevel> GetAllEmergencyLevel();
    }
}
