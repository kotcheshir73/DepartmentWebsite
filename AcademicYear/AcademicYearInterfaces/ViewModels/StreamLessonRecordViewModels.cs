using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class StreamLessonRecordPageViewModel : PageSettingListViewModel<StreamLessonRecordViewModel> { }

    public class StreamLessonRecordViewModel : PageSettingElementViewModel
    {
        [Display(Name = "Поток*")]
        public Guid StreamLessonId { get; set; }

        [Display(Name = "Нагрузка по записи учебного плана*")]
        public Guid AcademicPlanRecordElementId { get; set; }

        [Display(Name = "Запись учебного плана")]
        public Guid AcademicPlanRecordId { get; set; }

        [Display(Name = "Учебный план")]
        public Guid AcademicPlanId { get; set; }

        public string StreamLessonName { get; set; }

        [DisplayName("Запись")]
        public string AcademicPlanRecordElementText { get; set; }

        [DisplayName("Считать по этой записи часы")]
        [Display(Name = "Считать по этой записи часы")]
        public bool IsMain { get; set; }
    }
}