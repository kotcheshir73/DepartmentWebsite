using DepartmentService.Enums;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleServiceInterfaces.ViewModels
{
    public class ScheduleRecordsForDisciplinePageViewModel : PageViewModel<ScheduleRecordsForDisciplineViewModel> { }

    public class ScheduleRecordsForDisciplineViewModel : ScheduleRecordShortViewModel
    {
        public ScheduleRecordTypeForDiscipline Type { get; set; }

        public string LessonType { get; set; }

        public string Date { get; set; }
    }
}
