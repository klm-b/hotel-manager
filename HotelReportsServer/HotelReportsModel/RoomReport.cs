namespace HotelReportsModel
{
    public class RoomReport
    {
        public int Number { get; set; } // номер гостиничного номера
        public int RoomType { get; set; } // кол-во мест для проживания
        public int ClientsCount { get; set; } // число клиентов за указанный период
        public int DaysWhenFree { get; set; } // кол-во дней, в которые номер был занят
        public int DaysWhenOccupied { get; set; } // кол-во дней, в которые номер был свободен
        public decimal TotalIncome { get; set; } // общий доход
    }
}
