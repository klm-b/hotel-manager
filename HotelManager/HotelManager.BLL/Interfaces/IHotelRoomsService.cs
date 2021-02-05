using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.BLL.Models;

namespace HotelManager.BLL.Interfaces
{
    public interface IHotelRoomsService
    {
        void Add(HotelRoom hotelRoom);
        IEnumerable<HotelRoom> Get();
        void Remove(int id);
        HotelRoom Find(int id);
    }
}
