using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.BLL.Models
{
    public class Worker : Person
    {
        #region Поля

        private WeeklySchedule _cleaningSchedule; // график уборки. День недели - этаж
        private bool _working; // true, если работник находится на работе, false - если уволен
        private decimal _workdaySalary; // зарплата за рабочий день

        #endregion

        #region Свойства

        public WeeklySchedule CleaningSchedule
        {
            get => _cleaningSchedule;
            set => Set(ref _cleaningSchedule, value);
        }

        public bool Working
        {
            get => _working;
            set => Set(ref _working, value);
        }

        public decimal WorkdaySalary
        {
            get => _workdaySalary;
            set => Set(ref _workdaySalary, value);
        }

        #endregion

        // словарь названий полей
        public new static readonly Dictionary<string, string> Fields
            = new Dictionary<string, string>(Person.Fields)
            {
                ["WorkdaySalary"] = "Зарплата"
            };
    }
}
