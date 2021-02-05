using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HotelManager.UI.Styles;
using MahApps.Metro.Controls;

namespace HotelManager.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            MaterialDesignWindow.RegisterCommands(this);
            InitializeComponent();

        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            HamburgerMenuControl.Content = (e.InvokedItem as HamburgerMenuIconItem).Tag;
        }

        private void HamburgerMenuControl_OnItemClick(object sender, ItemClickEventArgs args)
        {
            HamburgerMenuControl.Content = args.ClickedItem;
        }
    }
}
