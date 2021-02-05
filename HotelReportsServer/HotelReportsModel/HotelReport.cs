using System;
using System.Collections.Generic;

namespace HotelReportsModel
{
    public class HotelReport
    {
        public string HotelName { get; set; } // название гостиницы
        public DateTime PeriodStart { get; set; } // начало периода
        public DateTime PeriodEnd { get; set; } // конец периода
        public int WorkersCount { get; set; } // кол-во сотрудников на данный момент
        public int ClientsCount { get; set; } // кол-во клиентов за весь период
        public List<RoomReport> RoomReports { get; set; } // отчеты по каждому номеру
        public List<WorkerReport> WorkerReports { get; set; } // отчет о работе сотрудников
        public decimal TotalIncome { get; set; } // общий доход
    }
}
