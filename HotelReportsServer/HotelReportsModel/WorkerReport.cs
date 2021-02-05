using System;

namespace HotelReportsModel
{
    public class WorkerReport
    {
        public string PassportNumber { get; set; } // номер паспорта
        public DateTime EmploymentDate { get; set; } // дата приема на работу
        public int WorkDaysCount { get; set; } // кол-во рабочих дней за указанный период
        public double Salary { get; set; } // зарплата
    }
}
