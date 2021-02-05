using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace HotelManager.BLL.Models
{
    public class HotelRoom : ObservableObject
    {
        #region Поля

        private int _roomType; // тип номера. Кол-во мест для проживания
        private int _floor; // этаж
        private string _phoneNumber; // номер телефона в номере
        private double _price; // стоимость проживания в сутки

        #endregion

        #region Свойства
        public int Id { get; set; }


        public int RoomType
        {
            get => _roomType;
            set => Set(ref _roomType, value);
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => Set(ref _phoneNumber, value);
        }

        public double Price
        {
            get => _price;
            set => Set(ref _price, value);
        }

        public int Floor
        {
            get => _floor;
            set => Set(ref _floor, value);
        }


        #endregion

    }
}
