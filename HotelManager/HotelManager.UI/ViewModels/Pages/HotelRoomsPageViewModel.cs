using System.Collections.ObjectModel;
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
    public class HotelRoomsPageViewModel : ViewModelBase
    {
        private readonly IHotelRoomsService _hotelRoomsService;
        private readonly IDialogHost _dialogHost;

        public HotelRoomsPageViewModel(IHotelRoomsService hotelRoomsService, IDialogHost dialogHost)
        {
            _hotelRoomsService = hotelRoomsService;
            _dialogHost = dialogHost;

            HotelRooms = new ObservableCollection<HotelRoom>(_hotelRoomsService.Get());

            HotelRoomsViews = (CollectionView)CollectionViewSource.GetDefaultView(HotelRooms);
        }

        private ObservableCollection<HotelRoom> _hotelRooms;
        public ObservableCollection<HotelRoom> HotelRooms
        {
            get => _hotelRooms;
            set => Set(ref _hotelRooms, value);
        }

        // используются для фильтрации, сортировки и группировки выводимых данных
        public CollectionView HotelRoomsViews { get; set; }


        private HotelRoom _selectedHotelRoom;
        public HotelRoom SelectedHotelRoom
        {
            get => _selectedHotelRoom;
            set => Set(ref _selectedHotelRoom, value);
        }
        // вывод диалога с подробной информацией
        private RelayCommand<HotelRoom> _showPersonDetailsDialogCommand;
        public ICommand ShowHotelRoomDetailsDialogCommand
            => _showPersonDetailsDialogCommand ?? (_showPersonDetailsDialogCommand = new RelayCommand<HotelRoom>(ExecuteShowHotelRoomDetailsDialog));

        private async void ExecuteShowHotelRoomDetailsDialog(HotelRoom hotelRoom)
        {
            if (hotelRoom == null) return;

            // создание ViewModel'и для отображения подробной информации
            var vm = new HotelRoomDialogViewModel(hotelRoom);

            // вывод
            var result = await _dialogHost.Show(vm, "RootDialog");

            if (result != null && result is bool b && !b)
            {
                var editVm = new HotelRoomEditDialogViewModel();

                // вывод
                await _dialogHost.Show(editVm, "RootDialog");
            }
        }

    }
}
