using System.Collections.Generic;

namespace DepartmentService.BindingModels
{
    public class ScheduleSemesterBindingModel
    {
        public string ClassroomId { get; set; }
    }

    public class LoadHTMLForClassroomsBindingModel
    {
        public string ScheduleUrl { get; set; }

        public List<string> Classrooms { get; set; }

        public string[] StopWords { get; set; }
    }

    public class ExportToExcelClassroomsBindingModel
    {
        public string FileName { get; set; }

        public List<string> Classrooms { get; set; }

        public long SeasonDatesId { get; set; }
    }

    public class ExportToHTMLClassroomsBindingModel
    {
        public string FilePath { get; set; }

        public List<string> Classrooms { get; set; }

        public long SeasonDatesId { get; set; }
    }
}
