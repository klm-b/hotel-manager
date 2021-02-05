using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.UI.Interfaces;
using MaterialDesignThemes.Wpf;

namespace HotelManager.UI.Infrastructure
{
    public class EmbeddedDialogHost : IDialogHost
    {
        public Task<object> Show(object content, object dialogId)
        {
            return DialogHost.Show(content, dialogId);
        }
    }
}
