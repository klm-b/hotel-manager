using System.Collections.Generic;

namespace HotelManager.DAL.Entities
{
    public class HotelRoom : Entity
    {
        public int RoomType { get; set; } // тип номера. Кол-во мест для проживания
        public int Floor { get; set; } // этаж 
        public string PhoneNumber { get; set; } // номер телефона в номере
        public double Price { get; set; } // стоимость проживания в сутки
        public virtual ICollection<Residence> Residences { get; set; } // проживания в этом номере
        public virtual Hotel Hotel { get; set; } // отель
    }
}
