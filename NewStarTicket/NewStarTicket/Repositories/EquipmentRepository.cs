using Microsoft.Data.SqlClient;
using NewStarTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.Repositories
{
    public class EquipmentRepository : RepositoryBase, IEquipmentRepository
    {
        public void AddEquipment(Equipment equipment)
        {
            throw new NotImplementedException();
        }
        public void EditEquipment(Equipment equipment)
        {
            throw new NotImplementedException();
        }
        public void RemoveEquipment(Equipment equipment)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Equipment> GetAllEquipment()
        {
            List<Equipment> equipmentList = new List<Equipment>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * From EquipmentTable";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var equipment = new Equipment
                        {
                            IdEquipment = reader.GetInt32(0),
                            NameEquipment = reader.GetString(1)
                        };
                        equipmentList.Add(equipment);
                    }
                }
            }

            return equipmentList;
        }
    }
}
