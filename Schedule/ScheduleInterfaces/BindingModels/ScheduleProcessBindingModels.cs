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

    /// <summary>
    /// Загружаем данные по зачетам
    /// </summary>
    public class ImportToOffsetFromExcel
    {
        public string FileName { get; set; }
    }

    /// <summary>
    /// Загружаем данные по экзаменам
    /// </summary>
    public class ImportToExaminationFromExcel
    {
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
