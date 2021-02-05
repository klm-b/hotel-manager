using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using HotelManager.BLL.Models;

namespace HotelManager.UI.ViewModels.Dialogs
{
    class PersonDialogViewModel : ViewModelBase
    {
        public PersonDialogViewModel(Person person)
        {
            Person = person;


        }

        private Person _person;
        public Person Person
        {
            get => _person;
            set => Set(ref _person, value);
        }
    }
}
