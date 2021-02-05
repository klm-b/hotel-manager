namespace HotelManager.DAL.Entities
{
    // человек
    public class Person : Entity
    {
        public string Surname { get; set; } // фамилия
        public string Name { get; set; } // имя
        public string Patronymic { get; set; } // отчество
        public string PassportNumber { get; set; } // номер паспорта
        public string Email { get; set; } // электоронная почта
        public string PhoneNumber { get; set; } // номер телефона
    }
}
