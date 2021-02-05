using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using HotelManager.BLL.Interfaces;
using HotelManager.BLL.Models;
using HotelManager.UI.Interfaces;

namespace HotelManager.UI.ViewModels.Dialogs
{
    class ClientDialogViewModel : ViewModelBase
    {
        public ClientDialogViewModel(Client client)
        {
            Client = client;


        }

        private Client _client;
        public Client Client
        {
            get => _client;
            set => Set(ref _client, value);
        }
    }
}
