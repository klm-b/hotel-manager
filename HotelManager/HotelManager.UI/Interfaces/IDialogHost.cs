using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.UI.Interfaces
{
    public interface IDialogHost
    {
        Task<object> Show(object content, object dialogId);
    } 
}
