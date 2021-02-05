namespace HotelManager.DAL.Entities
{
    public class Worker : Person
    {
        public virtual Hotel Hotel { get; set; } // отель
        public virtual WeeklySchedule CleaningSchedule { get; set; } // график уборки. День недели - этаж
        public bool Working { get; set; } // true, если работник находится на работе, false - если уволен
        public decimal WorkdaySalary { get; set; } // зарплата за рабочий день
    }
}
