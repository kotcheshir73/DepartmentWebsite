using System.ComponentModel.DataAnnotations;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class AcademicYearPageViewModel : PageSettingListViewModel<AcademicYearViewModel> { }

    public class AcademicYearViewModel : PageSettingElementViewModel
    {
        [MaxLength(10)]
        [Required]
        [Display(Name = "Название учебного года")]
        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}