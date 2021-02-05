namespace HotelManager.DAL.Entities
{
    // недельное расписание сотрудника
    public class WeeklySchedule : Entity
    {
        public int? Monday { get; set; }
        public int? Tuesday { get; set; }
        public int? Wednesday { get; set; }
        public int? Thursday { get; set; }
        public int? Friday { get; set; }
        public int? Saturday { get; set; }
        public int? Sunday { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
