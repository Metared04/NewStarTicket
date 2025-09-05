using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Models
{
    public interface IEquipmentRepository
    {
        void AddEquipment(Equipment equipment);
        void EditEquipment(Equipment equipment);
        void RemoveEquipment(Equipment equipment);
        IEnumerable<Equipment> GetAllEquipment();
    }
}
