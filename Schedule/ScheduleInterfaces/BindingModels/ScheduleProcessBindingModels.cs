using AcademicYearInterfaces.ViewModels;
using ScheduleInterfaces.ViewModels;
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

        public Guid SeasonDatesId { get; set; }

        public List<ScheduleLessonTimeViewModel> Times { get; set; }

        public SeasonDatesViewModel Dates { get; set; }
    }

    /// <summary>
    /// Выгружаем расписание по аудиториям в html-файлы для текущей версии сайта
    /// </summary>
    public class ExportToHTMLClassroomsBindingModel
    {
        public string FilePath { get; set; }

        public List<string> Classrooms { get; set; }

        public Guid SeasonDatesId { get; set; }
    }

    /// <summary>
    /// Импорт данных с общего сайта
    /// </summary>
    public class ImportToSemesterFromHTMLBindingModel
    {
        public string ScheduleUrl { get; set; }

        public bool IsFirstHalfSemester { get; set; }
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
        public Guid SeasonDateId { get; set; }

        public Guid DisciplineId { get; set; }
    }
}
