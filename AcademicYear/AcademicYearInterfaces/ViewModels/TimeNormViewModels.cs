using System;
using System.ComponentModel;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class TimeNormPageViewModel : PageSettingListViewModel<TimeNormViewModel> { }

    public class TimeNormViewModel : PageSettingElementViewModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid DisciplineBlockId { get; set; }

        public string AcademicYear { get; set; }

        public string DisciplineBlockName { get; set; }

        [DisplayName("Название")]
        public string TimeNormName { get; set; }

        [DisplayName("Краткое")]
        public string TimeNormShortName { get; set; }

        public int TimeNormOrder { get; set; }

        public string TimeNormEducationDirectionQualification { get; set; }

        [DisplayName("Вид нагрузки")]
        public string KindOfLoadName { get; set; }

        public string KindOfLoadAttributeName { get; set; }

        public string KindOfLoadBlueAsteriskName { get; set; }

        public string KindOfLoadBlueAsteriskAttributeName { get; set; }

        public string KindOfLoadBlueAsteriskPracticName { get; set; }

        [DisplayName("Тип нагрузки")]
        public string KindOfLoadType { get; set; }

        [DisplayName("Часы")]
        public decimal? Hours { get; set; }

        [DisplayName("Числ. коэф.")]
        public decimal? NumKoef { get; set; }

        [DisplayName("Коэф. норм. вр.")]
        public string TimeNormKoef { get; set; }

        public bool UseInLearningProgress { get; set; }

        public bool UseInSite { get; set; }
    }
}