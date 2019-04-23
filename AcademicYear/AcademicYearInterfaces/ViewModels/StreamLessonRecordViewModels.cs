using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class StreamLessonRecordPageViewModel : PageSettingListViewModel<StreamLessonRecordViewModel> { }

    public class StreamLessonRecordViewModel : PageSettingElementViewModel
    {
        public Guid StreamLessonId { get; set; }

        public Guid AcademicPlanRecordElementId { get; set; }

        public string StreamLessonName { get; set; }

        public string AcademicPlanRecordElementText { get; set; }

        public bool IsMain { get; set; }
    }
}