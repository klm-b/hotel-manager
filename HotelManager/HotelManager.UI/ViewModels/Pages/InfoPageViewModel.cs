using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Threading;
using HotelManager.UI.Common;
using HotelManager.UI.Interfaces;
using MaterialDesignThemes.Wpf;

namespace HotelManager.UI.ViewModels.Pages
{
    public class InfoPageViewModel : ViewModelBase
    {
        private readonly IDialogHost _dialogHost;

        public InfoPageViewModel(IDialogHost dialogHost)
        {
            _dialogHost = dialogHost;
        }

        private RelayCommand<string> _openLinkInBrowserCommand;
        public ICommand OpenLinkInBrowserCommand =>
            _openLinkInBrowserCommand ?? (_openLinkInBrowserCommand =
                new RelayCommand<string>(o =>
                {
                    if (o is string link) Link.OpenInBrowser(link);
                }));
    }
}
