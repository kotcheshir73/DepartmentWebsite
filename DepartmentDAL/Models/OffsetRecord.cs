namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий зачет на зачетной неделе
    /// </summary>
    public class OffsetRecord : ScheduleRecord
    {
        public int Week { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }
    }
}
