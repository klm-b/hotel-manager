using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using HotelManager.BLL.Interfaces;
using HotelManager.BLL.Models;
using HotelManager.UI.Interfaces;

namespace HotelManager.UI.ViewModels.Pages
{
    public class WeeklySchedulesPageViewModel : ViewModelBase
    {
        private readonly ISchedulesService _schedulesService;
        private readonly IDialogHost _dialogHost;

        public WeeklySchedulesPageViewModel(ISchedulesService schedulesService, IDialogHost dialogHost)
        {
            _schedulesService = schedulesService;
            _dialogHost = dialogHost;


            WeeklySchedules = new ObservableCollection<WeeklySchedule>(_schedulesService.Get());

        }

        private ObservableCollection<WeeklySchedule> _weeklySchedules;
        public ObservableCollection<WeeklySchedule> WeeklySchedules
        {
            get => _weeklySchedules;
            set => Set(ref _weeklySchedules, value);
        }
    }
}
