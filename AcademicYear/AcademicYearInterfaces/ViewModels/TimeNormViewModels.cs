using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class TimeNormPageViewModel : PageSettingListViewModel<TimeNormViewModel> { }

    public class TimeNormViewModel : PageSettingElementViewModel
    {
        [Required]
        [Display(Name = "Учебный год*")]
        public Guid AcademicYearId { get; set; }

        [Required]
        [Display(Name = "Блок дисциплин*")]
        public Guid DisciplineBlockId { get; set; }

        public string AcademicYear { get; set; }

        public string DisciplineBlockName { get; set; }

        [MaxLength(50)]
        [Required]
        [DisplayName("Название")]
        [Display(Name = "Название*")]
        public string TimeNormName { get; set; }

        [MaxLength(5)]
        [Required]
        [DisplayName("Краткое")]
        [Display(Name = "Краткое название*")]
        public string TimeNormShortName { get; set; }

        [Required]
        [Display(Name = "Порядок*")]
        public int TimeNormOrder { get; set; }

        [Display(Name = "Уровень обучения")]
        public string TimeNormEducationDirectionQualification { get; set; }

        [MaxLength(100)]
        [Required]
        [DisplayName("Вид нагрузки")]
        [Display(Name = "Название вида нагрузки*")]
        public string KindOfLoadName { get; set; }

        [MaxLength(10)]
        [Display(Name = "Имя атрибута")]
        public string KindOfLoadAttributeName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Синоним для синей звездочки")]
        public string KindOfLoadBlueAsteriskName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Атрибут для получения часов")]
        public string KindOfLoadBlueAsteriskAttributeName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Синоним для практики")]
        public string KindOfLoadBlueAsteriskPracticName { get; set; }

        [Required]
        [DisplayName("Тип нагрузки")]
        [Display(Name = "Тип нагрузки*")]
        public string KindOfLoadType { get; set; }

        [DisplayName("Часы")]
        [Display(Name = "Часы")]
        public decimal? Hours { get; set; }

        [DisplayName("Числ. коэф.")]
        [Display(Name = "Числовой коэффициент")]
        public decimal? NumKoef { get; set; }

        [Required]
        [DisplayName("Коэф. норм. вр.")]
        [Display(Name = "Коэффициент норм времени*")]
        public string TimeNormKoef { get; set; }

        [Display(Name = "Выводить при настройке дисциплины")]
        public bool UseInLearningProgress { get; set; }

        [Display(Name = "Выводить на сайте")]
        public bool UseInSite { get; set; }
    }
}