using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace HotelManager.BLL.Models
{
    // человек
    public class Person : ObservableObject
    {
        #region Поля
        private string _surname; // фамилия
        private string _name; // имя
        private string _patronymic; // отчество
        private string _passportNumber; // номер паспорта
        private string _email; // электоронная почта
        private string _phoneNumber; // номер телефона
        #endregion

        #region Свойства
        public int Id { get; set; }

        public string Surname
        {
            get => _surname;
            set => Set(ref _surname, value);
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public string Patronymic
        {
            get => _patronymic;
            set => Set(ref _patronymic, value);
        }

        public string PassportNumber
        {
            get => _passportNumber;
            set => Set(ref _passportNumber, value);
        }

        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => Set(ref _phoneNumber, value);
        }

        #endregion

        // словарь названий полей
        public static readonly Dictionary<string, string> Fields = new Dictionary<string, string>()
        {
            ["Id"] = "Идентификатор",
            ["Surname"] = "Фамилия",
            ["Name"] = "Имя",
            ["Patronymic"] = "Отчество",
            ["PassportNumber"] = "Номер паспорта",
            ["Email"] = "Электронная почта",
            ["PhoneNumber"] = "Номер телефона"
        };
    }
}
