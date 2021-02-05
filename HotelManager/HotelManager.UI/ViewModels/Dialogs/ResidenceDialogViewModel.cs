using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using HotelManager.BLL.Models;

namespace HotelManager.UI.ViewModels.Dialogs
{
    class ResidenceDialogViewModel : ViewModelBase

    {
        public ResidenceDialogViewModel(Residence residence)
        {
            Residence = residence;
        }

        private Residence _residence;
        public Residence Residence
        {
            get => _residence;
            set => Set(ref _residence, value);
        }
    }
}
