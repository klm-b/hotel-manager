using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace HotelManager.UI
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Set |DataDirectory| value
            var dataDirectory = $"{Environment.CurrentDirectory}\\App_Data\\";
            Directory.CreateDirectory(dataDirectory);
            AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);
            
            DispatcherHelper.Initialize();
        }
    }
}
