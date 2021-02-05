using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HotelManager.BLL.Interfaces;
using HotelManager.BLL.Models;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using HotelManager.UI.Interfaces;
using HotelManager.UI.ViewModels.Dialogs;
using HotelManager.UI.ViewModels.Pages;
using HotelReportsModel;

namespace HotelManager.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IDialogHost _dialogHost;

        public MainViewModel(ISnackbarMessageQueue snackbarMessageQueue, IDataService dataService, IDialogHost dialogHost)
        {
            SnackbarMessageQueue = snackbarMessageQueue;
            _dataService = dataService;
            _dialogHost = dialogHost;

            _dataService.Initialize();

            Hotel = _dataService.Hotel.Get();

            HamburgerMenuSelectedOptionsIndex = -1;

            SelectedClient = ClientsPageViewModel.Clients.First();
        }


        // ViewModels
        private PersonsPageViewModel _personsPageViewModel;
        public PersonsPageViewModel PersonsPageViewModel =>
            _personsPageViewModel ?? (_personsPageViewModel = new PersonsPageViewModel(_dataService.Persons, _dialogHost));

        private ClientsPageViewModel _clientsPageViewModel;
        public ClientsPageViewModel ClientsPageViewModel =>
            _clientsPageViewModel ?? (_clientsPageViewModel = new ClientsPageViewModel(_dataService.Clients, _dataService.Residences, SnackbarMessageQueue, _dialogHost, Hotel));

        private WorkersPageViewModel _workersPageViewModel;
        public WorkersPageViewModel WorkersPageViewModel =>
            _workersPageViewModel ?? (_workersPageViewModel = new WorkersPageViewModel(_dataService.Workers, _dialogHost));

        private WeeklySchedulesPageViewModel _weeklySchedulesPageViewModel;
        public WeeklySchedulesPageViewModel WeeklySchedulesPageViewModel =>
            _weeklySchedulesPageViewModel ?? (_weeklySchedulesPageViewModel = new WeeklySchedulesPageViewModel(_dataService.Schedules, _dialogHost));

        private HotelRoomsPageViewModel _hotelRoomsPageViewModel;
        public HotelRoomsPageViewModel HotelRoomsPageViewModel =>
            _hotelRoomsPageViewModel ?? (_hotelRoomsPageViewModel = new HotelRoomsPageViewModel(_dataService.HotelRooms, _dialogHost));

        private ResidencesPageViewModel _residencesPageViewModel;
        public ResidencesPageViewModel ResidencesPageViewModel =>
            _residencesPageViewModel ?? (_residencesPageViewModel = new ResidencesPageViewModel(_dataService.Residences, _dialogHost, Hotel));

        private InfoPageViewModel _infoPageViewModel;
        public InfoPageViewModel InfoPageViewModel =>
            _infoPageViewModel ?? (_infoPageViewModel = new InfoPageViewModel(_dialogHost));

        private SettingsPageViewModel _settingsPageViewModel;
        public SettingsPageViewModel SettingsPageViewModel =>
            _settingsPageViewModel ?? (_settingsPageViewModel = new SettingsPageViewModel());


        private ISnackbarMessageQueue _snackbarMessageQueue;
        public ISnackbarMessageQueue SnackbarMessageQueue
        {
            get => _snackbarMessageQueue;
            set => Set(ref _snackbarMessageQueue, value);
        }

        private Hotel _hotel;
        public Hotel Hotel
        {
            get => _hotel;
            set => Set(ref _hotel, value);
        }

        private bool _isRootDialogOpened;
        public bool IsRootDialogOpened
        {
            get => _isRootDialogOpened;
            set => Set(ref _isRootDialogOpened, value);
        }

        private RelayCommand<string> _showPageCommand;
        public ICommand ShowPageCommand =>
            _showPageCommand ?? (_showPageCommand = new RelayCommand<string>(PageShowExecute));

        private void PageShowExecute(string index) => HamburgerMenuSelectedIndex = int.Parse(index);


        private RelayCommand<string> _showOptionsPageCommand;
        public ICommand ShowOptionsPageCommand =>
            _showOptionsPageCommand ?? (_showOptionsPageCommand = new RelayCommand<string>(OptionsPageShowExecute));

        private void OptionsPageShowExecute(string index) => HamburgerMenuSelectedOptionsIndex = int.Parse(index);


        private int _hamburgerMenuSelectedIndex;
        public int HamburgerMenuSelectedIndex
        {
            get => _hamburgerMenuSelectedIndex;
            set => Set(ref _hamburgerMenuSelectedIndex, value);
        }

        private int _hamburgerMenuSelectedOptionsIndex;
        public int HamburgerMenuSelectedOptionsIndex
        {
            get => _hamburgerMenuSelectedOptionsIndex;
            set => Set(ref _hamburgerMenuSelectedOptionsIndex, value);
        }

        private TransitionType _selectedTransitionType;
        public TransitionType SelectedTransitionType
        {
            get => _selectedTransitionType;
            set => Set(ref _selectedTransitionType, value);
        }

        private string _reportName;
        public string ReportName
        {
            get => _reportName;
            set => Set(ref _reportName, value);
        }

        public DateTime TodayDate => DateTime.Today;

        private DateTime? _selectedDateFrom;
        public DateTime? SelectedDateFrom
        {
            get => _selectedDateFrom;
            set => Set(ref _selectedDateFrom, value);
        }

        private DateTime? _selectedDateTo;
        public DateTime? SelectedDateTo
        {
            get => _selectedDateTo;
            set => Set(ref _selectedDateTo, value);
        }

        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set => Set(ref _selectedClient, value);
        }

        
        // отправка тестового отчета
        private RelayCommand _sendTestReportCommand;

        public ICommand SendTestReportCommand =>
            _sendTestReportCommand ?? (_sendTestReportCommand = new RelayCommand(SendTestReportExecute));
        private void SendTestReportExecute()
        {
            if (string.IsNullOrWhiteSpace(ReportName))
            {
                _snackbarMessageQueue.Enqueue("Название отчета не может быть пустым.");
                return;
            }

            if (SelectedDateTo == null || SelectedDateFrom == null || SelectedDateTo <= SelectedDateFrom)
            {
                _snackbarMessageQueue.Enqueue("Введите корректные даты, для построения отчета.");
                return;
            }


            // построение отчета
            var report = new HotelReport
            {
                HotelName = Hotel.Name,
                PeriodStart = (DateTime)SelectedDateFrom,
                PeriodEnd = (DateTime)SelectedDateTo,
                ClientsCount = ResidencesPageViewModel.Residences.Count(r => SelectedDateFrom < r.CheckInDate && r.CheckOutDate < SelectedDateTo),
                WorkersCount = WorkersPageViewModel.Workers.Count,
                TotalIncome = (decimal)ResidencesPageViewModel.Residences.Sum(r => SelectedDateFrom < r.CheckInDate && r.CheckOutDate < SelectedDateTo
                    ? r.HotelRoom.Price * (SelectedDateTo - SelectedDateFrom).Value.TotalDays : 0),
                RoomReports = ResidencesPageViewModel.Residences
                    .GroupBy(r => r.HotelRoom.Id)
                    .Select(g => new RoomReport()
                    {
                        Number = g.Key,
                        ClientsCount = g.Count(),
                        DaysWhenOccupied = Convert.ToInt32(g.Sum(r => (r.CheckOutDate - r.CheckInDate).TotalDays)),
                        RoomType = g.First().HotelRoom.RoomType,
                        DaysWhenFree = Convert.ToInt32((SelectedDateTo - SelectedDateFrom).Value.TotalDays - g.Sum(r => (r.CheckOutDate - r.CheckInDate).TotalDays)),
                        TotalIncome = (decimal)(Convert.ToInt32(g.Sum(r => (r.CheckOutDate - r.CheckInDate).TotalDays)) * g.First().HotelRoom.Price)
                    }).ToList(),
                WorkerReports = WorkersPageViewModel.Workers
                    .Select(w => new WorkerReport()
                    {
                        PassportNumber = w.PassportNumber,
                        EmploymentDate = SelectedDateFrom.Value.AddDays(-100)/*TODO: добавить рабтнику запись о дате принятия на работу*/,
                        Salary = Convert.ToInt32((SelectedDateTo - SelectedDateFrom).Value.TotalDays * (double)w.WorkdaySalary),
                        WorkDaysCount = Convert.ToInt32((SelectedDateTo - SelectedDateFrom).Value.TotalDays)
                    }).ToList()
            };


            try
            {
                // создание клиента
                var client = new HotelReportsClient.HotelReportsClient((new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888)));

                // отправка отчета на сервер
                client.SendReport(report, $"{ReportName}.json");

                // выход
                client.SendCommand("@@@exit");
            }
            catch (Exception ex)
            {
                _snackbarMessageQueue.Enqueue(ex.Message);
                return;
            }

            _snackbarMessageQueue.Enqueue("Отчет успешно отправлен.");
        }


        // создание чека
        private RelayCommand<Client> _createCheckCommand;
        public ICommand CreateCheckCommand =>
            _createCheckCommand ?? (_createCheckCommand = new RelayCommand<Client>(ExecuteCreateCheckCommand));

        private async void ExecuteCreateCheckCommand(Client client)
        {
            if (client == null) return;

            var residence = ResidencesPageViewModel.Residences.FirstOrDefault(r =>
                client.Id == r.Client.Id && r.CheckInDate < DateTime.Today && r.CheckOutDate >= DateTime.Today);

            if (residence == null)
            {
                _snackbarMessageQueue.Enqueue("Выбранный клиент не проживает в отеле на данный момент.");
                return;
            }

            var vm = new CheckDialogViewModel(Hotel, residence);

            // вывод
            var result = await _dialogHost.Show(vm, "RootDialog");
        }


        public ICommand WindowClosing
        {
            get
            {
                return new RelayCommand<CancelEventArgs>(
                    (args) =>
                    {
                        _dataService.SaveChanges();
                    });
            }
        }
    }
}
