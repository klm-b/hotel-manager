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
using HotelManager.BLL.Interfaces;
using HotelManager.BLL.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HotelManager.UI.Interfaces;
using MaterialDesignThemes.Wpf;
using HotelManager.UI.ViewModels.Dialogs;
using HotelManager.UI.ViewModels.EditDialogs;

namespace HotelManager.UI.ViewModels.Pages
{
    public class PersonsPageViewModel : ViewModelBase
    {
        private const string AnyFieldFilterValue = "AnyField";
        private const string NoSortingSortValue = "NoSorting";

        private readonly IPersonsService _personsService;
        private readonly IDialogHost _dialogHost;

        public PersonsPageViewModel(IPersonsService personsService, IDialogHost dialogHost)
        {
            _personsService = personsService;
            _dialogHost = dialogHost;

            Persons = new ObservableCollection<Person>(_personsService.Get());


            PersonsViews = (CollectionView)CollectionViewSource.GetDefaultView(Persons);
            PersonsViews.Filter = PersonsFilter;

            FilteredFields = new Dictionary<string, string>(Person.Fields) { { AnyFieldFilterValue, "Любое поле" } };
            SelectedFilterField = AnyFieldFilterValue;
            IsContainsFilter = true;

            SortedFields = new Dictionary<string, string>(Person.Fields) { { NoSortingSortValue, "Не сортировать" } };
            SelectedSortField = NoSortingSortValue;
            IsAscendingSort = true;
        }

        private ObservableCollection<Person> _persons;
        public ObservableCollection<Person> Persons
        {
            get => _persons;
            set => Set(ref _persons, value);
        }

        private string _filterText;
        public string FilterText
        {
            get => _filterText;
            set
            {
                Set(ref _filterText, value);
                PersonsViews.Refresh();
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
        public CollectionView PersonsViews { get; set; }


        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get => _selectedPerson;
            set => Set(ref _selectedPerson, value);
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

        public int FilteredPersonsCount => PersonsViews.Count;



        // применение заданных настроек фильтрации
        private RelayCommand _filterCommand;
        public ICommand FilterCommand
            => _filterCommand ?? (_filterCommand = new RelayCommand(ExecuteFilterCommand));

        private void ExecuteFilterCommand()
        {
            PersonsViews.Filter = PersonsFilter;
            IsFilterPopupBoxOpen = false;
            RaisePropertyChanged(nameof(FilteredPersonsCount));
        }

        private bool PersonsFilter(object obj)
        {
            if (!(obj is Person person) || string.IsNullOrEmpty(FilterText)) return true;

            var fields = typeof(Person).GetProperties();

            // если фильтрация идет по всем полям, мы получаем значения всех полей и возвращаем true,
            // когда хоть бы одно поле содержит или полностью соответствует запросу(в зависимости от выбраного режима)
            if (SelectedFilterField == AnyFieldFilterValue)
                return fields.Select(x => x.GetValue(person, null))
                .Any(p => IsContainsFilter ? p.ToString().ToLower().Contains(FilterText.ToLower()) : p.ToString().ToLower().Equals(FilterText.ToLower()));

            // если фильтрация идет по одному полю, мы сначала получаем значение этого поля
            var value = fields.First(p => p.Name == SelectedFilterField)
                .GetValue(person, null).ToString().ToLower();

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
            PersonsViews.Filter = null;
            RaisePropertyChanged(nameof(FilteredPersonsCount));
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


            PersonsViews.SortDescriptions.Clear();
            PersonsViews.SortDescriptions.Add(new SortDescription(SelectedSortField,
                IsAscendingSort ? ListSortDirection.Ascending : ListSortDirection.Descending));
        }

        // сброс сортировки
        private void ExecuteResetSortCommand()
        {
            IsSortPopupBoxOpen = false;
            SelectedSortField = NoSortingSortValue;
            PersonsViews.SortDescriptions.Clear();
        }

        // вывод диалога с подробной информацией
        private RelayCommand<Person> _showPersonDetailsDialogCommand;
        public ICommand ShowPersonDetailsDialogCommand
            => _showPersonDetailsDialogCommand ?? (_showPersonDetailsDialogCommand = new RelayCommand<Person>(ExecuteShowPersonDetailsDialog));

        private async void ExecuteShowPersonDetailsDialog(Person person)
        {
            if (person == null) return;

            // создание ViewModel'и для отображения подробной информации
            var vm = new PersonDialogViewModel(person);

            // вывод
            var result = await _dialogHost.Show(vm, "RootDialog");

            if (result != null && result is bool b && !b)
            {
                var editVm = new PersonEditDialogViewModel();

                // вывод
                await _dialogHost.Show(editVm, "RootDialog");
            }
        }

    }
}
