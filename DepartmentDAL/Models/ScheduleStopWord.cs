using DepartmentDAL.Enums;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс стоп-слов для поиска по расписанию
    /// </summary>
    public class ScheduleStopWord
    {
        public long Id { get; set; }

        public string StopWord { get; set; }

        public ScheduleStopWordTypes StopWordType { get; set; }
    }
}
