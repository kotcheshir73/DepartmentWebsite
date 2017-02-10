using System.Collections.Generic;

namespace DepartmentService.BindingModels
{
    public class LoadHTMLForClassroomsBindingModel
    {
        public string ScheduleUrl { get; set; }

        public List<string> Classrooms { get; set; }
    }

    public class ExportToExcelClassroomsBindingModel
    {
        public string FileName { get; set; }

        public List<string> Classrooms { get; set; }
    }

    public class ExportToHTMLClassroomsBindingModel
    {
        public string FilePath { get; set; }

        public List<string> Classrooms { get; set; }
    }
}
