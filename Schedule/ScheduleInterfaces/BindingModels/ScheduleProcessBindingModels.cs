using System;
using System.Collections.Generic;
using Tools.BindingModels;

namespace ScheduleInterfaces.BindingModels
{
    /// <summary>
    /// Выгружаем расписание по аудиториям в excel-файл
    /// </summary>
    public class ExportToExcelClassroomsBindingModel
    {
        public string FileName { get; set; }

        public List<string> Classrooms { get; set; }
    }

    /// <summary>
    /// Выгружаем расписание по аудиториям в html-файлы для текущей версии сайта
    /// </summary>
    public class ExportToHTMLClassroomsBindingModel
    {
        public string FilePath { get; set; }

        public List<string> Classrooms { get; set; }
    }

    /// <summary>
    /// Импорт данных с общего сайта
    /// </summary>
    public class ImportToSemesterFromHTMLBindingModel
    {
        public string ScheduleUrl { get; set; }
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

    /// <summary>
    /// 
    /// </summary>
    public class ScheduleRecordsForDiciplineBindingModel : PageSettingGetBinidingModel
    {
        public Guid DisciplineId { get; set; }
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
