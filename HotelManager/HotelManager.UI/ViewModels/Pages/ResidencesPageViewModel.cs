using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HotelManager.BLL.Interfaces;
using HotelManager.BLL.Models;
using HotelManager.UI.Interfaces;
using HotelManager.UI.ViewModels.Dialogs;
using HotelManager.UI.ViewModels.EditDialogs;

namespace HotelManager.UI.ViewModels.Pages
{
    public class ResidencesPageViewModel : ViewModelBase

    {
        private readonly IResidencesService _residencesService;
        private readonly IDialogHost _dialogHost;
        private readonly Hotel _hotel;

        public ResidencesPageViewModel(IResidencesService residencesService, IDialogHost dialogHost, Hotel hotel)
        {
            _residencesService = residencesService;
            _dialogHost = dialogHost;
            _hotel = hotel;

            Residences = new ObservableCollection<Residence>(_residencesService.Get());

        }

        private ObservableCollection<Residence> _residences;
        public ObservableCollection<Residence> Residences
        {
            get => _residences;
            set => Set(ref _residences, value);
        }

        private Residence _selectedResidence;
        public Residence SelectedResidence
        {
            get => _selectedResidence;
            set => Set(ref _selectedResidence, value);
        }

        // вывод диалога с подробной информацией
        private RelayCommand<Residence> _showResidenceDetailsDialogCommand;
        public ICommand ShowResidenceDetailsDialogCommand
            => _showResidenceDetailsDialogCommand ?? (_showResidenceDetailsDialogCommand = new RelayCommand<Residence>(ExecuteShowResidenceDetailsDialog));

        private async void ExecuteShowResidenceDetailsDialog(Residence residence)
        {
            if (residence == null) return;

            // создание ViewModel'и для отображения подробной информации
            var vm = new ResidenceDialogViewModel(residence);

            // вывод
            var result = await _dialogHost.Show(vm, "RootDialog");

            if (result == null || !(result is bool b)) return;

            // выписывание чека
            if (b)
            {
                var checkVm = new CheckDialogViewModel(_hotel, residence);

                // вывод
                await _dialogHost.Show(checkVm, "RootDialog");
            }
            else
            {
                var editVm = new ResidenceEditDialogViewModel();

                // вывод
                await _dialogHost.Show(editVm, "RootDialog");
            }
        }
    }
}
