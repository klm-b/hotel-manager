using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HotelManager.BLL.Interfaces;
using HotelManager.BLL.Models;
using HotelManager.UI.Interfaces;
using HotelManager.UI.ViewModels.Dialogs;
using HotelManager.UI.ViewModels.EditDialogs;

namespace HotelManager.UI.ViewModels.Pages
{
    public class WorkersPageViewModel : ViewModelBase
    {
        private const string AnyFieldFilterValue = "AnyField";
        private const string NoSortingSortValue = "NoSorting";

        private readonly IWorkersService _workersService;
        private readonly IDialogHost _dialogHost;

        public WorkersPageViewModel(IWorkersService workersService, IDialogHost dialogHost)
        {
            _workersService = workersService;
            _dialogHost = dialogHost;


            Workers = new ObservableCollection<Worker>(_workersService.Get());

            WorkersViews = (CollectionView)CollectionViewSource.GetDefaultView(Workers);
            WorkersViews.Filter = WorkersFilter;

            FilteredFields = new Dictionary<string, string>(Worker.Fields) { { AnyFieldFilterValue, "Любое поле" } };
            SelectedFilterField = AnyFieldFilterValue;
            IsContainsFilter = true;

            SortedFields = new Dictionary<string, string>(Worker.Fields) { { NoSortingSortValue, "Не сортировать" } };
            SelectedSortField = NoSortingSortValue;
            IsAscendingSort = true;

            IsExcludeLaidOffWorkers = false;
        }

        private ObservableCollection<Worker> _workers;
        public ObservableCollection<Worker> Workers
        {
            get => _workers;
            set => Set(ref _workers, value);
        }

        private string _filterText;
        public string FilterText
        {
            get => _filterText;
            set
            {
                Set(ref _filterText, value); 
                WorkersViews.Refresh();
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


        // исключить уволенных работников
        private bool _isExcludeLaidOffWorkers;
        public bool IsExcludeLaidOffWorkers
        {
            get => _isExcludeLaidOffWorkers;
            set => Set(ref _isExcludeLaidOffWorkers, value);
        }

        // представление коллекции персон 
        // используются для фильтрации, сортировки и группировки выводимых данных
        public CollectionView WorkersViews { get; set; }


        private Worker _selectedWorker;
        public Worker SelectedWorker
        {
            get => _selectedWorker;
            set => Set(ref _selectedWorker, value);
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

        public int FilteredWorkersCount => WorkersViews.Count;
        public decimal TotalWorkersSalary => Workers.Sum(w => w.WorkdaySalary);



        // применение заданных настроек фильтрации
        private RelayCommand _filterCommand;
        public ICommand FilterCommand
            => _filterCommand ?? (_filterCommand = new RelayCommand(ExecuteFilterCommand));

        private void ExecuteFilterCommand()
        {
            WorkersViews.Filter = WorkersFilter;
            IsFilterPopupBoxOpen = false;
            RaisePropertyChanged(nameof(FilteredWorkersCount));
        }

        private bool WorkersFilter(object obj)
        {
            if (!(obj is Worker worker)) return true;

            // свойства объекта "Работник"
            var fields = typeof(Worker).GetProperties();

            // возврат false, если работник уволен и выброан чекбокс "Исключать уволенных работников"
            if (IsExcludeLaidOffWorkers &&
                !(bool)fields.First(p => p.Name == "Working").GetValue(worker, null)) { return false; }

            // если запрос пуст - подходят все
            if (string.IsNullOrEmpty(FilterText)) return true;

            // текст для поиск и фильтрации
            var filterText = FilterText.ToLower();

            // если фильтрация идет по всем полям, мы получаем значения всех полей и возвращаем true,
            // когда хоть бы одно поле содержит или полностью соответствует запросу(в зависимости от выбраного режима)
            if (SelectedFilterField == AnyFieldFilterValue)
                return fields.Select(x => x.GetValue(worker, null))
                .Any(p => IsContainsFilter 
                    ? p.ToString().ToLower().Contains(filterText) 
                    : p.ToString().ToLower().Equals(filterText));

            // если фильтрация идет по одному полю, мы сначала получаем значение этого поля
            var value = fields.First(p => p.Name == SelectedFilterField)
                .GetValue(worker, null).ToString().ToLower();

            // возвращаем true, когда хоть бы одно поле содержит или полностью
            // соответствует запросу(в зависимости от выбраного режима)
            return IsContainsFilter ? value.Contains(filterText) : value.Equals(filterText);
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
            IsExcludeLaidOffWorkers = false;
            WorkersViews.Filter = null;
            RaisePropertyChanged(nameof(FilteredWorkersCount));
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


            WorkersViews.SortDescriptions.Clear();
            WorkersViews.SortDescriptions.Add(new SortDescription(SelectedSortField,
                IsAscendingSort ? ListSortDirection.Ascending : ListSortDirection.Descending));
        }

        // сброс сортировки
        private void ExecuteResetSortCommand()
        {
            IsSortPopupBoxOpen = false;
            SelectedSortField = NoSortingSortValue;
            WorkersViews.SortDescriptions.Clear();
        }

        // вывод диалога с подробной информацией ResetFilterCommand
        private RelayCommand<Worker> _showWorkerDetailsDialogCommand;
        public ICommand ShowWorkerDetailsDialogCommand
            => _showWorkerDetailsDialogCommand ?? (_showWorkerDetailsDialogCommand = new RelayCommand<Worker>(ExecuteShowWorkerDetailsDialog));

        private async void ExecuteShowWorkerDetailsDialog(Worker worker)
        {
            if (worker == null) return;

            // создание ViewModel'и для отображения подробной информации
            var vm = new WorkerDialogViewModel(worker);

            // вывод
            var result = await _dialogHost.Show(vm, "RootDialog");

            if (result != null && result is bool b && !b)
            {
                var editVm = new WorkerEditDialogViewModel();

                // вывод
                await _dialogHost.Show(editVm, "RootDialog");
            }
        }
    }
}
