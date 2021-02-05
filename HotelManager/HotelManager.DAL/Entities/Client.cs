using System.Collections.Generic;

namespace HotelManager.DAL.Entities
{
    // клиент гостиницы
    public class Client : Person
    {
        public string City { get; set; } // город проживания
        public virtual ICollection<Residence> Residences { get; set; } // проживания клиента в гостинице
    }
}
