using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.BLL.Models
{

    public class WeeklySchedule : ObservableObject
    {
        public int Id { get; set; }
        public Worker Worker { get; set; }

        private int? _monday;
        private int? _tuesday;
        private int? _wednesday;
        private int? _thursday;
        private int? _friday;
        private int? _saturday;
        private int? _sunday;

        public int? Monday
        {
            get => _monday;
            set => Set(ref _monday, value);
        }

        public int? Tuesday
        {
            get => _tuesday;
            set => Set(ref _tuesday, value);
        }

        public int? Wednesday
        {
            get => _wednesday;
            set => Set(ref _wednesday, value);
        }

        public int? Thursday
        {
            get => _thursday;
            set => Set(ref _thursday, value);
        }

        public int? Friday
        {
            get => _friday;
            set => Set(ref _friday, value);
        }

        public int? Saturday
        {
            get => _saturday;
            set => Set(ref _saturday, value);
        }

        public int? Sunday
        {
            get => _sunday;
            set => Set(ref _sunday, value);
        }

        public int WorkingDays => new List<int?>{Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday}.Count(i => i != null);
        public Dictionary<string, int?> Schedule => new Dictionary<string, int?>()
        {
            ["Понедельник"] = Monday,
            ["Вторник"] = Tuesday,
            ["Среда"] = Wednesday,
            ["Четверг"] = Thursday,
            ["Пятница"] = Friday,
            ["Суббота"] = Saturday,
            ["Воскресенье"] = Sunday
        };
    }
}
