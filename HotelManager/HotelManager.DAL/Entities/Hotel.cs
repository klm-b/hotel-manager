using System.Collections.Generic;

namespace HotelManager.DAL.Entities
{
    public class Hotel : Entity
    {
        public string Name { get; set; } // название гостиницы
        public string Address { get; set; } // адрес
        public int FloorCount { get; set; } // кол-во этажей
        public virtual ICollection<HotelRoom> HotelRooms { get; set; } // номера гостиницы
        public virtual ICollection<Worker> Workers { get; set; } // работники отеля
    }
}
