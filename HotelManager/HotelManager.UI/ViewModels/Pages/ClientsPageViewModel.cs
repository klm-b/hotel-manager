using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HotelManager.BLL.Interfaces;
using HotelManager.BLL.Models;
using HotelManager.UI.Interfaces;
using HotelManager.UI.ViewModels.Dialogs;
using HotelManager.UI.ViewModels.EditDialogs;
using MaterialDesignThemes.Wpf;

namespace HotelManager.UI.ViewModels.Pages
{
    public class ClientsPageViewModel : ViewModelBase
    {
        private const string AnyFieldFilterValue = "AnyField";
        private const string NoSortingSortValue = "NoSorting";

        private readonly IClientsService _clientsService;
        private readonly IResidencesService _residencesService;
        private readonly ISnackbarMessageQueue _snackbarMessageQueue;
        private readonly Hotel _hotel;
        private readonly IDialogHost _dialogHost;

        public ClientsPageViewModel(IClientsService clientsService, IResidencesService residencesService, ISnackbarMessageQueue snackbarMessageQueue, IDialogHost dialogHost, Hotel hotel)
        {
            _clientsService = clientsService;
            _residencesService = residencesService;
            _snackbarMessageQueue = snackbarMessageQueue;
            _hotel = hotel;
            _dialogHost = dialogHost;


            Clients = new ObservableCollection<Client>(_clientsService.Get());

            ClientsViews = (CollectionView)CollectionViewSource.GetDefaultView(Clients);
            ClientsViews.Filter = ClientsFilter;


            FilteredFields = new Dictionary<string, string>(Client.Fields) { { AnyFieldFilterValue, "Любое поле" } };
            SelectedFilterField = AnyFieldFilterValue;
            IsContainsFilter = true;

            SortedFields = new Dictionary<string, string>(Client.Fields) { { NoSortingSortValue, "Не сортировать" } };
            SelectedSortField = NoSortingSortValue;
            IsAscendingSort = true;
        }

        private ObservableCollection<Client> _clients;
        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set => Set(ref _clients, value);
        }

        private string _filterText;
        public string FilterText
        {
            get => _filterText;
            set
            {
                Set(ref _filterText, value);
                ClientsViews.Refresh();
            }
        }

        // словарь полей, по которым может идти фильтрация
        private Dictionary<string, string> _filteredFields;
        public Dictionary<string, string> FilteredFields
        {
            get => _filteredFields;
            set => Set(ref _filteredFields, value);
        }

        // словарь полей, по которым может идти сортировка
        private Dictionary<string, string> _sortedFields;
        public Dictionary<string, string> SortedFields
        {
            get => _sortedFields;
            set => Set(ref _sortedFields, value);
        }

        // название выбранного для фильтрации поля
        private string _selectedFilterField;
        public string SelectedFilterField
        {
            get => _selectedFilterField;
            set => Set(ref _selectedFilterField, value);
        }

        // название выбранного для сортировки поля
        private string _selectedSortField;
        public string SelectedSortField
        {
            get => _selectedSortField;
            set => Set(ref _selectedSortField, value);
        }


        // отбор свойств содержащих заданныое значение, или полностью совпадающих
        private bool _isContainsFilter;
        public bool IsContainsFilter
        {
            get => _isContainsFilter;
            set => Set(ref _isContainsFilter, value);
        }

        // сортировка идет по возрастанию или нет
        private bool _isAscendingSort;
        public bool IsAscendingSort
        {
            get => _isAscendingSort;
            set => Set(ref _isAscendingSort, value);
        }

        // представление коллекции персон 
        // используются для фильтрации, сортировки и группировки выводимых данных
        public CollectionView ClientsViews { get; set; }


        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set => Set(ref _selectedClient, value);
        }

        private bool _isFilterPopupBoxOpen;
        public bool IsFilterPopupBoxOpen
        {
            get => _isFilterPopupBoxOpen;
            set => Set(ref _isFilterPopupBoxOpen, value);
        }

        private bool _isSortPopupBoxOpen;
        public bool IsSortPopupBoxOpen
        {
            get => _isSortPopupBoxOpen;
            set => Set(ref _isSortPopupBoxOpen, value);
        }

        public int FilteredClientsCount => ClientsViews.Count;



        // применение заданных настроек фильтрации
        private RelayCommand _filterCommand;
        public ICommand FilterCommand
            => _filterCommand ?? (_filterCommand = new RelayCommand(ExecuteFilterCommand));

        private void ExecuteFilterCommand()
        {
            ClientsViews.Filter = ClientsFilter;
            IsFilterPopupBoxOpen = false;
            RaisePropertyChanged(nameof(FilteredClientsCount));
        }

        private bool ClientsFilter(object obj)
        {
            if (!(obj is Client client) || string.IsNullOrEmpty(FilterText)) return true;

            var fields = typeof(Client).GetProperties();

            // если фильтрация идет по всем полям, мы получаем значения всех полей и возвращаем true,
            // когда хоть бы одно поле содержит или полностью соответствует запросу(в зависимости от выбраного режима)
            if (SelectedFilterField == AnyFieldFilterValue)
                return fields.Select(x => x.GetValue(client, null))
                .Any(p => IsContainsFilter ? p.ToString().ToLower().Contains(FilterText.ToLower()) : p.ToString().Equals(FilterText.ToLower()));

            // если фильтрация идет по одному полю, мы сначала получаем значение этого поля
            var value = fields.First(p => p.Name == SelectedFilterField)
                .GetValue(client, null).ToString().ToLower();

            // возвращаем true, когда хоть бы одно поле содержит или полностью
            // соответствует запросу(в зависимости от выбраного режима)
            return IsContainsFilter ? value.Contains(FilterText.ToLower()) : value.Equals(FilterText.ToLower());
        }

        // сброс настроек
        private RelayCommand _resetFilterCommand;
        public ICommand ResetFilterCommand
            => _resetFilterCommand ?? (_resetFilterCommand = new RelayCommand(ExecuteResetFilterCommand));

        private void ExecuteResetFilterCommand()
        {
            IsFilterPopupBoxOpen = false;
            FilterText = null;
            SelectedFilterField = AnyFieldFilterValue;
            IsContainsFilter = true;
            ClientsViews.Filter = null;
            RaisePropertyChanged(nameof(FilteredClientsCount));
        }

        // применение настроек сортировки
        private RelayCommand _sortCommand;
        public ICommand SortCommand => _sortCommand ?? (_sortCommand = new RelayCommand(ExecuteSortCommand));

        // сброс настроек сортировки
        private RelayCommand _resetSortCommand;
        public ICommand ResetSortCommand
            => _resetSortCommand ?? (_resetSortCommand = new RelayCommand(ExecuteResetSortCommand));

        // применение заданных настроек сортировки 
        private void ExecuteSortCommand()
        {
            IsSortPopupBoxOpen = false;

            // при выборе варианта не сортировать - настройки сортировки сбрасываются
            if (SelectedSortField == NoSortingSortValue)
            {
                ResetSortCommand.Execute(null);
                return;
            }


            ClientsViews.SortDescriptions.Clear();
            ClientsViews.SortDescriptions.Add(new SortDescription(SelectedSortField,
                IsAscendingSort ? ListSortDirection.Ascending : ListSortDirection.Descending));
        }

        // сброс сортировки
        private void ExecuteResetSortCommand()
        {
            IsSortPopupBoxOpen = false;
            SelectedSortField = NoSortingSortValue;
            ClientsViews.SortDescriptions.Clear();
        }

        // вывод диалога с подробной информацией
        private RelayCommand<Client> _showClientDetailsDialogCommand;
        public ICommand ShowClientDetailsDialogCommand
            => _showClientDetailsDialogCommand ?? (_showClientDetailsDialogCommand = new RelayCommand<Client>(ExecuteShowClientDetailsDialog));

        private async void ExecuteShowClientDetailsDialog(Client client)
        {
            if (client == null) return;

            // создание ViewModel'и для отображения подробной информации
            var vm = new ClientDialogViewModel(client);

            // вывод
            var result = await _dialogHost.Show(vm, "RootDialog");

            if(result == null || !(result is bool b)) return;

            // выписывание чека
            if (b)
            {
                var residence = _residencesService.Get().FirstOrDefault(r =>
                    client.Id == r.Client.Id && r.CheckInDate < DateTime.Today && r.CheckOutDate >= DateTime.Today);

                if (residence == null)
                {
                    _snackbarMessageQueue.Enqueue("Выбранный клиент не проживает в отеле на данный момент.");
                    return;
                }

                var checkVm = new CheckDialogViewModel(_hotel, residence);

                // вывод
                await _dialogHost.Show(checkVm, "RootDialog");
            }
            else
            {
                var editVm = new ClientEditDialogViewModel();

                // вывод
                await _dialogHost.Show(editVm, "RootDialog");
            }
        }
    }
}
