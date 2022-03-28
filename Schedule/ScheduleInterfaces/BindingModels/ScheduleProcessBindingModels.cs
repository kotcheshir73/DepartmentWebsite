using System;
using System.Collections.Generic;

namespace ScheduleInterfaces.BindingModels
{
    /// <summary>
    /// Импорт данных с общего сайта
    /// </summary>
    public class ImportToSemesterRecordsBindingModel
    {
        public DateTime ScheduleDate { get; set; }

        public List<string> ScheduleUrls { get; set; }
    }

    public class TimeTableAPIScheduleRecord
    {
        public string group { get; set; }

        public string nameOfLesson { get; set; }

        public string teacher { get; set; }

        public string room { get; set; }
    }

    public class TimeTableAPIScheduleDay
    {
        public int day { get; set; }

        public IList<TimeTableAPIScheduleRecord[]> lessons { get; set; }
    }

    public class TimeTableAPIScheduleWeek
    {
        public TimeTableAPIScheduleDay[] days { get; set; }
    }

    public class TimeTableAPIScheduleResponse
    {
        public TimeTableAPIScheduleWeek[] weeks { get; set; }
    }

    public class TimeTableAPIScheduleAnswer
    {
        public TimeTableAPIScheduleResponse response { get; set; }

        public string error { get; set; }
    }

    /// <summary>
    /// Загружаем данные по зачетам
    /// </summary>
    public class ImportToOffsetFromExcel
    {
        public DateTime ScheduleDate { get; set; }

        public string FileName { get; set; }
    }

    /// <summary>
    /// Загружаем данные по экзаменам
    /// </summary>
    public class ImportToExaminationFromExcel
    {
        public DateTime ScheduleDate { get; set; }

        public string FileName { get; set; }
    }

    public class LoadScheduleBindingModel
    {
        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ClassroomNumber { get; set; }

        public Guid? ClassroomId { get; set; }

        public string StudentGroupName { get; set; }

        public Guid? StudentGroupId { get; set; }

        public string DisciplineName { get; set; }

        public Guid? DisciplineId { get; set; }

        public string LecturerName { get; set; }

        public Guid? LecturerId { get; set; }
    }
}
