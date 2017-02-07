using System.Collections.Generic;

namespace DepartmentService.BindingModels
{
    public class LoadHTMLForClassroomsBindingModel
    {
        public string ScheduleUrl { get; set; }

        public List<string> Classrooms { get; set; } 
    }
}
