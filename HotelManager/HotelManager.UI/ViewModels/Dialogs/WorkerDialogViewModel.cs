using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using HotelManager.BLL.Models;

namespace HotelManager.UI.ViewModels.Dialogs
{
    class WorkerDialogViewModel : ViewModelBase
    {
        public WorkerDialogViewModel(Worker worker)
        {
            Worker = worker;


        }

        private Worker _worker;
        public Worker Worker
        {
            get => _worker;
            set => Set(ref _worker, value);
        }
    }
}
