using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.BLL.Models;

namespace HotelManager.BLL.Interfaces
{
    public interface IPersonsService
    {
        void Add(Person person);
        IEnumerable<Person> Get();
        void Remove(int id);
        Person Find(int id);
    }
}
