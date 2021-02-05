using HotelManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.BLL.Interfaces
{
    public interface IClientsService
    {
        void Add(Client client);
        IEnumerable<Client> Get();
        void Remove(int id);
        Client Find(int id);
    }
}
