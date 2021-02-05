using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using HotelManager.BLL.Interfaces;
using HotelManager.BLL.Services;
using HotelManager.DAL.EF;
using HotelManager.DAL.Interfaces;
using HotelManager.DAL.Repositories;
using HotelManager.UI.Interfaces;
using HotelManager.UI.ViewModels;
using MaterialDesignThemes.Wpf;

namespace HotelManager.UI.Infrastructure
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {

            SimpleIoc.Default.Register<IDialogHost, EmbeddedDialogHost>();

            SimpleIoc.Default.Unregister<ISnackbarMessageQueue>();
            SimpleIoc.Default.Register<ISnackbarMessageQueue>(() => new SnackbarMessageQueue());

            SimpleIoc.Default.Register<IDatabaseContext, HotelDbContext>();
            SimpleIoc.Default.Register<IUnitOfWork, EFUnitOfWork>();

            if (/*ViewModelBase.IsInDesignModeStatic*/false)
            {
                // SimpleIoc.Default.Register<IDataService, TestDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }


            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel MainWindow => SimpleIoc.Default.GetInstance<MainViewModel>();
    }
}
