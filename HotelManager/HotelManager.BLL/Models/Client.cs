using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.BLL.Models
{
    // клиент гостиницы
    public class Client : Person
    {
        #region Поля
        private string _city; // город проживания
        #endregion

        #region Свойства

        public string City
        {
            get => _city;
            set => Set(ref _city, value);
        }
        #endregion

        // словарь названий полей
        public new static readonly Dictionary<string, string> Fields 
            = new Dictionary<string, string>(Person.Fields) {["City"] = "Город"};
    }
}
