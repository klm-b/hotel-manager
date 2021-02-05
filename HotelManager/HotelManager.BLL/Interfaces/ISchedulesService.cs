using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.BLL.Models;

namespace HotelManager.BLL.Interfaces
{
    public interface ISchedulesService
    {
        void Add(WeeklySchedule weeklySchedule);
        IEnumerable<WeeklySchedule> Get();
        void Remove(int id);
        WeeklySchedule Find(int id);
    }
}
