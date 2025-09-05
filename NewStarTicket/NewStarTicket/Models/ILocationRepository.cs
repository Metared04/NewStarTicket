using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Models
{
    public interface ILocationRepository
    {
        void AddLocation(Location location);
        void EditLocation(Location location);
        void RemoveLocation(Location location);
        IEnumerable<Location> GetAllLocations();
    }
}
