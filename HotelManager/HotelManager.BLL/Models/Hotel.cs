using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.BLL.Models
{
    public class Hotel : ObservableObject
    {
        #region Поля

        private string _name; // название гостиницы
        private string _address; // адрес
        private int _floorCount; // кол-во этажей

        #endregion

        #region Свойства
        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public string Address
        {
            get => _address;
            set => Set(ref _address, value);
        }

        public int FloorCount
        {
            get => _floorCount;
            set => Set(ref _floorCount, value);
        }

        #endregion
    }
}
