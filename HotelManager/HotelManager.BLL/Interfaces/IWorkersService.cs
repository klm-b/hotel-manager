using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.BLL.Models;

namespace HotelManager.BLL.Interfaces
{
    public interface IWorkersService
    {
        void Add(Worker worker);
        IEnumerable<Worker> Get();
        void Remove(int id);
        Worker Find(int id);
    }
}
