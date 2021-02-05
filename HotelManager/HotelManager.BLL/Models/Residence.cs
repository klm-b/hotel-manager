using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace HotelManager.BLL.Models
{
    // информация о проживании клиента
    public class Residence : ObservableObject
    {
        #region Поля

        private DateTime _checkInDate; // дата заселения
        private DateTime _checkOutDate; // выселения

        #endregion

        #region Свойства

        public int Id { get; set; }

        public Client Client { get; set; }
        public HotelRoom HotelRoom { get; set; }

        public DateTime CheckInDate
        {
            get => _checkInDate;
            set => Set(ref _checkInDate, value);
        }

        public DateTime CheckOutDate
        {
            get => _checkOutDate;
            set => Set(ref _checkOutDate, value);
        }

        #endregion
    }
}
