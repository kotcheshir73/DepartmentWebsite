using Tools.ViewModels;

namespace ScheduleInterfaces.ViewModels
{
    //TODO заменить на ScheduleRecordShortViewModel
    public class ScheduleRecordsForDisciplinePageViewModel : PageSettingListViewModel<ScheduleRecordsForDisciplineViewModel> { }

    public class ScheduleRecordsForDisciplineViewModel : ScheduleRecordShortViewModel
    {
        public string Date { get; set; }
    }
}