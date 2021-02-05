using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using HotelManager.BLL.Models;

namespace HotelManager.UI.ViewModels.Dialogs
{
    class HotelRoomDialogViewModel : ViewModelBase
    {
        public HotelRoomDialogViewModel(HotelRoom hotelRoom)
        {
            HotelRoom = hotelRoom;


        }

        private HotelRoom _hotelRoom;
        public HotelRoom HotelRoom
        {
            get => _hotelRoom;
            set => Set(ref _hotelRoom, value);
        }
    }
}
