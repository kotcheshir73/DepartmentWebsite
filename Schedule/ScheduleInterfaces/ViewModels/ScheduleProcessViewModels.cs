using Enums;
using Tools.ViewModels;

namespace ScheduleInterfaces.ViewModels
{
    public class ScheduleRecordsForDisciplinePageViewModel : PageSettingListViewModel<ScheduleRecordsForDisciplineViewModel> { }

    public class ScheduleRecordsForDisciplineViewModel : ScheduleRecordShortViewModel
    {
        public ScheduleRecordTypeForDiscipline Type { get; set; }

        public LessonTypes LessonType { get; set; }

        public string Date { get; set; }
    }
}